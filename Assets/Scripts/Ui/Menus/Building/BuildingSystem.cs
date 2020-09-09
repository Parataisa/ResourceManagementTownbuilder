using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingSystem : MonoBehaviour
    {
    public GameObject buildingPanel;
    public static Dictionary<int, string> buildingDirectory = new Dictionary<int, string>();
    public GameObject[] placeableObjectPrefabs;
    private GameObject currentPlaceableObject;
    private bool objectPlacable;

#pragma warning disable IDE0051 // Remove unused private members
    private void Start()
#pragma warning restore IDE0051 // Remove unused private members
        {
        UpdateItemsInDictionary();
        }
#pragma warning disable IDE0051 // Remove unused private members
    private void LateUpdate()
#pragma warning restore IDE0051 // Remove unused private members
        {
        if (placeableObjectPrefabs.Length == buildingDirectory.Count)
            {
            return;
            }
        else
            {
            UpdateItemsInDictionary();
            buildingPanel.GetComponent<BuildingMenuButtonManagment>().UpdateButtons();
            }
        }

    public void UpdateItemsInDictionary()
        {
        if (buildingDirectory.Count == 0)
            {
            for (int i = 0; i < placeableObjectPrefabs.Length; i++)
                {
                buildingDirectory.Add(i, placeableObjectPrefabs[i].name);
                }
            }
        else
            {
            buildingDirectory.Clear();
            for (int i = 0; i < placeableObjectPrefabs.Length; i++)
                {
                buildingDirectory.Add(i, placeableObjectPrefabs[i].name);
                }
            }
        }

#pragma warning disable IDE0051 // Remove unused private members
    private void Update()
#pragma warning restore IDE0051 // Remove unused private members
        {
        //HandleNewObjectHotkey();

        if (currentPlaceableObject != null && !EventSystem.current.IsPointerOverGameObject())
            {
            MoveCurrentObjectToMouse();
            ReleaseIfClicked();
            }
        }
    public void OnButtonClick(int buildingId)
        {
        if (currentPlaceableObject != null)
            {
            Destroy(currentPlaceableObject);
            currentPlaceableObject = null;
            return;
            }
        currentPlaceableObject = Instantiate(placeableObjectPrefabs[buildingId]);

        }
    public void ClearCurser()
        {
        Destroy(currentPlaceableObject);
        currentPlaceableObject = null;
        }

    private void MoveCurrentObjectToMouse()
        {
        float gameObjectSizeOffsetX = currentPlaceableObject.transform.localScale.x / 2;
        float gameObjectSizeOffsetY = currentPlaceableObject.transform.localScale.y / 2;
        float gameObjectSizeOffsetZ = currentPlaceableObject.transform.localScale.z / 2;
        GameObject hitObject;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {

            currentPlaceableObject.transform.position = hitInfo.point;
            if (currentPlaceableObject.transform.position.y != gameObjectSizeOffsetY)
                {
                currentPlaceableObject.transform.position = new Vector3(hitInfo.point.x, gameObjectSizeOffsetY, hitInfo.point.z);
                }
            if (hitInfo.transform.gameObject.layer == 9 || hitInfo.transform.gameObject.layer == 8)
                {
                if (hitInfo.transform.gameObject)
                    {
                    hitObject = hitInfo.transform.gameObject;
                    objectPlacable = false;
                    currentPlaceableObject.transform.position = new Vector3(hitInfo.point.x - gameObjectSizeOffsetX, gameObjectSizeOffsetY, hitInfo.point.z - gameObjectSizeOffsetZ);
                    if (hitObject.transform.position.x > hitInfo.point.x)
                        {
                        currentPlaceableObject.transform.position = new Vector3(hitInfo.point.x - gameObjectSizeOffsetX, gameObjectSizeOffsetY, hitInfo.point.z - gameObjectSizeOffsetZ);
                        }
                    else if (hitObject.transform.position.x < hitInfo.point.x)
                        {
                        currentPlaceableObject.transform.position = new Vector3(hitInfo.point.x + gameObjectSizeOffsetX, gameObjectSizeOffsetY, hitInfo.point.z - gameObjectSizeOffsetZ);
                        }
                    }
                }
            else
                {
                objectPlacable = true;
                }
            }
        }

    private void ReleaseIfClicked()
        {
        if (Input.GetMouseButtonDown(0) && objectPlacable)
            {
            currentPlaceableObject.layer = 8;
            currentPlaceableObject = null;
            }
        }
    }
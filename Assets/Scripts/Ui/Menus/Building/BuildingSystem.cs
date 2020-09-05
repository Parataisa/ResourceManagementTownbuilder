using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingSystem : MonoBehaviour
    {
    public GameObject buildingPanel;
    public static Dictionary<int, string> buildingDirectory = new Dictionary<int, string>();
    public GameObject[] placeableObjectPrefabs;
    private GameObject currentPlaceableObject;

    private float mouseWheelRotation;
    //private int currentPrefabIndex = -1;

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
            RotateFromMouseWheel();
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

    //private void HandleNewObjectHotkey()
    //    {
    //    for (int i = 0; i < placeableObjectPrefabs.Length; i++)
    //        {
    //        if (Input.GetKeyDown(KeyCode.Alpha0 + 1 + i))
    //            {
    //            if (PressedKeyOfCurrentPrefab(i))
    //                {
    //                Destroy(currentPlaceableObject);
    //                currentPrefabIndex = -1;
    //                }
    //            else
    //                {
    //                if (currentPlaceableObject != null)
    //                    {
    //                    Destroy(currentPlaceableObject);
    //                    }
    //                currentPlaceableObject = Instantiate(placeableObjectPrefabs[i]);
    //                currentPrefabIndex = i;
    //                }
    //            break;
    //            }
    //        }
    //    }

    //private bool PressedKeyOfCurrentPrefab(int i)
    //    {
    //    return currentPlaceableObject != null && currentPrefabIndex == i;
    //    }

    private void MoveCurrentObjectToMouse()
        {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
            currentPlaceableObject.transform.position = hitInfo.point;
            currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
            }
        }

    private void RotateFromMouseWheel()
        {
        mouseWheelRotation += Input.mouseScrollDelta.y;
        currentPlaceableObject.transform.Rotate(Vector3.up, mouseWheelRotation * 10f);
        }

    private void ReleaseIfClicked()
        {
        if (Input.GetMouseButtonDown(0))
            {
            currentPlaceableObject.layer = 8;
            currentPlaceableObject = null;
            }
        }
    }
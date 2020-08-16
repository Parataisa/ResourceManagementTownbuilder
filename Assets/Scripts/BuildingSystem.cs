using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingSystem : MonoBehaviour
    {

    public GameObject buildingPanel;
    public GameObject buttonPrefab;
    Dictionary<string, int> buildingDirectory = new Dictionary<string, int>();

    [SerializeField]
    public GameObject[] placeableObjectPrefabs = null;

    private GameObject currentPlaceableObject;

    private float mouseWheelRotation;
    private int currentPrefabIndex = -1;

    private void Start()
        {
        AddingItemToDictionary();
        }

    private void AddingItemToDictionary()
        {
        for (int i = 0; i < placeableObjectPrefabs.Length; i++)
            {
            buildingDirectory.Add(placeableObjectPrefabs[i].name, i);
            }
        }

    private void Update()
        {
        HandleNewObjectHotkey();
        if (currentPlaceableObject != null && !EventSystem.current.IsPointerOverGameObject())
            {
            MoveCurrentObjectToMouse();
            RotateFromMouseWheel();
            ReleaseIfClicked();
            }
        }
    public void OnButtonClick(String buildingId)
        {
        if (currentPlaceableObject != null)
            {
            Destroy(currentPlaceableObject);
            currentPlaceableObject = null;
            return;
            }
        currentPlaceableObject = Instantiate(placeableObjectPrefabs[Int32.Parse(buildingId)]);

        }
    public void ClearCurser()
        {
        Destroy(currentPlaceableObject);
        currentPlaceableObject = null;
        }

    private void HandleNewObjectHotkey()
        {
        for (int i = 0; i < placeableObjectPrefabs.Length; i++)
            {
            if (Input.GetKeyDown(KeyCode.Alpha0 + 1 + i))
                {
                if (PressedKeyOfCurrentPrefab(i))
                    {
                    Destroy(currentPlaceableObject);
                    currentPrefabIndex = -1;
                    }
                else
                    {
                    if (currentPlaceableObject != null)
                        {
                        Destroy(currentPlaceableObject);
                        }

                    currentPlaceableObject = Instantiate(placeableObjectPrefabs[i]);
                    currentPrefabIndex = i;
                    }
                break;
                }
            }
        }

    private bool PressedKeyOfCurrentPrefab(int i)
        {
        return currentPlaceableObject != null && currentPrefabIndex == i;
        }

    private void MoveCurrentObjectToMouse()
        {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
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
            currentPlaceableObject.layer = 0;
            currentPlaceableObject = null;
            }
        }
    }
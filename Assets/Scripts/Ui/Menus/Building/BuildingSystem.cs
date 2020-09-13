using Assets.Scripts.Buildings.ResourceBuildings;
using Assets.Scripts.Buildings.SocialBuildings;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingSystem : MonoBehaviour
    {
    public GameObject buildingPanel;
    public static Dictionary<int, string> buildingDirectory = new Dictionary<int, string>();
    public Object[] ResouceBuildingsListObjects;
    public Object[] SocialBuildingsListObjects;
    public GameObject[] placeableObjectPrefabs;
    private GameObject currentPlaceableObject;
    private bool objectPlacable;

    private void Start()
        {
        ResouceBuildingsListObjects = Resources.LoadAll("GameObjects/Buildings/ResourceBuildings", typeof(GameObject));
        SocialBuildingsListObjects = Resources.LoadAll("GameObjects/Buildings/SocialBuildings", typeof(GameObject));
        placeableObjectPrefabs = new GameObject[ResouceBuildingsListObjects.Length + SocialBuildingsListObjects.Length];
        GetBuildingsInPlacableObjects(ResouceBuildingsListObjects, SocialBuildingsListObjects, placeableObjectPrefabs);
        }

    private void GetBuildingsInPlacableObjects(Object[] resouceBuildingsListObjects, Object[] socialBuildingsListObjects, GameObject[] placeableObject)
        {
        if (buildingDirectory.Count == 0)
            {
            UpdatePlaceableObjects(resouceBuildingsListObjects, socialBuildingsListObjects, placeableObject);
            }
        else
            {
            buildingDirectory.Clear();
            UpdatePlaceableObjects(resouceBuildingsListObjects, socialBuildingsListObjects, placeableObject);
            }
        }

    private static void UpdatePlaceableObjects(Object[] resouceBuildingsListObjects, Object[] socialBuildingsListObjects, GameObject[] placeableObject)
        {
        int i = 0;
        foreach (var resouceBuilding in resouceBuildingsListObjects)
            {
            buildingDirectory.Add(i, resouceBuilding.name);
            placeableObject[i] = (GameObject)resouceBuilding;
            placeableObject[i].name = resouceBuilding.name;
            i++;
            }
        foreach (var socialBuilding in socialBuildingsListObjects)
            {
            buildingDirectory.Add(i, socialBuilding.name);
            placeableObject[i] = (GameObject)socialBuilding;
            placeableObject[i].name = socialBuilding.name;
            i++;
            }
        }
    private void Update()
        {

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
            if (hitInfo.transform.gameObject.layer == 8 || hitInfo.transform.gameObject.layer == 9 || hitInfo.transform.gameObject.layer == 10)
                {
                if (hitInfo.transform.gameObject)
                    {
                    hitObject = hitInfo.transform.gameObject;
                    objectPlacable = false;
                    currentPlaceableObject.transform.position = new Vector3(hitInfo.point.x - gameObjectSizeOffsetX, gameObjectSizeOffsetY, hitInfo.point.z - gameObjectSizeOffsetZ);
                    if (hitObject.transform.position.x > hitInfo.point.x)
                        {
                        if (hitObject.transform.position.z > hitInfo.point.z)
                            {
                            currentPlaceableObject.transform.position = new Vector3(hitInfo.point.x - gameObjectSizeOffsetX, gameObjectSizeOffsetY, hitInfo.point.z - gameObjectSizeOffsetZ);
                            }
                        else if (hitObject.transform.position.z < hitInfo.point.z)
                            {
                            currentPlaceableObject.transform.position = new Vector3(hitInfo.point.x - gameObjectSizeOffsetX, gameObjectSizeOffsetY, hitInfo.point.z + gameObjectSizeOffsetZ);
                            }
                        }
                    else if (hitObject.transform.position.x < hitInfo.point.x)
                        {
                        if (hitObject.transform.position.z > hitInfo.point.z)
                            {
                            currentPlaceableObject.transform.position = new Vector3(hitInfo.point.x + gameObjectSizeOffsetX, gameObjectSizeOffsetY, hitInfo.point.z - gameObjectSizeOffsetZ);
                            }
                        else if (hitObject.transform.position.z < hitInfo.point.z)
                            {
                            currentPlaceableObject.transform.position = new Vector3(hitInfo.point.x + gameObjectSizeOffsetX, gameObjectSizeOffsetY, hitInfo.point.z + gameObjectSizeOffsetZ);
                            }
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
            if (currentPlaceableObject.name.Contains("Social"))
                {
                currentPlaceableObject.layer = 8;
                currentPlaceableObject.AddComponent<SocialBuildingBase>();
                GameObject socilaBuildingMain = new GameObject
                    {
                    name = "SocilaBuildingMain"
                    };
                socilaBuildingMain.AddComponent<SocialBuildingManagment>();
                currentPlaceableObject.transform.parent = socilaBuildingMain.transform;
                currentPlaceableObject = null;
                }
            else if (currentPlaceableObject.name.Contains("Resouce"))
                {
                currentPlaceableObject.layer = 9;
                currentPlaceableObject.AddComponent<ResourceBuildingBase>();
                GameObject resouceBuildingMain = new GameObject
                    {
                    name = "ResouceBuildingMain"
                    };
                resouceBuildingMain.AddComponent<ResouceBuildingsManagment>();
                currentPlaceableObject.transform.parent = resouceBuildingMain.transform;
                currentPlaceableObject = null;
                }
            }
        }
    }
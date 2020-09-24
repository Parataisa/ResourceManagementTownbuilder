using Assets.Scripts.Buildings.ResourceBuildings;
using Assets.Scripts.Buildings.SocialBuildings;
using Assets.Scripts.TerrainGeneration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Buildings
    {
    public class BuildingSystem : MonoBehaviour
        {
        public GameObject buildingPanel;
        public static Dictionary<int, string> buildingDirectory = new Dictionary<int, string>();
        public UnityEngine.Object[] ResouceBuildingsListObjects;
        public UnityEngine.Object[] SocialBuildingsListObjects;
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

        private void GetBuildingsInPlacableObjects(UnityEngine.Object[] resouceBuildingsListObjects, UnityEngine.Object[] socialBuildingsListObjects, GameObject[] placeableObject)
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

        private static void UpdatePlaceableObjects(UnityEngine.Object[] resouceBuildingsListObjects, UnityEngine.Object[] socialBuildingsListObjects, GameObject[] placeableObject)
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
                DrawCircle(currentPlaceableObject, ResourceBuildingBase.ResourceCollectingRadius);
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
            float gameObjectSizeOffsetY = currentPlaceableObject.transform.localScale.y / 2;
            GameObject[] hitObject;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
                {
                currentPlaceableObject.transform.position = hitInfo.point;
                if (currentPlaceableObject.transform.position.y != gameObjectSizeOffsetY)
                    {
                    currentPlaceableObject.transform.position = new Vector3(hitInfo.point.x, gameObjectSizeOffsetY, hitInfo.point.z);
                    }
                hitObject = ScannForObjectsInArea(hitInfo, false);
                if (hitObject[0] == null)
                    {
                    objectPlacable = true;
                    currentPlaceableObject.GetComponent<Renderer>().material.color = currentPlaceableObject.GetComponent<IBuildings>().BuildingColor;
                    }
                else
                    {
                    var currentPlacableObjectCollider = currentPlaceableObject.GetComponent<BoxCollider>();
                    foreach (var objectInList in hitObject)
                        {
                        if (objectInList == null)
                            {
                            break;
                            }
                        var hitobjectCollider = objectInList.GetComponent<BoxCollider>();
                        if (currentPlacableObjectCollider.bounds.Intersects(hitobjectCollider.bounds))
                            {
                            objectPlacable = false;
                            currentPlaceableObject.GetComponent<Renderer>().material.color = UnityEngine.Color.magenta;
                            break;
                            }
                        else
                            {
                            objectPlacable = true;
                            currentPlaceableObject.GetComponent<Renderer>().material.color = currentPlaceableObject.GetComponent<IBuildings>().BuildingColor;
                            }
                        }
                    }
                }
            }
        private void ReleaseIfClicked()
            {
            if (Input.GetMouseButtonDown(0) && objectPlacable)
                {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hitInfo))
                    {
                    _ = new GameObject[20];
                    GameObject[] gameObjectArray = ScannForObjectsInArea(hitInfo, true);
                    int hitObjectCount = 0;
                    foreach (var hitObject in gameObjectArray)
                        {
                        if (hitObject != null)
                            {
                            hitObjectCount++;
                            }
                        }
                    GameObject[] hitBuildingArray = new GameObject[hitObjectCount];
                    Array.Copy(gameObjectArray, hitBuildingArray, hitObjectCount);                  
                    if (currentPlaceableObject.name.Contains("Social"))
                        {
                        if (hitBuildingArray.Length == 0)
                            {
                            CreateSocialBuilding(false, null);
                            return;
                            }
                        int x = 1;
                        foreach (GameObject child in hitBuildingArray)
                            {
                            if (child.name != currentPlaceableObject.name && x >= hitBuildingArray.Length)
                                {
                                CreateSocialBuilding(false, null);
                                break;
                                }
                            else if (child.name == currentPlaceableObject.name)
                                {
                                CreateSocialBuilding(true, hitBuildingArray[x - 1].transform.parent.gameObject);
                                break;
                                }
                            else
                                {
                                x++;
                                continue;
                                }
                            }
                        }
                    else if (currentPlaceableObject.name.Contains("Resouce"))
                        {
                        if (hitBuildingArray.Length == 0)
                            {
                            CreateResouceBuilding(false, null);
                            return;
                            }
                        int x = 1;
                        foreach (GameObject child in hitBuildingArray)
                            {
                            if (child.name != currentPlaceableObject.name && x >= hitBuildingArray.Length)
                                {
                                CreateResouceBuilding(false, null);
                                break;
                                }
                            else if (child.name == currentPlaceableObject.name)
                                {
                                CreateResouceBuilding(true, hitBuildingArray[x - 1].transform.parent.gameObject);
                                break;
                                }
                            else
                                {
                                x++;
                                continue;
                                }
                            }
                        }
                    }
                }
            }

        private void CreateResouceBuilding(bool sameBuildingTypeNearby, GameObject parent)
            {
            currentPlaceableObject.layer = 9;
            currentPlaceableObject.AddComponent<ResourceBuildingBase>();
            if (sameBuildingTypeNearby == false)
                {
                string[] buildingName = currentPlaceableObject.name.Split('-');
                GameObject resouceBuildingMain = new GameObject
                    {
                    name = "(ResouceBuildingMain)-" + buildingName[1]
                    };
                resouceBuildingMain.AddComponent<ResourceBuildingsManagment>();
                currentPlaceableObject.transform.parent = resouceBuildingMain.transform;
                }
            else
                {
                currentPlaceableObject.transform.parent = parent.transform;
                }
            Destroy(currentPlaceableObject.GetComponent<LineRenderer>());
            currentPlaceableObject = null;
            }

        private void CreateSocialBuilding(bool sameBuildingTypeNearby, GameObject parent)
            {
            currentPlaceableObject.layer = 8;
            currentPlaceableObject.AddComponent<SocialBuildingBase>();
            if (sameBuildingTypeNearby == false)
                {
                string[] buildingName = currentPlaceableObject.name.Split('-');
                GameObject socilaBuildingMain = new GameObject
                    {
                    name = "(SocialBuildingMain)-" + buildingName[1]
                    };
                socilaBuildingMain.AddComponent<SocialBuildingManagment>();
                currentPlaceableObject.transform.parent = socilaBuildingMain.transform;
                }
            else
                {
                currentPlaceableObject.transform.parent = parent.transform;
                }
            Destroy(currentPlaceableObject.GetComponent<LineRenderer>());
            currentPlaceableObject = null;
            }
        private GameObject[] ScannForObjectsInArea(RaycastHit hitInfo, bool buildingScann)
            {
            Collider[] collidersInArea = new Collider[100];
            int collisions = Physics.OverlapSphereNonAlloc(hitInfo.point, 15, collidersInArea);
            GameObject[] gameObjectArray = new GameObject[collisions];
            if (collisions <= 2)
                {
                return gameObjectArray;
                }
            int i = 0;
            foreach (Collider collider in collidersInArea)
                {
                if (collider == null)
                    {
                    return gameObjectArray;
                    }
                if (buildingScann && (collider.gameObject.layer == 8 || collider.gameObject.layer == 9))
                    {
                    GameObject parent = collider.transform.parent.gameObject;
                    if (gameObjectArray.Contains(parent))
                        continue;
                    else
                        {
                        if (i > 40)
                            {
                            return gameObjectArray;
                            }
                        gameObjectArray[i] = collider.gameObject;
                        i++;
                        }
                    }
                else if (!buildingScann && (collider.gameObject.layer == 8 || collider.gameObject.layer == 9 || collider.gameObject.layer == 10))
                    {
                    if (i > 40)
                        {
                        return gameObjectArray;
                        }
                    gameObjectArray[i] = collider.gameObject;
                    i++;
                    }
                }
            return gameObjectArray;
            }
        private static void DrawCircle(GameObject container, float radius, float lineWidth = 0.2f)
            {
            var segments = 360;
            if (container.GetComponent<LineRenderer>() == null)
                {
                container.AddComponent<LineRenderer>();
                }
            else
                {
                var line = container.GetComponent<LineRenderer>();
                line.useWorldSpace = false;
                line.startWidth = lineWidth;
                line.endWidth = lineWidth;
                line.positionCount = segments + 1;

                var pointCount = segments + 1;
                var points = new Vector3[pointCount];

                for (int i = 0; i < pointCount; i++)
                    {
                    var rad = Mathf.Deg2Rad * (i * 360f / segments);
                    points[i] = new Vector3(Mathf.Sin(rad) * radius, 0, Mathf.Cos(rad) * radius);
                    }

                line.SetPositions(points);
                }
            }
        }

    }
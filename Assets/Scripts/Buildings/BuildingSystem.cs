using Assets.Scripts.Buildings.ResourceBuildings;
using Assets.Scripts.Buildings.SocialBuildings;
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
                    currentPlaceableObject.GetComponent<Renderer>().material.color = currentPlaceableObject.GetComponent<IResourcBuilding>().BuildingColor;
                    }
                else
                    {
                    Rectangle placableRectangle = GetRectangle(currentPlaceableObject);
                    foreach (var objectInList in hitObject)
                        {
                        if (objectInList == null)
                            {
                            break;
                            }
                        Rectangle hitRectangle = GetRectangle(objectInList);
                        if (placableRectangle.IntersectsWith(hitRectangle))
                            {
                            objectPlacable = false;
                            currentPlaceableObject.GetComponent<Renderer>().material.color = UnityEngine.Color.magenta;
                            break;
                            }
                        else
                            {
                            objectPlacable = true;
                            currentPlaceableObject.GetComponent<Renderer>().material.color = currentPlaceableObject.GetComponent<IResourcBuilding>().BuildingColor;
                            }
                        }
                    }
                }
            }
        private static Rectangle GetRectangle(GameObject hitGameObject)
            {
            Rectangle RecArea;
            RecArea.X = Mathf.CeilToInt((hitGameObject.transform.position.x - hitGameObject.transform.localScale.x / 2));
            RecArea.Y = Mathf.CeilToInt((hitGameObject.transform.position.z - hitGameObject.transform.localScale.z / 2));
            RecArea.Width = Mathf.CeilToInt(hitGameObject.transform.localScale.x);
            RecArea.Height = Mathf.CeilToInt(hitGameObject.transform.localScale.z);
            return RecArea;
            }

        private void ReleaseIfClicked()
            {
            if (Input.GetMouseButtonDown(0) && objectPlacable)
                {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hitInfo))
                    {
                    GameObject[] gameObjectArray = new GameObject[20];
                    gameObjectArray = ScannForObjectsInArea(hitInfo, true);
                    if (currentPlaceableObject.name.Contains("Social"))
                        {
                        if (gameObjectArray[0] == null)
                            {
                            CreateSocialBuilding(false, null);
                            return;
                            }
                        int x = 0;
                        foreach (GameObject child in gameObjectArray)
                            {
                            if (child.name != currentPlaceableObject.name && x < gameObjectArray.Length)
                                {
                                break;
                                }
                            else if (child.name == currentPlaceableObject.name)
                                {
                                CreateSocialBuilding(true, gameObjectArray[x].transform.parent.gameObject);
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
                        if (gameObjectArray[0] == null)
                            {
                            CreateResouceBuilding(false, null);
                            return;
                            }
                        int x = 0;
                        foreach (GameObject child in gameObjectArray)
                            {
                            if (child.name != currentPlaceableObject.name && x < gameObjectArray.Length)
                                {
                                break;
                                }
                            else if (child.name == currentPlaceableObject.name)
                                {
                                CreateResouceBuilding(true, gameObjectArray[x].transform.parent.gameObject);
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
                resouceBuildingMain.AddComponent<ResouceBuildingsManagment>();
                currentPlaceableObject.transform.parent = resouceBuildingMain.transform;
                }
            else
                {
                currentPlaceableObject.transform.parent = parent.transform;
                }
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
            currentPlaceableObject = null;
            }
        private GameObject[] ScannForObjectsInArea(RaycastHit hitInfo, bool buildingScann)
            {
            Collider[] collidersInArea = new Collider[100];
            int collisions = Physics.OverlapSphereNonAlloc(hitInfo.point, 15, collidersInArea);
            GameObject[] gameObjectArray = new GameObject[100];
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
        }
    }
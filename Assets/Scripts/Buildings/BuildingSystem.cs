using Assets.Scripts.Buildings.BuildingSystemHelper;
using Assets.Scripts.Buildings.ResourceBuildings;
using Assets.Scripts.Buildings.SocialBuildings;
using System;
using System.Collections.Generic;
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
        public GameObject currentPlaceableObject;
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
                DrawGatheringCircle.DrawCircle(currentPlaceableObject, ResourceBuildingBase.ResourceCollectingRadius);
                CreateBuildingIfClicked(MoveCurrentObjectToMouse().Item1, MoveCurrentObjectToMouse().Item2, GetLocalMesh());
                }
            }

        private GameObject GetLocalMesh()
            {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
                {
                if (hitInfo.transform.gameObject.layer == 11)
                    {
                    return hitInfo.transform.gameObject;
                    }
                }
            return null;
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

        private Tuple<bool, GameObject> MoveCurrentObjectToMouse()
            {
            float gameObjectSizeOffsetY = currentPlaceableObject.transform.localScale.y / 2;
            bool IsbuildingHit = false;
            GameObject[] hitObject;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
                {
                if (!IsbuildingHit)
                    {
                    currentPlaceableObject.transform.position = hitInfo.point;
                    }
                if (currentPlaceableObject.transform.position.y != gameObjectSizeOffsetY)
                    {
                    currentPlaceableObject.transform.position = new Vector3(hitInfo.point.x, gameObjectSizeOffsetY, hitInfo.point.z);
                    }
                hitObject = ScannForObjectsInArea(hitInfo, 4);
                if (hitObject[0] == null)
                    {
                    objectPlacable = true;
                    IsbuildingHit = false;
                    BuildingColoringSystem.ResetBuildingToOrigionColor(currentPlaceableObject);
                    }
                else
                    {
                    BoxCollider currentPlacableObjectCollider = currentPlaceableObject.GetComponent<BoxCollider>();
                    foreach (var hitObjectInList in hitObject)
                        {
                        if (hitObjectInList == null)
                            {
                            break;
                            }
                        BoxCollider hitobjectCollider = hitObjectInList.GetComponent<BoxCollider>();
                        if (currentPlacableObjectCollider.bounds.Intersects(hitobjectCollider.bounds))
                            {
                            if (hitobjectCollider.transform.gameObject.layer == 8 || hitobjectCollider.transform.gameObject.layer == 9)
                                {
                                if (currentPlaceableObject.GetComponent<IBuildings>().BuildingTyp == hitobjectCollider.gameObject.GetComponent<IBuildings>().BuildingTyp)
                                    {
                                    IsbuildingHit = CouplingBuildingsWithEatchOther(currentPlaceableObject, hitobjectCollider.gameObject);
                                    objectPlacable = true;
                                    BuildingColoringSystem.ResetBuildingToOrigionColor(currentPlaceableObject);
                                    return Tuple.Create(IsbuildingHit, hitobjectCollider.gameObject);
                                    }
                                else
                                    {
                                    objectPlacable = false;
                                    BuildingColoringSystem.SetColorForCollisions(currentPlaceableObject, Color.magenta);
                                    return Tuple.Create(IsbuildingHit, hitobjectCollider.gameObject);
                                    }
                                }
                            else
                                {
                                objectPlacable = false;
                                BuildingColoringSystem.SetColorForCollisions(currentPlaceableObject, Color.magenta);
                                return Tuple.Create(IsbuildingHit, hitobjectCollider.gameObject);
                                }
                            }
                        else
                            {
                            objectPlacable = true;
                            BuildingColoringSystem.ResetBuildingToOrigionColor(currentPlaceableObject);
                            }
                        }
                    }
                }
            return Tuple.Create(IsbuildingHit, currentPlaceableObject);
            }
        private bool CouplingBuildingsWithEatchOther(GameObject c, GameObject h)
            {
            Vector3 MouseCurserWorldPosition = new Vector3();
            Plane plane = new Plane(Vector3.up, 0);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out float distance))
                {
                MouseCurserWorldPosition = ray.GetPoint(distance);
                }
            if (BuildingSystemHitChecker.NorthHitCheck(h, MouseCurserWorldPosition))
                {
                SetBuildingCouplingPosition.MoveBuildingToTheNorth(c, h);
                return true;
                }
            else if (BuildingSystemHitChecker.EastHitCheck(h, MouseCurserWorldPosition))
                {
                SetBuildingCouplingPosition.MoveBuildingToTheEast(c, h);
                return true;
                }
            else if (BuildingSystemHitChecker.SouthHitCheck(h, MouseCurserWorldPosition))
                {
                SetBuildingCouplingPosition.MoveBuildingToTheSouth(c, h);
                return true;
                }
            else if (BuildingSystemHitChecker.WestHitCheck(h, MouseCurserWorldPosition))
                {
                SetBuildingCouplingPosition.MoveBuildingToTheWest(c, h);
                return true;
                }
            return false;
            }
        private void CreateBuildingIfClicked(bool IsbuildingHit, GameObject hitBuilding, GameObject localMesh)
            {
            if (Input.GetMouseButtonDown(0) && objectPlacable)
                {
                if (currentPlaceableObject.name.Contains("Social"))
                    {
                    if (!IsbuildingHit)
                        {
                        CreateSocialBuilding(IsbuildingHit, null, localMesh);
                        }
                    else if (IsbuildingHit)
                        {
                        CreateSocialBuilding(IsbuildingHit, hitBuilding.transform.parent.gameObject, localMesh);
                        }
                    }
                else if (currentPlaceableObject.name.Contains("Resouce"))
                    {
                    if (!IsbuildingHit)
                        {
                        CreateResouceBuilding(IsbuildingHit, null, localMesh);
                        return;
                        }
                    else if (IsbuildingHit)
                        {
                        CreateResouceBuilding(IsbuildingHit, hitBuilding.transform.parent.gameObject, localMesh);
                        }
                    }
                }
            }
        private void CreateResouceBuilding(bool sameBuildingTypeNearby, GameObject parent, GameObject localMesh)
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
                resouceBuildingMain.transform.parent = localMesh.transform;
                currentPlaceableObject.transform.parent = resouceBuildingMain.transform;
                }
            else
                {
                currentPlaceableObject.transform.parent = parent.transform;
                currentPlaceableObject.GetComponent<ResourceBuildingBase>().GatherableResouceInArea = parent.transform.GetChild(0).GetComponent<ResourceBuildingBase>().GatherableResouceInArea;
                }
            Destroy(currentPlaceableObject.GetComponent<LineRenderer>());
            currentPlaceableObject = null;
            }
        private void CreateSocialBuilding(bool sameBuildingTypeNearby, GameObject parent, GameObject localMesh)
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
                socilaBuildingMain.transform.parent = localMesh.transform;
                currentPlaceableObject.transform.parent = socilaBuildingMain.transform;
                }
            else
                {
                currentPlaceableObject.transform.parent = parent.transform;
                }
            Destroy(currentPlaceableObject.GetComponent<LineRenderer>());
            currentPlaceableObject = null;
            }

        private GameObject[] ScannForObjectsInArea(RaycastHit hitInfo, int scannRadius)
            {
            Collider[] collidersInArea = new Collider[20];
            int collisions = Physics.OverlapSphereNonAlloc(hitInfo.point, scannRadius, collidersInArea);
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
                if (collider.gameObject.layer == 8 || collider.gameObject.layer == 9 || collider.gameObject.layer == 10)
                    {
                    if (i > 20)
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
using Assets.Scripts.Buildings.BuildingSystemHelper;
using Assets.Scripts.Buildings.ResourceBuildings;
using Assets.Scripts.Buildings.SocialBuildings;
using Assets.Scripts.Ui.Menus.InfoUI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Buildings
    {
    public class BuildingSystem : MonoBehaviour
        {
        public GameObject buildingPanel;
        public static Dictionary<int, string> buildingDirectory = new Dictionary<int, string>();
        public GameObject currentPlaceableObject;
        public GameObject[] placeableObjectPrefabs;
        private Object[] BuildingsListObjects;
        private bool objectPlacable;
        private static int lastButtonHit;
        private bool CreatingBuilding = false;

        private void Start()
            {
            BuildingsListObjects = Resources.LoadAll("GameObjects/Buildings", typeof(GameObject));
            placeableObjectPrefabs = new GameObject[BuildingsListObjects.Length];
            GetBuildingsInPlacableObjects(BuildingsListObjects, placeableObjectPrefabs);
            }
        private void Update()
            {
            if (currentPlaceableObject != null && !EventSystem.current.IsPointerOverGameObject())
                {
                DrawGatheringCircle.DrawCircle(currentPlaceableObject, ResourceBuildingAccountant.ResourceCollectingRadius);
                MoveCurrentObjectToMouse();
                CreateBuildingIfClicked();
                }
            }
        private void GetBuildingsInPlacableObjects(Object[] buildingArray, GameObject[] placeableObject)
            {
            if (buildingDirectory.Count == 0)
                {
                UpdatePlaceableObjects(buildingArray, placeableObject);
                }
            else
                {
                buildingDirectory.Clear();
                UpdatePlaceableObjects(buildingArray, placeableObject);
                }
            }
        private static void UpdatePlaceableObjects(Object[] buildingArray, GameObject[] placeableObject)
            {
            int i = 0;
            foreach (var resouceBuilding in buildingArray)
                {
                buildingDirectory.Add(i, resouceBuilding.name);
                placeableObject[i] = (GameObject)resouceBuilding;
                placeableObject[i].name = resouceBuilding.name;
                i++;
                }
            }


        private GameObject GetLocalMesh()
            {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
                {
                if (hitInfo.transform.gameObject.layer == LayerClass.Ground)
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
            lastButtonHit = buildingId;
            currentPlaceableObject = Instantiate(placeableObjectPrefabs[buildingId]);
            }
        public void ClearCurser()
            {
            Destroy(currentPlaceableObject);
            currentPlaceableObject = null;
            }
        private GameObject MoveCurrentObjectToMouse()
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
                hitObject = ScannForObjectsInArea(hitInfo, 4);
                if (hitObject[0] == null)
                    {
                    objectPlacable = true;
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
                            objectPlacable = false;
                            BuildingColoringSystem.SetColorForCollisions(currentPlaceableObject, Color.magenta);
                            return hitobjectCollider.gameObject;
                            }
                        else
                            {
                            objectPlacable = true;
                            BuildingColoringSystem.ResetBuildingToOrigionColor(currentPlaceableObject);
                            }
                        }
                    }
                }
            return currentPlaceableObject;
            }
        private void CreateBuildingIfClicked()
            {
            if (Input.GetMouseButtonDown(0) && objectPlacable)
                {
                if (currentPlaceableObject.name.Contains("Social"))
                    {
                    CreateSocialBuilding(0);
                    }
                else if (currentPlaceableObject.name.Contains("Resouce"))
                    {
                    CreateResouceBuilding(0);
                    }
                }
            }
        public void CreateResouceBuilding(int couplingPosition)
            {
            if (couplingPosition == 0)
                {
                GameObject newBuilding = GetMainBuildingName();
                BuildingComponentTypAdder.AddBuildingTyp(newBuilding);
                newBuilding.GetComponent<ResourceBuildingsManagment>().GameobjectPrefab = placeableObjectPrefabs[lastButtonHit];
                newBuilding.transform.parent = GetLocalMesh().transform;
                ResourceBuildingsManagment.ResourceBuildingMain.Add(newBuilding);
                currentPlaceableObject.layer = LayerClass.ResourceBuildings;
                Destroy(currentPlaceableObject.GetComponent<LineRenderer>());
                currentPlaceableObject = null;
                }
            else if (!CreatingBuilding)
                {
                CreatingBuilding = true;
                var generalUi = GeneralUserInterfaceManagment.CurrentOnClickGameObject;
                var newGameobject = Instantiate(generalUi.transform.parent.GetComponent<ResourceBuildingsManagment>().GameobjectPrefab, generalUi.transform.parent.transform);
                AddBuildingToOtherBuilding(couplingPosition, LayerClass.ResourceBuildings, generalUi, newGameobject);
                CreatingBuilding = false;
                }
            }


        public void CreateSocialBuilding(int couplingPosition)
            {
            if (couplingPosition == 0)
                {
                GameObject newBuilding = GetMainBuildingName();
                BuildingComponentTypAdder.AddBuildingTyp(newBuilding);
                newBuilding.GetComponent<SocialBuildingManagment>().GameobjectPrefab = placeableObjectPrefabs[lastButtonHit];
                newBuilding.transform.parent = GetLocalMesh().transform;
                SocialBuildingManagment.SocialBuildingMain.Add(newBuilding);
                currentPlaceableObject.layer = LayerClass.SocialBuildings;
                Destroy(currentPlaceableObject.GetComponent<LineRenderer>());
                currentPlaceableObject = null;
                }
            else if (!CreatingBuilding)
                {
                CreatingBuilding = true;
                var generalUi = GeneralUserInterfaceManagment.CurrentOnClickGameObject;
                var newGameobject = Instantiate(generalUi.transform.parent.GetComponent<SocialBuildingManagment>().GameobjectPrefab, generalUi.transform.parent.transform);
                AddBuildingToOtherBuilding(couplingPosition, LayerClass.SocialBuildings, generalUi, newGameobject);
                CreatingBuilding = false;
                }
            }
        private GameObject GetMainBuildingName()
            {
            string[] buildingName = currentPlaceableObject.name.Split('-');
            GameObject newBuilding = new GameObject
                {
                name = buildingName[0] + "BuildingMain)-" + buildingName[1]
                };
            currentPlaceableObject.transform.parent = newBuilding.transform;
            return newBuilding;
            }
        private void AddBuildingToOtherBuilding(int couplingPosition, int buildingTyp, GameObject selectedGameobject, GameObject newGameobject)
            {
            AddBuildingTypInfos(newGameobject, buildingTyp);
            Vector3 newPosition = GetChildBuildingPosition.GetPosition(couplingPosition, selectedGameobject);
            if (newPosition != selectedGameobject.transform.position)
                {
                newGameobject.transform.position = newPosition;
                }
            else
                {
                Destroy(newGameobject);
                Debug.Log("Position taken");
                }
            }
        private void AddBuildingTypInfos(GameObject newGameobject, int buildingTyp)
            {
            newGameobject.layer = buildingTyp;
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
                if (collider.gameObject.layer == LayerClass.SocialBuildings || collider.gameObject.layer == LayerClass.ResourceBuildings || collider.gameObject.layer == LayerClass.ResourcePatch)
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
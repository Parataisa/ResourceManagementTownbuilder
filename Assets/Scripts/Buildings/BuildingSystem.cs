using Assets.Scripts.Buildings.BuildingSystemHelper;
using Assets.Scripts.Buildings.ResourceBuildings;
using Assets.Scripts.Ui.Menus.InfoUI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Buildings
    {
    public class BuildingSystem : MonoBehaviour
        {
        public static Dictionary<int, string> BuildingDirectory { get; private set; } = new Dictionary<int, string>();
        public GameObject CurrentPlaceableObject { get; private set; }
        public GameObject[] PlaceableObjectPrefabs { get; private set; }

        private Object[] BuildingsListObjects;
        private bool objectPlacable;
        private static int lastButtonHit;
        private bool CreatingBuilding = false;

        private void Start()
            {
            BuildingsListObjects = Resources.LoadAll("GameObjects/Buildings", typeof(GameObject));
            PlaceableObjectPrefabs = new GameObject[BuildingsListObjects.Length];
            GetBuildingsInPlacableObjects(BuildingsListObjects, PlaceableObjectPrefabs);
            }
        private void Update()
            {
            if (CurrentPlaceableObject != null && !EventSystem.current.IsPointerOverGameObject())
                {
                DrawGatheringCircle.DrawCircle(CurrentPlaceableObject, ResourceBuildingAccountant.ResourceCollectingRadius);
                MoveCurrentObjectToMouse();
                CreateBuildingIfClicked();
                }
            }
        private void GetBuildingsInPlacableObjects(Object[] buildingArray, GameObject[] placeableObject)
            {
            if (BuildingDirectory.Count == 0)
                {
                UpdatePlaceableObjects(buildingArray, placeableObject);
                }
            else
                {
                BuildingDirectory.Clear();
                UpdatePlaceableObjects(buildingArray, placeableObject);
                }
            }
        private static void UpdatePlaceableObjects(Object[] buildingArray, GameObject[] placeableObject)
            {
            int i = 0;
            foreach (var resouceBuilding in buildingArray)
                {
                BuildingDirectory.Add(i, resouceBuilding.name);
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
            if (CurrentPlaceableObject != null)
                {
                Destroy(CurrentPlaceableObject);
                CurrentPlaceableObject = null;
                return;
                }
            lastButtonHit = buildingId;
            CurrentPlaceableObject = Instantiate(PlaceableObjectPrefabs[buildingId]);
            }
        public void ClearCurser()
            {
            Destroy(CurrentPlaceableObject);
            CurrentPlaceableObject = null;
            }
        private GameObject MoveCurrentObjectToMouse()
            {
            float gameObjectSizeOffsetY = CurrentPlaceableObject.transform.localScale.y / 2;
            GameObject[] hitObject;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
                {
                CurrentPlaceableObject.transform.position = hitInfo.point;
                if (CurrentPlaceableObject.transform.position.y != gameObjectSizeOffsetY)
                    {
                    CurrentPlaceableObject.transform.position = new Vector3(hitInfo.point.x, gameObjectSizeOffsetY, hitInfo.point.z);
                    }
                hitObject = ScannForObjectsInArea(hitInfo, 4);
                if (hitObject[0] == null)
                    {
                    objectPlacable = true;
                    BuildingColoringSystem.ResetBuildingToOrigionColor(CurrentPlaceableObject);
                    }
                else
                    {
                    BoxCollider currentPlacableObjectCollider = CurrentPlaceableObject.GetComponent<BoxCollider>();
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
                            BuildingColoringSystem.SetColorForCollisions(CurrentPlaceableObject, Color.magenta);
                            return hitobjectCollider.gameObject;
                            }
                        else
                            {
                            objectPlacable = true;
                            BuildingColoringSystem.ResetBuildingToOrigionColor(CurrentPlaceableObject);
                            }
                        }
                    }
                }
            return CurrentPlaceableObject;
            }
        private void CreateBuildingIfClicked()
            {
            if (Input.GetMouseButtonDown(0) && objectPlacable)
                {
                CreateBuilding(0);
                }
            }
        public void CreateBuilding(int couplingPosition)
            {
            if (couplingPosition == 0)
                {
                GameObject newBuilding = GetMainBuildingName();
                BuildingComponentTypAdder.AddBuildingTyp(newBuilding);
                newBuilding.GetComponent<IBuildingManagment>().GameobjectPrefab = PlaceableObjectPrefabs[lastButtonHit];
                newBuilding.transform.parent = GetLocalMesh().transform;
                MainBuildingList.BuildingMain.Add(newBuilding);
                CurrentPlaceableObject.layer = GetLayer(CurrentPlaceableObject);
                Destroy(CurrentPlaceableObject.GetComponent<LineRenderer>());
                CurrentPlaceableObject = null;
                }
            else if (!CreatingBuilding)
                {
                CreatingBuilding = true;
                var generalUi = UserInterfaceManagment.CurrentSelectedGameObject;
                var newGameobject = Instantiate(generalUi.transform.parent.GetComponent<IBuildingManagment>().GameobjectPrefab, generalUi.transform.parent.transform);
                AddBuildingToOtherBuilding(couplingPosition, generalUi.layer, generalUi, newGameobject);
                CreatingBuilding = false;
                }
            }

        private int GetLayer(GameObject building)
            {
            if (building.name.Contains("Social"))
                {
                return LayerClass.SocialBuildings;
                }
            else if (building.name.Contains("Resource"))
                {
                return LayerClass.ResourceBuildings;
                }
            else if (building.name.Contains("Storage"))
                {
                return LayerClass.StorageBuildings;
                }
            return 0;
            }

        private GameObject GetMainBuildingName()
            {
            string[] buildingName = CurrentPlaceableObject.name.Split('-');
            GameObject newBuilding = new GameObject
                {
                name = buildingName[0] + "BuildingMain)-" + buildingName[1]
                };
            CurrentPlaceableObject.transform.parent = newBuilding.transform;
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
                if (LayerClass.GetSolitObjectLayer().Contains(collider.gameObject.layer))
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
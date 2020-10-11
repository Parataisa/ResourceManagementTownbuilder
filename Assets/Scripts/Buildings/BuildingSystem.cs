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
        private UnityEngine.Object[] ResouceBuildingsListObjects;
        private UnityEngine.Object[] SocialBuildingsListObjects;
        private bool objectPlacable;
        private static int lastButtonHit;
        private bool CreatingBuilding = false;

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
                DrawGatheringCircle.DrawCircle(currentPlaceableObject, ResourceBuildingAccountant.ResourceCollectingRadius);
                MoveCurrentObjectToMouse();
                CreateBuildingIfClicked();
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
                string[] buildingName = currentPlaceableObject.name.Split('-');
                GameObject resouceBuildingMain = new GameObject
                    {
                    name = "(ResouceBuildingMain)-" + buildingName[1]
                    };
                currentPlaceableObject.transform.parent = resouceBuildingMain.transform;
                resouceBuildingMain.AddComponent<ResourceBuildingAccountant>();
                resouceBuildingMain.AddComponent<ResourceBuildingsManagment>();
                resouceBuildingMain.GetComponent<ResourceBuildingsManagment>().GameobjectPrefab = placeableObjectPrefabs[lastButtonHit];
                resouceBuildingMain.transform.parent = GetLocalMesh().transform;
                ResourceBuildingsManagment.ResourceBuildingMain.Add(resouceBuildingMain);
                currentPlaceableObject.layer = 9;
                Destroy(currentPlaceableObject.GetComponent<LineRenderer>());
                currentPlaceableObject = null;
                }
            else if (!CreatingBuilding)
                {
                CreatingBuilding = true;
                var generalUi = FindObjectOfType<GeneralUserInterfaceManagment>();
                var newGameobject = Instantiate(generalUi.CurrentOnClickGameObject.transform.parent.GetComponent<ResourceBuildingsManagment>().GameobjectPrefab, generalUi.CurrentOnClickGameObject.transform.parent.transform);
                AddBuildingToOtherBuilding(couplingPosition, 9, generalUi.CurrentOnClickGameObject, newGameobject);
                CreatingBuilding = false;
                }
            }
        public void CreateSocialBuilding(int couplingPosition)
            {
            if (couplingPosition == 0)
                {
                string[] buildingName = currentPlaceableObject.name.Split('-');
                GameObject socilaBuildingMain = new GameObject
                    {
                    name = "(SocialBuildingMain)-" + buildingName[1]
                    };
                currentPlaceableObject.transform.parent = socilaBuildingMain.transform;
                socilaBuildingMain.AddComponent<SocialBuildingManagment>();
                socilaBuildingMain.GetComponent<SocialBuildingManagment>().GameobjectPrefab = placeableObjectPrefabs[lastButtonHit];
                socilaBuildingMain.transform.parent = GetLocalMesh().transform;
                SocialBuildingManagment.SocialBuildingMain.Add(socilaBuildingMain);
                currentPlaceableObject.layer = 8;
                Destroy(currentPlaceableObject.GetComponent<LineRenderer>());
                currentPlaceableObject = null;
                }
            else if (!CreatingBuilding)
                {
                CreatingBuilding = true;
                var generalUi = FindObjectOfType<GeneralUserInterfaceManagment>();
                var newGameobject = Instantiate(generalUi.CurrentOnClickGameObject.transform.parent.GetComponent<SocialBuildingManagment>().GameobjectPrefab, generalUi.CurrentOnClickGameObject.transform.parent.transform);
                AddBuildingToOtherBuilding(couplingPosition, 8, generalUi.CurrentOnClickGameObject, newGameobject);
                CreatingBuilding = false;
                }
            }
        private void AddBuildingToOtherBuilding(int couplingPosition, int buildingTyp, GameObject selectedGameobject, GameObject newGameobject)
            {
            AddBuildingTypInfos(newGameobject, buildingTyp);
            Vector3 newPosition = GetBuildingPosition(couplingPosition, selectedGameobject);
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

        private Vector3 GetBuildingPosition(int couplingPosition, GameObject selectedGameobject)
            {
            //North
            if (couplingPosition == 1)
                {
                Vector3 newPosition = new Vector3(selectedGameobject.transform.position.x, selectedGameobject.transform.position.y, selectedGameobject.transform.position.z + selectedGameobject.transform.lossyScale.z);
                if (HaveNeightbourAtPosition(newPosition, selectedGameobject.transform.parent.GetComponent<IBuildingManagment>().ListOfChildren))
                    {
                    return selectedGameobject.transform.position;
                    }
                return newPosition;
                }
            //East
            else if (couplingPosition == 2)
                {
                Vector3 newPosition = new Vector3(selectedGameobject.transform.position.x + selectedGameobject.transform.lossyScale.x, selectedGameobject.transform.position.y, selectedGameobject.transform.position.z);
                if (HaveNeightbourAtPosition(newPosition, selectedGameobject.transform.parent.GetComponent<IBuildingManagment>().ListOfChildren))
                    {
                    return selectedGameobject.transform.position;
                    }
                return newPosition;
                }
            //South
            else if (couplingPosition == 3)
                {
                Vector3 newPosition = new Vector3(selectedGameobject.transform.position.x, selectedGameobject.transform.position.y, selectedGameobject.transform.position.z - selectedGameobject.transform.lossyScale.z);
                if (HaveNeightbourAtPosition(newPosition, selectedGameobject.transform.parent.GetComponent<IBuildingManagment>().ListOfChildren))
                    {
                    return selectedGameobject.transform.position;
                    }
                return newPosition;
                }
            //West
            else if (couplingPosition == 4)
                {
                Vector3 newPosition = new Vector3(selectedGameobject.transform.position.x - selectedGameobject.transform.lossyScale.x, selectedGameobject.transform.position.y, selectedGameobject.transform.position.z);
                if (HaveNeightbourAtPosition(newPosition, selectedGameobject.transform.parent.GetComponent<IBuildingManagment>().ListOfChildren))
                    {
                    return selectedGameobject.transform.position;
                    }
                return newPosition;
                }
            return selectedGameobject.transform.position;
            }
        private bool HaveNeightbourAtPosition(Vector3 newPosition, List<GameObject> neighbourPosititions)
            {
            foreach (var neighbour in neighbourPosititions)
                {
                if (neighbour.transform.position == newPosition)
                    {
                    return true;
                    }
                }
            return false;
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
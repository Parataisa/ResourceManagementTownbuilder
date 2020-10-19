using Assets.Scripts.Buildings.BuildingSystemHelper;
using ResourceGeneration.ResourceVariationen;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Buildings.ResourceBuildings
    {
    class ResourceBuildingAccountant : MonoBehaviour
        {
        public List<GameObject> GatherableResouceInArea { get; private set; } = new List<GameObject>();
        public static float ResourceCollectingRadius {get => 10;}
        public int SelecedResource { get; set; }

        private List<string> ListOfGatherableResources;
        private List<ResouceQuantityTyps> GatherableResouces;

        public class ResouceQuantityTyps
            {
            public int resouceQuantity;
            public string resouceName;
            }
        public void Start()
            {
            ListOfGatherableResources = GetComponentInChildren<IResourcBuilding>().ResourceToGather;
            GatherableResouceInArea = GatherableResouceInArea.Count == 0 ? GetUsableResources(ListOfGatherableResources) : GatherableResouceInArea;
            GatherableResouces = GetResourceQuantityInArea(GatherableResouceInArea);
            if (!GatherableResouces.Count.Equals(0))
                {
                this.GetComponent<ResourceBuildingsManagment>().UpdateResouces += UpdateResouces;
                }
            }

        private void UpdateResouces(GameObject mainBuilding)
            {
            if (GatherableResouceInArea.Count == 0 || GatherableResouceInArea[SelecedResource] == null)
                {
                return;
                }
            var selectedResouceInArea = mainBuilding.GetComponent<ResourceBuildingAccountant>().GatherableResouceInArea[SelecedResource].GetComponents<IResources>();
            if (selectedResouceInArea.Length == 1)
                {
                if (selectedResouceInArea[0].QuantityOfTheResource <= 0)
                    {
                    GatherableResouceInArea.RemoveAt(SelecedResource);
                    mainBuilding.GetComponent<ResourceBuildingsManagment>().UpdateResouces -= UpdateResouces;
                    mainBuilding.GetComponent<ResourceBuildingsManagment>().StopAllCoroutines();
                    mainBuilding.GetComponent<ResourceBuildingsManagment>().CoroutinRunning = false;

                    }
                else if (mainBuilding.GetComponent<ResourceBuildingsManagment>().WorkingPeople > 0)
                    {
                    selectedResouceInArea[0].QuantityOfTheResource -= 1;
                    selectedResouceInArea[0].ResourceQuantityCheck();
                    mainBuilding.GetComponent<ResourceBuildingsManagment>().GatheredResourcesOverall += 1;
                    mainBuilding.GetComponent<ResourceBuildingsManagment>().IncreaseGatherResource(1, selectedResouceInArea[0]);
                    }
                }
            else
                {
                foreach (var Resource in selectedResouceInArea)
                    {
                    if (mainBuilding.transform.GetChild(0).GetComponent<IResourcBuilding>().ResourceToGather.Contains(Resource.ResourceName))
                        {
                        if (Resource.QuantityOfTheResource <= 0)
                            {
                            GatherableResouceInArea.RemoveAt(SelecedResource);
                            mainBuilding.GetComponent<ResourceBuildingsManagment>().UpdateResouces -= UpdateResouces;
                            mainBuilding.GetComponent<ResourceBuildingsManagment>().StopAllCoroutines();
                            mainBuilding.GetComponent<ResourceBuildingsManagment>().CoroutinRunning = false;
                            }
                        else if (mainBuilding.GetComponent<ResourceBuildingsManagment>().WorkingPeople > 0)
                            {
                            Resource.QuantityOfTheResource -= 1;
                            Resource.ResourceQuantityCheck();
                            mainBuilding.GetComponent<ResourceBuildingsManagment>().GatheredResourcesOverall += 1;
                            mainBuilding.GetComponent<ResourceBuildingsManagment>().IncreaseGatherResource(1, Resource);
                            }
                        }
                    }
                }
            }

        public void SetSelecedResource(int x)
            {
            this.SelecedResource = x;
            }

        private List<ResouceQuantityTyps> GetResourceQuantityInArea(List<GameObject> gatherableResouceInArea)
            {
            List<ResouceQuantityTyps> resouceQuantityInArea = new List<ResouceQuantityTyps>();
            foreach (var resoucePatch in gatherableResouceInArea)
                {
                var resouceScript = resoucePatch.GetComponent<IResources>();
                ResouceQuantityTyps resouceQuantityTyps = new ResouceQuantityTyps
                    {
                    resouceName = resouceScript.ResourceName
                    };
                if (resouceQuantityInArea.Count == 0)
                    {
                    resouceQuantityTyps.resouceQuantity = resouceScript.QuantityOfTheResource;
                    resouceQuantityInArea.Add(resouceQuantityTyps);
                    }
                else
                    {
                    int x = 0;
                    foreach (var resouceInList in resouceQuantityInArea)
                        {
                        if (resouceInList.resouceName.Contains(resouceQuantityTyps.resouceName))
                            {
                            resouceInList.resouceQuantity += resouceScript.QuantityOfTheResource;
                            break;
                            }
                        else if (!resouceInList.resouceName.Contains(resouceQuantityTyps.resouceName) && !(x < resouceQuantityInArea.Count))
                            {
                            x++;
                            continue;
                            }
                        else
                            {
                            resouceQuantityTyps.resouceQuantity = resouceScript.QuantityOfTheResource;
                            resouceQuantityInArea.Add(resouceQuantityTyps);
                            break;
                            }
                        }
                    }
                }
            return resouceQuantityInArea;
            }
            

        public List<GameObject> GetUsableResources(List<string> resources)
            {
            List<GameObject> usableResources = new List<GameObject>();
            GameObject[] resourceInArea = ScannForResources(ResourceCollectingRadius, this.transform.GetChild(0).transform.position, 100);
            if (resourceInArea[0] == null)
                {
                return usableResources;
                }
            foreach (GameObject savedResource in resourceInArea)
                {
                if (savedResource == null)
                    {
                    return usableResources;
                    }
                var ResourceOnGameobject = savedResource.GetComponents<IResources>();
                foreach (var Resource in ResourceOnGameobject)
                    {
                    if (resources.Contains(Resource.ResourceName))
                        {
                        usableResources.Add(savedResource);
                        }
                    }
                }
            return usableResources;
            }
        private GameObject[] ScannForResources(float radius, Vector3 startpoint, int maxScannedResources)
            {
            Collider[] collidersInArea = new Collider[maxScannedResources];
            int collisions = Physics.OverlapSphereNonAlloc(startpoint, radius, collidersInArea);
            GameObject[] gameObjectArray = new GameObject[maxScannedResources];
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
                if (collider.gameObject.layer == LayerClass.ResourcePatch)
                    {
                    GameObject parent = collider.transform.parent.gameObject;
                    if (gameObjectArray.Contains(parent))
                        continue;
                    else
                        {
                        if (i > maxScannedResources)
                            {
                            return gameObjectArray;
                            }
                        gameObjectArray[i] = parent;
                        i++;
                        }
                    }
                }
            return gameObjectArray;
            }
        }
    }


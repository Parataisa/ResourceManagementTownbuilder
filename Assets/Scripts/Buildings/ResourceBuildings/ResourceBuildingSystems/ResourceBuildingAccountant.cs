using ResourceGeneration.ResourceVariationen;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Buildings.ResourceBuildings
    {
    class ResourceBuildingAccountant : MonoBehaviour
        {
        public List<GameObject> GatherableResouceInArea = new List<GameObject>();
        public const float ResourceCollectingRadius = 10;
        public int selecedResource = 0;
        private List<string> ListOfGatherableResources;
        private List<ResouceQuantityTyps> GatherableResouces;

        public class ResouceQuantityTyps
            {
            public GameObject resoucePatch;
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
            if (GatherableResouceInArea.Count == 0 || GatherableResouceInArea[selecedResource] == null)
                {
                return;
                }
            var selectedResouceInArea = mainBuilding.GetComponent<ResourceBuildingAccountant>().GatherableResouceInArea[selecedResource].GetComponent<ResourceBase>();
            if (selectedResouceInArea.QuantityOfTheResource <= 0)
                {
                GatherableResouceInArea.RemoveAt(selecedResource);
                mainBuilding.GetComponent<ResourceBuildingsManagment>().UpdateResouces -= UpdateResouces;
                }
            else
                {
                selectedResouceInArea.QuantityOfTheResource -= 1;
                selectedResouceInArea.ResourceQuantityCheck();
                mainBuilding.GetComponent<ResourceBuildingsManagment>().GatheredResourcesOverall += 1;
                mainBuilding.GetComponent<ResourceBuildingsManagment>().IncreaseGatherResource(1, selectedResouceInArea);
                }
            }

        public void SetSelecedResource(int x)
            {
            this.selecedResource = x;
            }

        private List<ResouceQuantityTyps> GetResourceQuantityInArea(List<GameObject> gatherableResouceInArea)
            {
            List<ResouceQuantityTyps> resouceQuantityInArea = new List<ResouceQuantityTyps>();
            foreach (var resoucePatch in gatherableResouceInArea)
                {
                var resouceScript = resoucePatch.GetComponent<ResourceBase>();
                ResouceQuantityTyps resouceQuantityTyps = new ResouceQuantityTyps
                    {
                    resouceName = resouceScript.ResourceName
                    };
                if (resouceQuantityInArea.Count == 0)
                    {
                    resouceQuantityTyps.resouceQuantity = resouceScript.QuantityOfTheResource;
                    resouceQuantityTyps.resoucePatch = resouceScript.gameObject;
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
                            resouceQuantityTyps.resoucePatch = resouceScript.gameObject;
                            resouceQuantityInArea.Add(resouceQuantityTyps);
                            break;
                            }
                        }
                    }
                }
            return resouceQuantityInArea;
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
                if (collider.gameObject.layer == 10)
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
                else if (resources.Contains(savedResource.GetComponent<ResourceBase>().ResourceName))
                    {
                    usableResources.Add(savedResource);
                    }
                }
            return usableResources;
            }
        }
    }


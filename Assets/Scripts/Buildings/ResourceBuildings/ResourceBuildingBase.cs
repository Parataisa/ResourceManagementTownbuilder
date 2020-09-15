using ResourceGeneration.ResourceVariationen;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Buildings.ResourceBuildings
    {
    class ResourceBuildingBase : MonoBehaviour
        {
        public List<ResouceQuantityTyps> GatherableResouces;
        private List<string> ListOfGatherableResources;
        private float ResourceCollectingRadius = 20;
        public Vector3 BuildingPosition;
        public List<GameObject> GatherableResouceInArea;


        public class ResouceQuantityTyps
            {
            public GameObject resoucePatch;
            public int resouceQuantity;
            public string resouceName;
            }
        public void Start()
            {
            BuildingPosition = GetComponent<Transform>().position;
            ListOfGatherableResources = GetComponent<IResourcBuilding>().ResouceToGather;
            GatherableResouceInArea = GetUsableResources(ListOfGatherableResources);
            GatherableResouces = GetResouceQuantityInArea(GatherableResouceInArea);
            if (!GatherableResouces.Count.Equals(0))
                {
                FindObjectOfType<ResouceBuildingsManagment>().UpdateResouces += UpdateResouces;
                }
            }

        private void UpdateResouces(GameObject childBuilding)
            {
            var selectedResouceInArea = GetComponent<ResourceBuildingBase>().GatherableResouceInArea[0].GetComponent<ResourceBase>();
            if (selectedResouceInArea.quantityOfTheResource <= 0)
                {
                FindObjectOfType<ResouceBuildingsManagment>().UpdateResouces -= UpdateResouces;
                }
            else
                {
                selectedResouceInArea.quantityOfTheResource -= 1;
                childBuilding.transform.parent.GetComponent<ResouceBuildingsManagment>().GatheredResourcesOverall += 1;
                childBuilding.transform.parent.GetComponent<ResouceBuildingsManagment>().StortedResources += 1;
                }
            }

        private List<ResouceQuantityTyps> GetResouceQuantityInArea(List<GameObject> gatherableResouceInArea)
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
                    resouceQuantityTyps.resouceQuantity = resouceScript.quantityOfTheResource;
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
                            resouceInList.resouceQuantity += resouceScript.quantityOfTheResource;
                            break;
                            }
                        else if (!resouceInList.resouceName.Contains(resouceQuantityTyps.resouceName) && !(x < resouceQuantityInArea.Count))
                            {
                            x++;
                            continue;
                            }
                        else
                            {
                            resouceQuantityTyps.resouceQuantity = resouceScript.quantityOfTheResource;
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
            GameObject[] resourceInArea = ScannForResources(ResourceCollectingRadius, BuildingPosition, 100);
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


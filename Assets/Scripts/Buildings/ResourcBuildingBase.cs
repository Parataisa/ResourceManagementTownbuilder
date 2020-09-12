using Assets.Scripts.Buildings.ResourceBuildings;
using ResourceGeneration.ResourceVariationen;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Buildings
    {
    class ResourcBuildingBase : MonoBehaviour
        {
        public int GatheredResourcesOverall;
        public List<ResouceQuantityTyps> GatherableResouces;
        public int StortedResources;
        public float ProduktionSpeed;
        public int WorkingPeople;
        private float ResourceCollectingRadius = 40;
        public Vector3 BuildingPosition;
        private List<string> ListOfGatherableResources;
        public List<GameObject> GatherableResouceInArea;

        public class ResouceQuantityTyps
            {
            public int resouceQuantity;
            public string resouceName;
            }
        public void Start()
            {
            BuildingPosition = GetComponent<Transform>().position;
            ListOfGatherableResources = GetComponent<IResourcBuilding>().ResouceToGather;
            GatherableResouceInArea = GetUsableResources(ListOfGatherableResources);
            GatherableResouces = GetResouceQuantityInArea(GatherableResouceInArea);
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


using Assets.Scripts.Buildings.ResourceBuildings;
using ResourceGeneration.ResourceVariationen;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Buildings
    {
    class ResourcBuildingBase : MonoBehaviour
        {
        public int GatheredResourcesOverall;
        public int StortedResources;
        public float ProduktionSpeed;
        public int WorkingPeople;
        public float ResourceCollectingRadius = 20;
        public Vector3 BuildingPosition;
        private List<string> ListOfGatherableResources;
        public List<GameObject> GatherableResouceInArea;

        private void Start()
            {
            BuildingPosition = GetComponent<Transform>().position;
            ListOfGatherableResources = GetComponent<IResourcBuilding>().ResouceToGather;
            GatherableResouceInArea = GetUsableResources(ListOfGatherableResources);
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


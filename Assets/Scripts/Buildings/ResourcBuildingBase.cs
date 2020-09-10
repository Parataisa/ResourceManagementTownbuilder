using ResourceGeneration.ResourceVariationen;
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
        public List<GameObject> GatherableResources;

        private void Start()
            {
            BuildingPosition = GetComponent<Transform>().position;
            List<string> test = new List<string>();
            test.Add("Coal");
            GatherableResources = GetUsableResources(test, ScannForResources(ResourceCollectingRadius, BuildingPosition, 100));

            }

        public GameObject[] ScannForResources(float radius, Vector3 startpoint, int maxScannedResources)
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
                if (collider.gameObject.layer == 9)
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
        private List<GameObject> GetUsableResources(List<string> resources, GameObject[] resourcesInArea)
            {
            List<GameObject> usableResources = new List<GameObject>();
            if (resourcesInArea[0] == null)
                {
                return usableResources;
                }
            foreach (GameObject savedResource in resourcesInArea)
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


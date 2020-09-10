using Assets.Scripts.TerrainGeneration.RecourceGeneration;
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

        private void Start()
            {
            BuildingPosition = GetComponent<Transform>().position;
            GameObject[] resourceInArea = ScannForResources(ResourceCollectingRadius, BuildingPosition, 10);

            }

        public GameObject[] ScannForResources(float radius, Vector3 startpoint, int maxScannedResources)
            {
            Collider[] collidersInArea = new Collider[maxScannedResources];
            int collisions = Physics.OverlapSphereNonAlloc(startpoint, radius, collidersInArea);
            GameObject[] gameObjectArray = new GameObject[maxScannedResources];
            if (collisions == 0)
                {
                return gameObjectArray;
                }
            int i = 0;
            foreach (Collider collider in collidersInArea)
                {
                GameObject parent = collider.transform.parent.gameObject;
                if (gameObjectArray.Contains(parent))
                    break;
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
            return gameObjectArray;
            }
        }
    }


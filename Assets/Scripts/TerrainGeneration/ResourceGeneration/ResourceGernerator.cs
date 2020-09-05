using Assets.Scripts.TerrainGeneration.ResourceGeneration.ResourceVariationen;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.TerrainGeneration.RecourceGeneration
    {
    class ResourceGernerator : MonoBehaviour
        {
        public static List<GameObject> ResourcePrefabs = new List<GameObject>();
        public int NumberOfResources;
        public Mesh terrainMesh;
        public void Start()
            {
            NumberOfResources = UnityEngine.Random.Range(1, 10);
            UnityEngine.Object[] subListObjects = Resources.LoadAll("ResourceVariationen", typeof(GameObject));
            foreach (GameObject gameObject in subListObjects)
                {
                GameObject lo = (GameObject)gameObject;
                ResourcePrefabs.Add(lo);
                }
            for (int i = 0; i <= NumberOfResources; i++)
                {
                int randomResouce = UnityEngine.Random.Range(0, ResourcePrefabs.Count);
                ChooseResouceToGenerate(ResourcePrefabs[randomResouce]); 
                }


            }

        private void ChooseResouceToGenerate(GameObject resourceType)
            {
            var resourceToSpawn = Instantiate<GameObject>(resourceType);
            var scriptOfTheResource = resourceToSpawn.GetComponent<ResourceBase>();
            FindObjectOfType<ResourceBase>(resourceToSpawn).ResourceGenerated2 += Test;
            FindObjectOfType<ResourceBase>(scriptOfTheResource).ResourceGenerated += generatResources;
            }

        private void Test(GameObject resourceToSpawn)
            {
            resourceToSpawn.GetComponent<Transform>().transform.position = resourceToSpawn.GetComponent<ResourceBase>().positionOnTheMap;
            }

        private void generatResources(ResourceBase scriptOfTheResource)
            {
            int size = scriptOfTheResource.sizeOfTheResource - 2;
            GameObject child = scriptOfTheResource.transform.GetChild(0).gameObject;
            for (int i = 0; i <= size; i++)
                {
                GameObject _ = Instantiate<GameObject>(child, scriptOfTheResource.transform);
                Vector3 vector3 = new Vector3(child.transform.localPosition.x + i + 1, 0.5f, child.transform.localPosition.z + i + 1);
                _.GetComponent<Transform>().localPosition = vector3;
                   }
                }
        
        }
    }

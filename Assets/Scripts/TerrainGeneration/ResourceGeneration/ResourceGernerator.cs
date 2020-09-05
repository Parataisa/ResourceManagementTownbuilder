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
            NumberOfResources = UnityEngine.Random.Range(1, 20);
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
            scriptOfTheResource.GetComponent<ResourceBase>().sizeOfTheModel = resourceType.gameObject.transform.localScale;
            FindObjectOfType<ResourceBase>(resourceToSpawn).ResourceGenerated2 += SetLocationOfTheResouurce;
            FindObjectOfType<ResourceBase>(scriptOfTheResource).ResourceGenerated += GeneratResources;
            }

        private void SetLocationOfTheResouurce(GameObject resourceToSpawn)
            {
            resourceToSpawn.GetComponent<Transform>().transform.position = resourceToSpawn.GetComponent<ResourceBase>().positionOnTheMap;
            }

        private void GeneratResources(ResourceBase scriptOfTheResource)
            {
            int size = scriptOfTheResource.sizeOfTheResource;
            Vector2 area = scriptOfTheResource.areaOfTheResource;
            Vector3 sizeOfTheModel = RandomSizeSelector(); ;
            scriptOfTheResource.sizeOfTheModel = sizeOfTheModel;
            GameObject child = scriptOfTheResource.transform.GetChild(0).gameObject;
            for (int i = 0; i < size; i++)
                {
                GameObject _ = Instantiate<GameObject>(child, scriptOfTheResource.transform);
                var localYTransform = _.GetComponent<Transform>().localScale = scriptOfTheResource.sizeOfTheModel;
                Vector2 localPositon = GetlocalPositon(area);
                Vector3 vector3 = new Vector3(localPositon.x, localYTransform.y / 2, localPositon.y);
                _.GetComponent<Transform>().localPosition = vector3;
                   }
            Destroy(child);
                }

        private Vector2 GetlocalPositon(Vector2 area)
            {
            Vector2 position;
            position.x = UnityEngine.Random.Range(0, area.x);
            position.y = UnityEngine.Random.Range(0, area.y);
            return position;
            }

        private Vector3 RandomSizeSelector()
            {
            Vector3 size;
            size.x = UnityEngine.Random.Range(0.5f, 4f);
            size.y = UnityEngine.Random.Range(0.5f, 2f);
            size.z = UnityEngine.Random.Range(0.5f, 4f);
            return size;
            }
        }
    }

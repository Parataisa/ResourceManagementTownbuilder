using Assets.Scripts.TerrainGeneration.ResourceGeneration.ResourceVariationen;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.TerrainGeneration.RecourceGeneration
    {
    class ResourceGernerator : MonoBehaviour
        {
        public static List<GameObject> ResourcePrefabs = new List<GameObject>();
        public Mesh terrainMesh;
        private void Start()
            {
            UnityEngine.Object[] subListObjects = Resources.LoadAll("ResourceVariationen", typeof(GameObject));
            foreach (GameObject gameObject in subListObjects)
                {
                GameObject lo = (GameObject)gameObject;
                ResourcePrefabs.Add(lo);
                }
            ResourceBase testResourceBase = new ResourceBase();
            var testPos = testResourceBase.GetPositionOfResource();
            var testGameOb = Instantiate<GameObject>(ResourcePrefabs[0], testPos, Quaternion.Euler(0, 0, 0));
            FindObjectOfType<Coal>().ResourceGenerated += generatResources;


           

            }

        private void generatResources()
            {
            //Just for testing
            var testGameOb = FindObjectOfType<Coal>();
            int size = testGameOb.GetComponent<Coal>().sizeOfTheResource - 2;
            GameObject child = testGameOb.transform.GetChild(0).gameObject;
            for (int i = 0; i <= size; i++)
                {
                GameObject _ = Instantiate<GameObject>(child, testGameOb.transform);
                Vector3 vector3 = new Vector3(testGameOb.transform.position.x + i, 0.5f, testGameOb.transform.position.z + i);
                _.GetComponent<Transform>().localPosition = vector3;
                }
            }
        
        }
    }

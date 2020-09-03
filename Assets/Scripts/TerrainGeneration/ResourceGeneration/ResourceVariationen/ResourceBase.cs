using UnityEngine;

namespace Assets.Scripts.TerrainGeneration.ResourceGeneration.ResourceVariationen
    {
    public class ResourceBase
        {
        private int sizeOfTheResource;
        private int quantityOfTheResource;
        private string nameOfTheResource;
        public Vector3 positionOfThePatch;
        public GameObject ResourcePrefab;
        public Vector3 positionOnTheMap;


        //public ResourceBase(string nameOfTheResource)
        //    {
        //    this.nameOfTheResource = nameOfTheResource;
        //    this.quantityOfTheResource = GetQuantityOfTheResource();
        //    this.sizeOfTheResource = GetSizeOfTheResource();
        //    Vector3 positonOnTheMesh = GetPositionOfResource();
        //    this.positionOfThePatch = positonOnTheMesh;

        //    }
        public int GetQuantityOfTheResource()
            {
            int quantity = Random.Range(1, 600);

            return quantity;
            }
        public int GetSizeOfTheResource()
            {
            int size = Random.Range(1, 100);

            return size;
            }
        public static Vector3 GetPositionOfResource()
            {
            Vector3 position;
            position.x = Random.Range(0, 256);
            position.z = Random.Range(0, 256);
            position.y = 0;//ToDo Get height of the Prefab and add half of that value.
            return position;
            }


        }
    }

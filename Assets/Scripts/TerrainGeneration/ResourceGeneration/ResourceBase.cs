using System;
using UnityEngine;

namespace Assets.Scripts.TerrainGeneration.ResourceGeneration.ResourceVariationen
    {
    public class ResourceBase : MonoBehaviour
        {
        public int sizeOfTheResource;
        public int quantityOfTheResource;
        public Vector3 positionOnTheMap;
        public Vector2 areaOfTheResource;
        public Vector3 sizeOfTheModel;
        public event Action<ResourceBase> ResourceGenerated;
        public event Action<GameObject> ResourceGenerated2;


        protected virtual void Start()
            {
            this.quantityOfTheResource = GetQuantityOfTheResource();
            this.sizeOfTheResource = GetSizeOfTheResource(quantityOfTheResource);
            this.areaOfTheResource = GetAreaOfTheResource(sizeOfTheModel, sizeOfTheResource);
            this.positionOnTheMap = GetPositionOfResource(areaOfTheResource);
            ResourceGenerated?.Invoke(this);
            ResourceGenerated2?.Invoke(this.gameObject);
            }
        public static int GetQuantityOfTheResource()
            {
            int quantity = UnityEngine.Random.Range(40000, 60000);
            return quantity;
            }
        public static int GetSizeOfTheResource(int quantity)
            {
            int size = quantity / UnityEngine.Random.Range(600, 2000);
            return size;
            }
        public static Vector3 GetPositionOfResource(Vector2 area)
            {
            Vector3 position;
            position.x = GetPositionMinusArea(area.x); 
            position.z = GetPositionMinusArea(area.y); 
            position.y = 0;
            return position;
            }


        private static float GetPositionMinusArea(float area)
            {
            float x = UnityEngine.Random.Range(0, 256);
            if (x <= area) return x;
            else return x - area;
            }
        public static Vector2 GetAreaOfTheResource(Vector3 sizeModel, int size)
            {
            Vector2 area;
            area.x = UnityEngine.Random.Range(1, 1.2f) * sizeModel.x * size;
            area.y = UnityEngine.Random.Range(1, 1.2f) * sizeModel.z * size;
            return area;
            }
        }
    }

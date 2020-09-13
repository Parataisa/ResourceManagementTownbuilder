using System;
using UnityEngine;

namespace ResourceGeneration.ResourceVariationen
    {
    public class ResourceBase : MonoBehaviour
        {
        public string ResourceName;
        public int sizeOfTheResource;
        public int quantityOfTheResource;
        public int numberOfIterationen = 20;
        public Vector3 positionOnTheMap;
        public Vector2 areaOfTheResource;
        public Vector3 sizeOfTheModel;
        public event Action<ResourceBase, int> ResourceGenerated;
        public event Action<GameObject> ChooseLocationEvent;


        protected virtual void Start()
            {
            this.ResourceName = GetResourceName();
            this.quantityOfTheResource = GetQuantityOfTheResource();
            this.sizeOfTheResource = GetSizeOfTheResource(quantityOfTheResource);
            this.sizeOfTheModel = GetRandomSizeForTheModel(quantityOfTheResource, sizeOfTheResource);
            this.areaOfTheResource = GetAreaOfTheResource(sizeOfTheModel, sizeOfTheResource);
            this.positionOnTheMap = GetPositionOfResource(areaOfTheResource);
            ChooseLocationEvent?.Invoke(this.gameObject);
            ResourceGenerated?.Invoke(this, numberOfIterationen);
            }

        private string GetResourceName()
            {
            string resourceName = this.GetType().Name;
            return resourceName;
            }

        public static int GetQuantityOfTheResource()
            {
            int quantity = UnityEngine.Random.Range(50000, 80000);
            return quantity;
            }
        public static int GetSizeOfTheResource(int quantity)
            {
            int size = quantity / UnityEngine.Random.Range(1000, 4000);
            return size;
            }
        private Vector3 GetRandomSizeForTheModel(int quantity, int patchSize)
            {
            //ToDo: Inplement a better size system 
            Vector3 size;
            size.x = UnityEngine.Random.Range(0.95f, 1.05f) * quantity / patchSize / 1000;
            size.y = size.x; // UnityEngine.Random.Range(0.5f, 2f);
            size.z = size.x;  //UnityEngine.Random.Range(0.5f, 2.5f);
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
            float x = UnityEngine.Random.Range(area / 2, 256 - area / 2);
            return x;
            }
        public static Vector2 GetAreaOfTheResource(Vector3 sizeModel, int size)
            {
            Vector2 area;
            area.x = UnityEngine.Random.Range(0.8f, 1.1f) * sizeModel.x * size / 2f;
            area.y = UnityEngine.Random.Range(0.8f, 1.1f) * sizeModel.z * size / 2f;
            return area;
            }
        }
    }

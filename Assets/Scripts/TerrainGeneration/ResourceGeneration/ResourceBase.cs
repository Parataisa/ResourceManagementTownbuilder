using System;
using UnityEngine;

namespace ResourceGeneration.ResourceVariationen
    {
    public class ResourceBase : MonoBehaviour, IResources
        {
        public int SizeOfTheResource;
        private int quantityOfTheResource;
        private int startingQuantity;
        public Vector3 PositionOnTheMap;
        public Vector2 AreaOfTheResource;
        public Vector3 SizeOfTheModel;
        public event Action<GameObject> ChooseLocationEvent;
        public event Action<ResourceBase> ResourceGenerated;
        public int FractionValiesForThePatch;

        public string ResourceName => GetResourceName();
        public int QuantityOfTheResource { get => quantityOfTheResource; set => quantityOfTheResource = value; }

        protected virtual void Start()
            {
            quantityOfTheResource = GetQuantityOfTheResource();
            this.startingQuantity = quantityOfTheResource;
            this.SizeOfTheResource = GetSizeOfTheResource(quantityOfTheResource);
            this.SizeOfTheModel = GetRandomSizeForTheModel(quantityOfTheResource, SizeOfTheResource);
            this.AreaOfTheResource = GetAreaOfTheResource(SizeOfTheModel, SizeOfTheResource);
            this.PositionOnTheMap = GetPositionOfResource(AreaOfTheResource);
            this.FractionValiesForThePatch = quantityOfTheResource / this.SizeOfTheResource;
            ChooseLocationEvent?.Invoke(this.gameObject);
            ResourceGenerated?.Invoke(this);

            }
        public void ResourceQuantityCheck()
            {
            if (this.startingQuantity == quantityOfTheResource)
                {
                return;
                }
            else
                {
                if ((this.startingQuantity - quantityOfTheResource) % this.FractionValiesForThePatch == 0)
                    {
                    Destroy(this.transform.GetChild(0).gameObject);
                    this.SizeOfTheResource -= 1;
                    }
                else if (quantityOfTheResource <= 0)
                    {
                    Destroy(this.gameObject);
                    }
                else
                    {
                    return;
                    }
                }
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
            Vector3 size;
            size.x = UnityEngine.Random.Range(0.95f, 1.05f) * quantity / patchSize / 1000;
            size.y = size.x;
            size.z = size.x;
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

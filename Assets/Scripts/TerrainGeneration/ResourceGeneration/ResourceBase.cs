using System;
using UnityEngine;

namespace Assets.Scripts.TerrainGeneration.ResourceGeneration.ResourceVariationen
    {
    public class ResourceBase : MonoBehaviour
        {
        public int sizeOfTheResource;
        public int quantityOfTheResource;
        public Vector3 positionOnTheMap;
        public event Action<ResourceBase> ResourceGenerated;
        public event Action<GameObject> ResourceGenerated2;


        protected virtual void Start()
            {
            this.quantityOfTheResource = GetQuantityOfTheResource();
            this.sizeOfTheResource = GetSizeOfTheResource();
            this.positionOnTheMap = GetPositionOfResource();
            if (ResourceGenerated != null)
                {
                ResourceGenerated(this);
                }
            if (ResourceGenerated2 != null)
                {
                ResourceGenerated2(this.gameObject);
                }
            }
        public int GetQuantityOfTheResource()
            {
            int quantity = UnityEngine.Random.Range(1, 600);

            return quantity;
            }
        public int GetSizeOfTheResource()
            {
            int size = UnityEngine.Random.Range(1, 20);

            return size;
            }
        public static Vector3 GetPositionOfResource()
            {
            Vector3 position;
            position.x = UnityEngine.Random.Range(0, 256);
            position.z = UnityEngine.Random.Range(0, 256);
            position.y = 0.5f;//ToDo Get height of the Prefab and add half of that value.
            return position;
            }
        }
    }

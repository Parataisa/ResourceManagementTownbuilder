using UnityEngine;

namespace Assets.Scripts.TerrainGeneration.ResourceGeneration.ResourceVariationen
    {
    class Coal : MonoBehaviour
        {
        public int sizeOfTheResource;
        public int quantityOfTheResource;
        public string nameOfTheResource;
        void Start()
            {
            ResourceBase resourceBase = new ResourceBase();
            nameOfTheResource = this.GetType().Name;
            sizeOfTheResource = resourceBase.GetSizeOfTheResource();
            quantityOfTheResource = resourceBase.GetQuantityOfTheResource();
            }
        }
    }

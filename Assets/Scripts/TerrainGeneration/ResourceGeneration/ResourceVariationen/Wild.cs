using UnityEngine;

namespace Assets.Scripts.TerrainGeneration.ResourceGeneration.ResourceVariationen
    {
    class Wild : MonoBehaviour
        {
        public int quantityOfTheResource;
        public string nameOfTheResource;
        void Start()
            {
            ResourceBase resourceBase = new ResourceBase();
            nameOfTheResource = this.GetType().Name;
            quantityOfTheResource = resourceBase.GetQuantityOfTheResource();
            }
        }
    }
using UnityEngine;

namespace ResourceGeneration.ResourceVariationen
    {
    class Wild : MonoBehaviour, IResources
        {
        public string ResourceName { get => GetResourceName(); }
        public int QuantityOfTheResource { get; set; }

        public void Start()
            {
            QuantityOfTheResource = GetQuantityOfTheResource();
            }
        private string GetResourceName()
            {
            string resourceName = this.GetType().Name;
            return resourceName;
            }

        public static int GetQuantityOfTheResource()
            {
            int quantity = Random.Range(50000, 80000);
            return quantity;
            }
        public void ResourceQuantityCheck()
            {
            return;
            }

        }
    }

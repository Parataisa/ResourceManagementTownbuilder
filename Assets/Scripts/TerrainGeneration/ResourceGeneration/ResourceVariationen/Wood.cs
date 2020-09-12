using System;
using UnityEngine;

namespace ResourceGeneration.ResourceVariationen
    {
    class Wood : ResourceBase
        {
        public int WildSizeOfTheResource;
        public int WildQuantityOfTheResource;
        protected override void Start()
            {
            base.Start();
            this.ResourceName = "Tree";
            GetWildData();
            }

        private void GetWildData()
            {
            WildSizeOfTheResource = GetQuantityOfTheResource();
            WildQuantityOfTheResource = GetSizeOfTheResource(WildSizeOfTheResource);
            }
        }
    }

using UnityEngine;

namespace Assets.Scripts.TerrainGeneration.ResourceGeneration.ResourceVariationen
    {
    class Wild : ResourceBase
        {
        public string ResourceName;
        protected override void Start()
            {
            base.Start();
            ResourceName = this.GetType().Name;
            var treescript = GetComponent<Wood>();
            this.positionOnTheMap = treescript.positionOnTheMap;
            this.areaOfTheResource = treescript.areaOfTheResource;
            this.sizeOfTheModel = treescript.sizeOfTheModel;
            }
        }
    }
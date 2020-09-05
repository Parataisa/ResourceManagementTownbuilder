using System;
using UnityEngine;

namespace Assets.Scripts.TerrainGeneration.ResourceGeneration.ResourceVariationen
    {
    class Wheat : ResourceBase
        {
        public string ResourceName;
        protected override void Start()
            {
            base.Start();
            ResourceName = this.GetType().Name;
            }
        }
    }

using System;
using UnityEngine;

namespace Assets.Scripts.TerrainGeneration.ResourceGeneration.ResourceVariationen
    {
    class Wood : ResourceBase
        {
        public string ResourceName;
        protected override void Start()
            {
            base.Start();
            ResourceName = this.GetType().Name;
            }
        }
    }

using System;
using UnityEngine;

namespace Assets.Scripts.TerrainGeneration.ResourceGeneration.ResourceVariationen
    {
    class Coal : ResourceBase
        {
        public string ResourceName;
        protected override void Start()
            {
            base.Start();
            ResourceName = this.GetType().Name;
            }
        }
    }

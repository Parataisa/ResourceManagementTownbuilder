using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Buildings.ResourceBuildings
    {
    class Lumberjack : ResourceBuildingBase, IResourcBuilding
        {
        protected override void Start()
            {
            base.Start();
            }
        override internal List<string> SetResouces(List<string> resoucesToGather)
            {
            resoucesToGather.Add("Tree");
            return resouces;
            }
        }
    }

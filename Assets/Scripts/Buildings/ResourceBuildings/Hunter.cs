﻿using System.Collections.Generic;

namespace Assets.Scripts.Buildings.ResourceBuildings
    {
    class Hunter : ResourceBuildingBase, IResourcBuilding
        {
        protected override void Start()
            {
            base.Start();
            }
        override internal List<string> SetResouces(List<string> resoucesToGather)
            {
            resoucesToGather.Add("Wild");
            return resouces;
            }
        }
    }

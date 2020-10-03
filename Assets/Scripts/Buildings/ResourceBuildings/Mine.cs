using System.Collections.Generic;

namespace Assets.Scripts.Buildings.ResourceBuildings
    {
    class Mine : ResourceBuildingBase, IResourcBuilding
        {
        protected override void Start()
            {
            base.Start();
            }
        override internal List<string> SetResouces(List<string> resoucesToGather)
            {
            resoucesToGather.Add("Coal");
            resoucesToGather.Add("Copper");
            resoucesToGather.Add("Iron");
            return resouces;
            }
        }
    }

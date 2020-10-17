using System.Collections.Generic;

namespace Assets.Scripts.Buildings.ResourceBuildings
    {
    class Farm : ResourceBuildingBase
        {
        override internal List<string> SetResouces(List<string> resoucesToGather)
            {
            resoucesToGather.Add("Wheat");
            return resouces;
            }
        }
    }

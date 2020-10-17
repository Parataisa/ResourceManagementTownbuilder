using System.Collections.Generic;

namespace Assets.Scripts.Buildings.ResourceBuildings
    {
    class Hunter : ResourceBuildingBase
        {
        override internal List<string> SetResouces(List<string> resoucesToGather)
            {
            resoucesToGather.Add("Wild");
            return resouces;
            }
        }
    }

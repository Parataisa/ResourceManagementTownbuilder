using System.Collections.Generic;

namespace Assets.Scripts.Buildings.ResourceBuildings
    {
    class Lumberjack : ResourceBuildingBase
        {
        override internal List<string> SetResouces(List<string> resoucesToGather)
            {
            resoucesToGather.Add("Wood");
            return resouces;
            }
        }
    }

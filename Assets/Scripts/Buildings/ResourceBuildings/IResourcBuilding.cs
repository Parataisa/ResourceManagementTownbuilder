using System.Collections.Generic;

namespace Assets.Scripts.Buildings.ResourceBuildings
    {
    interface IResourcBuilding : IBuildings
        {
        List<string> ResourceToGather { get; set; }
        }
    }

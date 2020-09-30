using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Buildings.ResourceBuildings
    {
    interface IResourcBuilding : IBuildings
        {
        List<string> ResourceToGather { get; set; }
        }
    }

using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Buildings.ResourceBuildings
    {
    interface IResourcBuilding
        {
        List<string> ResouceToGather { get; set; }
        Color BuildingColor { get; }
        }
    }

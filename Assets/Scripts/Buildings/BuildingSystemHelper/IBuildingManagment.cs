using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Buildings.BuildingSystemHelper
    {
    interface IBuildingManagment
        {
        List<GameObject> ListOfChildren { get; }
        GameObject GameobjectPrefab { get; set; }

        }
    }

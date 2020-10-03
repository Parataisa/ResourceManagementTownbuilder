using System;
using UnityEngine;

namespace Assets.Scripts.Buildings
    {
    interface IBuildings
        {
        Color BuildingColor { get; }
        String BuildingTyp { get; }
        Vector3 BuildingPosition { get; }
        Vector3 BuildingSize { get; }
        }
    }

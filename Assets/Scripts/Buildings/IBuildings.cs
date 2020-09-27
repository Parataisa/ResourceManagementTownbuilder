using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Buildings
    {
    public static class MainBuildingList
        {
        private static List<GameObject> buildingMain = new List<GameObject>();
        public static List<GameObject> BuildingMain
            {
            get => buildingMain;
            set => buildingMain = value;
            }
        }
    }

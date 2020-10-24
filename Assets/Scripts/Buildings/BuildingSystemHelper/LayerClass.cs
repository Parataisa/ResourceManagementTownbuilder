using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Assets.Scripts.Buildings.BuildingSystemHelper
    {
    public static class LayerClass
        {
        public const int SocialBuildings = 8;
        public const int ResourceBuildings = 9;
        public const int ResourcePatch = 10;
        public const int StorageBuildings = 12;
        
        public const int Ground = 11;

        public static List<int> GetSolitObjectLayer()
            {
            List<int> layerNumberList = new List<int>
                {
                SocialBuildings,
                ResourceBuildings,
                ResourcePatch,
                StorageBuildings
                };
            return layerNumberList;
            }
        public static List<int> GetBuildingLayers()
            {
            List<int> layerNumberList = new List<int>
                {
                SocialBuildings,
                ResourceBuildings,
                StorageBuildings
                };
            return layerNumberList;
            }
        }
    }

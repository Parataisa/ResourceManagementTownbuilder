using System.Collections.Generic;
using UnityEngine;

namespace ResourceGeneration.ResourceVariationen
    {
    class ResouceVariationenList
        {
        public static List<Object> ListOfAllResources = new List<Object>();
        readonly Object[] subListObjects;
        public ResouceVariationenList()
            {
            if (ListOfAllResources == null)
                {
                subListObjects = Resources.LoadAll("GameObjects/GatherableResources/ResourceVariationen", typeof(Object));
                }
            foreach (Object ResourcePrefab in subListObjects)
                {
                ListOfAllResources.Add(ResourcePrefab);
                Debug.Log(ResourcePrefab);
                }
            }
        }
    }

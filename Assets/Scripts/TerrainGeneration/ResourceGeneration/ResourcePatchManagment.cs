using Assets.Scripts.TerrainGeneration.RecourceGeneration;
using ResourceGeneration.ResourceVariationen;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.TerrainGeneration
    {
    class ResourcePatchManagment : MonoBehaviour
        {
        public List<ResourceBase> ResourceOnTheMap = new List<ResourceBase>();
        private void Start()
            {
            FindObjectOfType<ResourceGenerator>().ResourceSuccessfullyGenerated += GetResourcePatchesOnTheMap;
            }

        private void GetResourcePatchesOnTheMap(ResourceBase generatedResource)
            {
            ResourceOnTheMap.Add(generatedResource);
            }
        }
    }

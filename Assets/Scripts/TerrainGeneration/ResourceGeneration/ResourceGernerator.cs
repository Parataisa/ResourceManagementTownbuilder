using Assets.Scripts.TerrainGeneration.ResourceGeneration.ResourceVariationen;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.TerrainGeneration.RecourceGeneration
    {
    class ResourceGernerator : MonoBehaviour
        {
        public static Dictionary<GameObject, ResourceBase> ResourceTyps = new Dictionary<GameObject, ResourceBase>();
        public Mesh terrainMesh;
        private void Start()
            {
            
            }
        }
    }

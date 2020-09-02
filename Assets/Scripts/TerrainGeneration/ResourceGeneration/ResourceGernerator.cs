using Assets.Scripts.TerrainGeneration.ResourceGeneration.ResourceVariationen;
using System;
using UnityEngine;

namespace Assets.Scripts.TerrainGeneration.RecourceGeneration
    {
    class ResourceGernerator : MonoBehaviour
        {
        public Mesh terrainMesh;

        void Start()
            {
            
            }
        private void Update()
            {
            int i = 0;
            if (terrainMesh != null && i<4)
                {
                ResourceBase newresourceBase = new ResourceBase("");
                i++;
                }
            }
        }
    }

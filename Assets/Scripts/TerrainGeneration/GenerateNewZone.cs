using System;
using UnityEngine;

namespace Assets.Scripts.TerrainGeneration
    {
    public class GenerateNewZone : MonoBehaviour
        {
        public GameObject MeshGenerator;

        public void GenerateNewMesh(int Side)
            {
            Vector3 vector3 = new Vector3(0, 0, 254);
            GameObject newMesh = Instantiate(MeshGenerator);
            newMesh.transform.parent = transform;
            newMesh.name = GetName(newMesh.name);
            newMesh.GetComponent<MeshGenerator>().MeshPosition = vector3;
            }

        private string GetName(string name)
            {
            string[] splittName = name.Split('(');
            string[] splittName2 = splittName[1].Split(')');
            return splittName[0] + splittName2[1];
            }
        }
    }

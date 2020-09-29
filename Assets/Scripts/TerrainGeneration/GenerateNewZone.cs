using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.TerrainGeneration
    {
    public class GenerateNewZone : MonoBehaviour
        {
        public GameObject MeshGenerator;
        private static List<Vector3> WorldMeshesPointList = new List<Vector3>();
        private static int ZMapSize;
        private static int XMapSize;
        public GameObject mesh; // test mesh
        private void Start()
            {
            XMapSize = MeshGenerator.GetComponent<MeshGenerator>().xSize;   
            ZMapSize = MeshGenerator.GetComponent<MeshGenerator>().zSize - 2;   
            }
        public void GenerateNewMesh(int side)
            {
            //GameObject mesh = ;//Add Button to some ExploreBuilding which konw on which mesh it stands on
            Vector3 vector3 = GetSpawnPoint(side, mesh);//new Vector3(0, 0, 254);
            WorldMeshesPointList.Add(vector3);
            GameObject newMesh = Instantiate(MeshGenerator);
            newMesh.transform.parent = transform;
            newMesh.name = GetName(newMesh.name);
            newMesh.GetComponent<MeshGenerator>().MeshPosition = vector3;
            }


        private Vector3 GetSpawnPoint(int side, GameObject mesh)
            {
            Vector3 worldPoint = new Vector3();
            if (side == 0)
                {
                worldPoint.z += ZMapSize;
                }
            else if (side == 1)
                {
                worldPoint.x += XMapSize;
                }
            else if (side == 2)
                {
                worldPoint.z -= ZMapSize;
                }
            else if (side == 3)
                {
                worldPoint.x -= XMapSize;
                }
            return worldPoint;
           }

        private string GetName(string name)
            {
            string[] splittName = name.Split('(');
            string[] splittName2 = splittName[1].Split(')');
            return splittName[0] + splittName2[1];
            }
        }
    }

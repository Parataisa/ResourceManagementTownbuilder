using Assets.Scripts.Ui.Menus.InfoUI;
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
            GameObject mesh = FindObjectOfType<SocialBuildingInterfaceOnClick>().selectedGameobject.transform.parent.gameObject; 
            Vector3 NewMeshSpawnPosition = GetSpawnPoint(side, mesh);
            if (NewMeshSpawnPosition == mesh.transform.position)
                {
                Debug.Log("At this Location is already a Mesh");
                return;
                }
            WorldMeshesPointList.Add(NewMeshSpawnPosition);
            GameObject newMesh = Instantiate(MeshGenerator);
            newMesh.transform.parent = transform;
            newMesh.name = GetName(newMesh.name);
            newMesh.GetComponent<MeshGenerator>().MeshPosition = NewMeshSpawnPosition;
            }


        private Vector3 GetSpawnPoint(int side, GameObject mesh)
            {
            Vector3 worldPoint = new Vector3();
            worldPoint = mesh.transform.position;
            if (side == 0)
                {
                worldPoint.z += ZMapSize;
                if (!CheckIfPositionIsFree(worldPoint))
                    {
                    worldPoint.z -= ZMapSize;
                    return worldPoint;
                    }
                }
            else if (side == 1)
                {
                worldPoint.x += XMapSize;
                if (!CheckIfPositionIsFree(worldPoint))
                    {
                    worldPoint.x -= XMapSize;
                    return worldPoint;
                    }
                }
            else if (side == 2)
                {
                worldPoint.z -= ZMapSize;
                if (!CheckIfPositionIsFree(worldPoint))
                    {
                    worldPoint.z += ZMapSize;
                    return worldPoint;
                    }
                }
            else if (side == 3)
                {
                worldPoint.x -= XMapSize;
                if (!CheckIfPositionIsFree(worldPoint))
                    {
                    worldPoint.x += XMapSize;
                    return worldPoint;
                    }
                }
            return worldPoint;
           }

        private bool CheckIfPositionIsFree(Vector3 worldPoint)
            {
            foreach (var savedPoint in WorldMeshesPointList)
                {
                if (savedPoint != worldPoint)
                    {                
                    continue;
                    }
                else if (savedPoint == worldPoint)
                    {
                    return false;
                    }
                return true;
                }
            return true;
            }

        private string GetName(string name)
            {
            string[] splittName = name.Split('(');
            string[] splittName2 = splittName[1].Split(')');
            return splittName[0] + splittName2[1];
            }
        }
    }

﻿using Assets.Scripts.TerrainGeneration.RecourceGeneration;
using System;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
    {
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;
    public readonly int xSize = 256;
    public readonly int zSize = 256;
    public event Action MapGeneratedEvent;
    public Vector3 MeshPosition;
    static int NumberOfMashes = 0;

    void Start()
        {
        this.gameObject.name = this.gameObject.name + "-" + NumberOfMashes;
        mesh = new Mesh
            {
            name = "MapMesh-" + NumberOfMashes
            };
        NumberOfMashes++;
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<Transform>().position = MeshPosition;
        GetComponentInChildren<ResourceGenerator>().terrainMesh = mesh;
        CreateShape();
        UpdadeMesh();
        GetComponent<MeshCollider>().sharedMesh = mesh;
        }


    private void CreateShape()
        {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        for (int i = 0, z = 0; z <= zSize; z++)
            {
            for (int x = 0; x <= xSize; x++)
                {
                vertices[i] = new Vector3(x, 0, z);
                i++;
                }
            }
        triangles = new int[xSize * zSize * 6];
        int vert = 0;
        int tris = 0;
        for (int z = 0; z < zSize; z++)
            {
            for (int x = 0; x < xSize; x++)
                {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;
                vert++;
                tris += 6;
                }

            vert++;
            }

        }

    private void UpdadeMesh()
        {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        MapGeneratedEvent?.Invoke();
        }
    }

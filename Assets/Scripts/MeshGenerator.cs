﻿using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
    {
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;
    Transform parentTransform;
    private readonly int xSize = 128;
    private readonly int zSize = 128;
    void Start()
        {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        CreateShape();
        UpdadeMesh();
        GetComponent<MeshCollider>().sharedMesh = mesh;
        parentTransform = GetComponent<Transform>().parent;
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
        }
}
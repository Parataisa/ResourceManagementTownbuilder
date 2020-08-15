using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationBaker : MonoBehaviour
{

    public NavMeshSurface[] meshSurface;
    void Start()
    {
        for (int i = 0; i < meshSurface.Length; i++)
            {
            meshSurface[i].BuildNavMesh();
            }
        }

}

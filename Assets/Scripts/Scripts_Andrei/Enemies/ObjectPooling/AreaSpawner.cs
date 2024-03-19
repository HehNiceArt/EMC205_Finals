using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Andrei Quirante
public class AreaSpawner : MonoBehaviour
{
    public Vector3 AreaSize;
    public GameObject Spawner;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(transform.position, AreaSize);
    }
}

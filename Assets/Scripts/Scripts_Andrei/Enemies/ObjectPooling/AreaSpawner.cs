using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSpawner : MonoBehaviour
{
    public Vector3 AreaSize;
    public GameObject Spawner;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(transform.position, AreaSize);
    }
}

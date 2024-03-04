using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreePoint : MonoBehaviour
{
    public GameObject Self;

    public static TreePoint Instance { get; private set; }

    private void Awake()
    {
        Instance = GetComponent<TreePoint>();
    }
}

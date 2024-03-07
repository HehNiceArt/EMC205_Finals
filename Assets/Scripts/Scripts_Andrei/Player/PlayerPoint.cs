using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPoint : MonoBehaviour
{
    public GameObject Self;
    public static PlayerPoint Instance { get; private set; }
    private void Awake()
    {
        Instance = GetComponent<PlayerPoint>();
    }
}

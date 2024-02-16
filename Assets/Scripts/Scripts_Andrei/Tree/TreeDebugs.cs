using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeDebugs : MonoBehaviour
{
    [HideInInspector]
    public string TreeDetection;
    private void OnGUI()
    {
        GUI.contentColor = Color.black;
        GUIStyle _guiStyle = new GUIStyle();
        _guiStyle.fontSize = 32;
        GUI.Label(new Rect(10, 10, 100, 20), TreeDetection, _guiStyle);
    }
}

//@Kyle Rafael
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCam : MonoBehaviour
{
    public float Sensitivity;

    float _targetXRotation;
    float _targetYRotation;

    Transform _parent;
    Transform _cam;

    PlayerMovement pm;
    void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _cam = Camera.main.transform;
        _parent = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        if (pm.IsInterrupted)
            return;
        _targetXRotation -= Input.GetAxisRaw("Mouse Y");
        _targetYRotation += Input.GetAxisRaw("Mouse X");

        _parent.eulerAngles = new Vector3(0, _targetYRotation, 0);
        _cam.localEulerAngles = new Vector3(_targetXRotation, 0, 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAAA : MonoBehaviour
{
    public float MoveSpeed;
    public Transform Orientation;
    public Rigidbody _rigidbody;

    float _horizontalInput;
    float _verticalInput;

    Vector3 _moveDirection;

    private void Start()
    {
        _rigidbody.freezeRotation = true;
    }

    private void Update()
    {
        MyInput();
        SpeedControl();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MyInput()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
    }

    void MovePlayer()
    {
        _moveDirection = Orientation.forward * _verticalInput + Orientation.right * _horizontalInput;
        _rigidbody.AddForce(_moveDirection.normalized * MoveSpeed * 10f, ForceMode.Force);
    }
    void SpeedControl()
    {
        Vector3 _flatVel = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);
        if (_flatVel.magnitude > MoveSpeed)
        {
            Vector3 _limitedVel = _flatVel.normalized * MoveSpeed;
            _rigidbody.velocity = new Vector3(_limitedVel.x, _rigidbody.velocity.y, _limitedVel.z);
        }
    }
}

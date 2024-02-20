//@Kyle Rafael
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerFirstPersonMovement : MonoBehaviour
{
    public Camera playerCamera;
    public float WalkSpeed = 6f;
    public float RunSpeed = 12f;
    public float JumpPower = 7f;
    public float Gravity = 10f;
    public float Stamina = 100f;
    public float MaxStamina = 100f;
    public float StaminaDepletionRate = 10f;
    public float StaminaRecoveryRate = 5f;
    public float StaminaThreshold = 20f;
    public float LookSpeed = 2f;
    public float LookXLimit = 45f;
    public bool CanMove = true;

    private Vector3 _moveDirection = Vector3.zero;
    private float _rotationX = 0;
    private CharacterController _characterController;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Vector3 _forward = transform.TransformDirection(Vector3.forward);
        Vector3 _right = transform.TransformDirection(Vector3.right);
        bool _isRunning = Input.GetKey(KeyCode.LeftShift) && Stamina > StaminaThreshold;
        float _speedMultiplier = Stamina > StaminaThreshold ? (_isRunning ? RunSpeed : WalkSpeed) : WalkSpeed;
        float _curSpeedX = CanMove ? _speedMultiplier * Input.GetAxis("Vertical") : 0;
        float _curSpeedY = CanMove ? _speedMultiplier * Input.GetAxis("Horizontal") : 0;

        if (_isRunning && CanMove)
        {
            Stamina -= StaminaDepletionRate * Time.deltaTime;
            Stamina = Mathf.Clamp(Stamina, 0f, MaxStamina);
        }
        else if (!_isRunning && Stamina < MaxStamina)
        {
            Stamina += StaminaRecoveryRate * Time.deltaTime;
            Stamina = Mathf.Clamp(Stamina, 0f, MaxStamina);
        }

        float _movementDirectionY = _moveDirection.y;
        _moveDirection = (_forward * _curSpeedX) + (_right * _curSpeedY);

        if (Input.GetButton("Jump") && CanMove && _characterController.isGrounded)
        {
            _moveDirection.y = JumpPower;
        }
        else
        {
            _moveDirection.y = _movementDirectionY;
        }

        if (!_characterController.isGrounded)
        {
            _moveDirection.y -= Gravity * Time.deltaTime;
        }

        _characterController.Move(_moveDirection * Time.deltaTime);

        if (CanMove)
        {
            _rotationX += -Input.GetAxis("Mouse Y") * LookSpeed;
            _rotationX = Mathf.Clamp(_rotationX, -LookXLimit, LookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * LookSpeed, 0);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerFirstPersonMovement : MonoBehaviour
{
    public Camera _playerCamera;
    public float _walkSpeed = 6f;
    public float _runSpeed = 12f;
    public float _jumpPower = 7f;
    public float _gravity = 10f;
    public float _stamina = 100f; 
    public float _maxStamina = 100f; 
    public float _staminaDepletionRate = 10f; 
    public float _staminaRecoveryRate = 5f; 
    public float _staminaThreshold = 20f; 

    public float _lookSpeed = 2f;
    public float _lookXLimit = 45f;

    private Vector3 _moveDirection = Vector3.zero;
    private float _rotationX = 0;

    public bool _canMove = true;

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

        
        bool _isRunning = Input.GetKey(KeyCode.LeftShift) && _stamina > _staminaThreshold;

        
        if (_isRunning && _canMove)
        {
            _stamina -= _staminaDepletionRate * Time.deltaTime;
            _stamina = Mathf.Clamp(_stamina, 0f, _maxStamina);
        }
        else if (!_isRunning && _stamina < _maxStamina)
        {
            _stamina += _staminaRecoveryRate * Time.deltaTime;
            _stamina = Mathf.Clamp(_stamina, 0f, _maxStamina);
        }

        
        float _speedMultiplier = _stamina > _staminaThreshold ? (_isRunning ? _runSpeed : _walkSpeed) : _walkSpeed;
        float _curSpeedX = _canMove ? _speedMultiplier * Input.GetAxis("Vertical") : 0;
        float _curSpeedY = _canMove ? _speedMultiplier * Input.GetAxis("Horizontal") : 0;

        float _movementDirectionY = _moveDirection.y;
        _moveDirection = (_forward * _curSpeedX) + (_right * _curSpeedY);

        
        if (Input.GetButton("Jump") && _canMove && _characterController.isGrounded)
        {
            _moveDirection.y = _jumpPower;
        }
        else
        {
            _moveDirection.y = _movementDirectionY;
        }

       
        if (!_characterController.isGrounded)
        {
            _moveDirection.y -= _gravity * Time.deltaTime;
        }

      
        _characterController.Move(_moveDirection * Time.deltaTime);

       
        if (_canMove)
        {
            _rotationX += -Input.GetAxis("Mouse Y") * _lookSpeed;
            _rotationX = Mathf.Clamp(_rotationX, -_lookXLimit, _lookXLimit);
            _playerCamera.transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * _lookSpeed, 0);
        }
    }
}

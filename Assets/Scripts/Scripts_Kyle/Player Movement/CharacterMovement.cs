//@Kyle Rafael
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Vector3 _playerMovementInput;
    private Vector2 _playerMouseInput;
    private float _xRotation;

    [SerializeField] private LayerMask _floorMask;
    [SerializeField] private Transform _feetTransform;
    [SerializeField] private Transform _playerCamera;
    [SerializeField] private Rigidbody _playerBody;
    [SerializeField] private float _speed;
    [SerializeField] private float _sprintSpeedMultiplier = 1.5f;
    [SerializeField] private float _sensitivity;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _stamina = 100f;
    [SerializeField] private float _maxStamina = 100f;
    [SerializeField] private float _staminaDepletionRate = 10f;
    [SerializeField] private float _staminaRecoveryRate = 5f;
    [SerializeField] private float _staminaThreshold = 20f;

    private bool _isSprinting = false;

    void Update()
    {
        _playerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        _playerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        if (Input.GetKeyDown(KeyCode.LeftShift) && _stamina > _staminaThreshold) // Check if player has enough stamina to sprint
        {
            _isSprinting = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) || _stamina <= 0)
        {
            _isSprinting = false;
        }

        // Movement
        float currentSpeed = _speed;
        if (_isSprinting)
        {
            currentSpeed *= _sprintSpeedMultiplier;
            _stamina -= _staminaDepletionRate * Time.deltaTime; // Reduce stamina while sprinting
            _stamina = Mathf.Clamp(_stamina, 0f, _maxStamina);
        }

        Vector3 moveVector = transform.TransformDirection(_playerMovementInput) * currentSpeed;
        _playerBody.velocity = new Vector3(moveVector.x, _playerBody.velocity.y, moveVector.z);

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && Physics.CheckSphere(_feetTransform.position, 0.1f, _floorMask))
        {
            _playerBody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }

        // Camera Movement
        _xRotation -= _playerMouseInput.y * _sensitivity;
        transform.Rotate(0f, _playerMouseInput.x * _sensitivity, 0f);
        _playerCamera.transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);

        // Stamina regeneration
        if (!_isSprinting && _stamina < _maxStamina)
        {
            _stamina += _staminaRecoveryRate * Time.deltaTime;
            _stamina = Mathf.Clamp(_stamina, 0f, _maxStamina);
        }
    }
}

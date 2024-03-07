//@Kyle Rafael
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;

    public float Speed;
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float RunSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;

    public float Stamina = 100f;
    public float MaxStamina = 100f;
    public float StaminaDepletionRate = 10f;
    public float StaminaRecoveryRate = 5f;
    public float StaminaThreshold = 20f;

    float _xAxis;
    float _zAxis;

    public bool IsInterrupted;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (IsInterrupted && !Cursor.visible)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else if (!IsInterrupted && Cursor.visible)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (IsInterrupted)
            return;

        _xAxis = Input.GetAxisRaw("Horizontal");
        _zAxis = Input.GetAxisRaw("Vertical");

        // Check for running input
        IsRunning = canRun && Input.GetKey(runningKey);

        // Update stamina
        UpdateStamina();
    }

    void FixedUpdate()
    {
        if (IsInterrupted)
            return;

        Movement();
    }

    void Movement()
    {
        // Calculate movement direction based on input
        Vector3 movement = transform.forward * _zAxis + transform.right * _xAxis;
        movement.Normalize();

        // Apply speed based on whether running or walking
        if (IsRunning && Stamina > StaminaThreshold)
        {
            movement *= RunSpeed;
            Stamina -= StaminaDepletionRate * Time.deltaTime; // Reduce stamina while running
            Stamina = Mathf.Clamp(Stamina, 0f, MaxStamina);
        }
        else
        {
            movement *= Speed;
        }

        // Maintain vertical velocity
        movement += new Vector3(0, rb.velocity.y, 0);

        // Apply movement to rigidbody
        rb.velocity = movement;
    }

    void UpdateStamina()
    {
        if (!IsRunning && Stamina < MaxStamina)
        {
            Stamina += StaminaRecoveryRate * Time.deltaTime;
            Stamina = Mathf.Clamp(Stamina, 0f, MaxStamina);
        }
    }
}
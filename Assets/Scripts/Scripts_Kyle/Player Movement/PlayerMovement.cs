using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;

    public float speed;
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;

    public float stamina = 100f;
    public float maxStamina = 100f;
    public float staminaDepletionRate = 10f;
    public float staminaRecoveryRate = 5f;
    public float staminaThreshold = 20f;

    float xAxis;
    float zAxis;

    public bool isInterrupted;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isInterrupted && !Cursor.visible)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else if (!isInterrupted && Cursor.visible)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (isInterrupted)
            return;

        xAxis = Input.GetAxisRaw("Horizontal");
        zAxis = Input.GetAxisRaw("Vertical");

        // Check for running input
        IsRunning = canRun && Input.GetKey(runningKey);

        // Update stamina
        UpdateStamina();
    }

    void FixedUpdate()
    {
        if (isInterrupted)
            return;

        Movement();
    }

    void Movement()
    {
        // Calculate movement direction based on input
        Vector3 movement = transform.forward * zAxis + transform.right * xAxis;
        movement.Normalize();

        // Apply speed based on whether running or walking
        if (IsRunning && stamina > staminaThreshold)
        {
            movement *= runSpeed;
            stamina -= staminaDepletionRate * Time.deltaTime; // Reduce stamina while running
            stamina = Mathf.Clamp(stamina, 0f, maxStamina);
        }
        else
        {
            movement *= speed;
        }

        // Maintain vertical velocity
        movement += new Vector3(0, rb.velocity.y, 0);

        // Apply movement to rigidbody
        rb.velocity = movement;
    }

    void UpdateStamina()
    {
        if (!IsRunning && stamina < maxStamina)
        {
            stamina += staminaRecoveryRate * Time.deltaTime;
            stamina = Mathf.Clamp(stamina, 0f, maxStamina);
        }
    }
}

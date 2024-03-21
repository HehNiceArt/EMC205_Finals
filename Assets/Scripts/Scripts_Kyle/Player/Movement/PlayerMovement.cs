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
    AudioManage audioManager;
    
    public StaminaBar staminaBar;
   

    float _xAxis;
    float _zAxis;

    public bool IsInterrupted;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioManager = AudioManage.instance;
        staminaBar.SetMaxStamina(MaxStamina);
    }

    void Update()
    {
        staminaBar.SetStamina(Stamina);

        //if (IsInterrupted )
        //{
        //    Cursor.lockState = CursorLockMode.None;
        //}
        //else if (!IsInterrupted)
        //{
        //    Cursor.lockState = CursorLockMode.Locked;
        //}

        if (IsInterrupted)
            return;

        _xAxis = Input.GetAxisRaw("Horizontal");
        _zAxis = Input.GetAxisRaw("Vertical");

        IsRunning = canRun && Input.GetKey(runningKey);

        UpdateStamina();
    }

    void FixedUpdate()
    {
        if (IsInterrupted)
            return;

        Movement();

        bool isWalking = _xAxis != 0 || _zAxis != 0;
        audioManager.PlayWalkingSound(isWalking);

        bool isRunning = IsRunning && Stamina > StaminaThreshold;
        audioManager.PlayRunningSound(isRunning);
    }


    void Movement()
    {
        Vector3 movement = transform.forward * _zAxis + transform.right * _xAxis;
        movement.Normalize();

        if (IsRunning && Stamina > StaminaThreshold)
        {
            movement *= RunSpeed;
            Stamina -= StaminaDepletionRate * Time.deltaTime; 
            Stamina = Mathf.Clamp(Stamina, 0f, MaxStamina);
        }
        else
        {
            movement *= Speed;
        }

        movement += new Vector3(0, rb.velocity.y, 0);

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
//@Kyle Rafael
using UnityEngine;

public class Jump : MonoBehaviour
{
    Rigidbody Rigidbody;
    public float JumpStrength = 2;
    public event System.Action Jumped;

    [SerializeField, Tooltip("Prevents jumping when the transform is in mid-air.")]
    GroundCheck _groundCheck;


    void Reset()
    {
        // Try to get groundCheck.
        _groundCheck = GetComponentInChildren<GroundCheck>();
    }

    void Awake()
    {
        // Get rigidbody.
        Rigidbody = GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {
        // Jump when the Jump button is pressed and we are on the ground.
        if (Input.GetButtonDown("Jump") && (!_groundCheck || _groundCheck.IsGrounded))
        {
            Rigidbody.AddForce(Vector3.up * 100 * JumpStrength);
            Jumped?.Invoke();
        }
    }
}

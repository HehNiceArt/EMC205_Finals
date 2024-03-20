using UnityEngine;

public class Jump : MonoBehaviour
{
    Rigidbody Rigidbody;
    public float JumpStrength = 2;
    public event System.Action Jumped;

    [SerializeField, Tooltip("Prevents jumping when the transform is in mid-air.")]
    GroundCheck _groundCheck;

    public AudioManage audioManager;

    void Reset()
    {
        _groundCheck = GetComponentInChildren<GroundCheck>();
    }

    void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
   
        audioManager = FindObjectOfType<AudioManage>();
    }

    void LateUpdate()
    {
        if (Input.GetButtonDown("Jump") && (!_groundCheck || _groundCheck.IsGrounded))
        {
            Rigidbody.AddForce(Vector3.up * 100 * JumpStrength);
            Jumped?.Invoke();

            if (audioManager != null)
            {
                audioManager.PlayJumpSound();
            }
        }
    }
}

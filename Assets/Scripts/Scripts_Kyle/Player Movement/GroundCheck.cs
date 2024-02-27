//@Kyle Rafael
using UnityEngine;

[ExecuteInEditMode]
public class GroundCheck : MonoBehaviour
{
    [Tooltip("Maximum distance from the ground.")]
    public float DistanceThreshold = .15f;

    [Tooltip("Whether this transform is grounded now.")]
    public bool IsGrounded = true;

    public event System.Action Grounded;

    const float OriginOffset = .001f;
    Vector3 _raycastOrigin => transform.position + Vector3.up * OriginOffset;
    float _raycastDistance => DistanceThreshold + OriginOffset;


    void LateUpdate()
    {
        // Check if we are grounded now.
        bool _isGroundedNow = Physics.Raycast(_raycastOrigin, Vector3.down, DistanceThreshold * 2);

        // Call event if we were in the air and we are now touching the ground.
        if (_isGroundedNow && !IsGrounded)
        {
            Grounded?.Invoke();
        }

        // Update isGrounded.
        IsGrounded = _isGroundedNow;
    }

    void OnDrawGizmosSelected()
    {
        // Draw a line in the Editor to show whether we are touching the ground.
        Debug.DrawLine(_raycastOrigin, _raycastOrigin + Vector3.down * _raycastDistance, IsGrounded ? Color.white : Color.red);
    }
}

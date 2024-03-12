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
        bool _isGroundedNow = Physics.Raycast(_raycastOrigin, Vector3.down, DistanceThreshold * 2);

        if (_isGroundedNow && !IsGrounded)
        {
            Grounded?.Invoke();
        }


        IsGrounded = _isGroundedNow;
    }

    void OnDrawGizmosSelected()
    {
        Debug.DrawLine(_raycastOrigin, _raycastOrigin + Vector3.down * _raycastDistance, IsGrounded ? Color.white : Color.red);
    }
}

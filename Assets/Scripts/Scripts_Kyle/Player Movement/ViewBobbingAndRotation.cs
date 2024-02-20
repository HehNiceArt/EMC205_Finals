//@Kyle Rafael
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ViewBobbingAndRotation : MonoBehaviour
{
    public Transform TargetTransform;
    public float EffectIntensity;
    public float EffectIntensityX;
    public float EffectSpeed;
    public Vector3 OffSet;

    private Vector3 OriginalOffset;
    private float _sinTime;

    void Start()
    {
        OriginalOffset = OffSet;
    }

    void Update()
    {
        transform.rotation = TargetTransform.rotation;

        Vector3 _inputVector = new Vector3(Input.GetAxis("Vertical"), 0f, Input.GetAxis("Horizontal"));

        if (_inputVector.magnitude > 0f)
        {
            _sinTime += Time.deltaTime * EffectSpeed;
        }
        else
        {
            _sinTime = 0f;
        }

        float _sinAmountY = -Mathf.Abs(EffectIntensity * Mathf.Sin(_sinTime));
        Vector3 _sinAmountX = transform.right * EffectIntensity * Mathf.Cos(_sinTime) * EffectIntensityX;

        OffSet = new Vector3
        {
            x = OriginalOffset.x,
            y = OriginalOffset.y + _sinAmountY,
            z = OriginalOffset.z
        };
        OffSet += _sinAmountX;

        transform.position = TargetTransform.position + OffSet;
    }
}
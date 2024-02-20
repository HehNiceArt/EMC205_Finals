//@Kyle Rafael
using UnityEngine;

public class HeadBobbing : MonoBehaviour
{
    public float BobbingSpeed = 0.18f;
    public float BobbingAmount = 0.2f;
    public float Midpoint = 2.0f;

    private float _timer = 0.0f;

    void Update()
    {
        float _waveslice = 0.0f;
        float _horizontal = Input.GetAxis("Horizontal");
        float _vertical = Input.GetAxis("Vertical");

        Vector3 cSharpConversion = transform.localPosition;

        if (Mathf.Abs(_horizontal) == 0 && Mathf.Abs(_vertical) == 0)
        {
            _timer = 0.0f;
        }
        else
        {
            _waveslice = Mathf.Sin(_timer);
            _timer = _timer + BobbingSpeed;
            if (_timer > Mathf.PI * 2)
            {
                _timer = _timer - (Mathf.PI * 2);
            }
        }
        if (_waveslice != 0)
        {
            float translateChange = _waveslice * BobbingAmount;
            float totalAxes = Mathf.Abs(_horizontal) + Mathf.Abs(_vertical);
            totalAxes = Mathf.Clamp(totalAxes, 0.0f, 1.0f);
            translateChange = totalAxes * translateChange;
            cSharpConversion.y = Midpoint + translateChange;
        }
        else
        {
            cSharpConversion.y = Midpoint;
        }

        transform.localPosition = cSharpConversion;
    }
}

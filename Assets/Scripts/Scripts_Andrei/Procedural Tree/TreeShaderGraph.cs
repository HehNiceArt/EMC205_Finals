using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeShaderGraph : MonoBehaviour
{
    public float DissolveStrength;
    public Material DissolveMaterial;

    float _dissolveTime = 1;
    private void Start()
    {
        DissolveMaterial = GetComponent<Renderer>().material;
        StartCoroutine(StartDissolveAppear());
    }
    public IEnumerator StartDissolveHide()
    {
        while (DissolveStrength <= _dissolveTime)
        {
            DissolveStrength += Time.deltaTime;
            DissolveMaterial.SetFloat("_DissolveTime", DissolveStrength);
            
            yield return null;
        }
        DissolveStrength = -1;
    }

    public IEnumerator StartDissolveAppear()
    {
        DissolveStrength = 1;
        while (DissolveStrength >= -1)
        {
            DissolveStrength -= Time.deltaTime;
            DissolveMaterial.SetFloat("_DissolveTime", DissolveStrength);

            yield return null;
        }
        DissolveStrength = 1;
    }
}

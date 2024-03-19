using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Andrei Quirante
public class SkyBoChange : MonoBehaviour
{
    [Header("Skybox")]
    public Material NewSkybox;
    public Material OldSkybox;

    [Header("Player and Tree Positions")]
    public GameObject PlayerPos;
    public GameObject TreePos;

    [Header("Tree area of Influence")]
    public SphereCollider SphereCollider;

    float _detectionRange;
    private  bool _playerInsideArea = false;

    private void Start()
    {
        SphereCollider = GetComponent<SphereCollider>();
    }
    private void Update()
    {

        float _playerToTree = Vector3.Distance(TreePos.transform.position, PlayerPos.transform.position);
        if(_playerToTree <= SphereCollider.radius)
        {
            if (!_playerInsideArea)
            {
                _playerInsideArea =true;
                RenderSettings.skybox = NewSkybox;
                DynamicGI.UpdateEnvironment();
                RenderSettings.fogDensity = 0.005f;
            }
        }
        else
        {
            if(_playerInsideArea)
            {
                _playerInsideArea = false;
                RenderSettings.skybox= OldSkybox;
                DynamicGI.UpdateEnvironment() ;
                RenderSettings.fogDensity = 0.03f;
            }
        }
        //Debug.Log($"Player Inside? {_playerInsideArea}");
    }

}

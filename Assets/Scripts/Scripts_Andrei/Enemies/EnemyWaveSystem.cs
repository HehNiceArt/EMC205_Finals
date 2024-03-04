using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyWaveSystem : MonoBehaviour
{
    public GameObject NextWave;
    public EnemyPoolManager EnemyPoolManager;
    public static EnemyWaveSystem Instance;
    private void Start()
    {
        Instance = this;
        NextWave.SetActive(false);
        if(EnemyPoolManager == null) { EnemyPoolManager = EnemyPoolManager.Instance; }
    }

    public void ActivateNextWaveUI()
    {
        NextWave.SetActive(true);
    }
    public void HideNextWaveUI()
    {
        NextWave.SetActive(false );
    }
}

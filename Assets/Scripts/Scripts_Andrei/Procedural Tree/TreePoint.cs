using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Andrei Quirante
public class TreePoint : MonoBehaviour
{
    public GameObject Self;

    [SerializeField] private Vector3 _firstScale = Vector3.one;
    [Space(10)]
    public bool DetectEnemies = false;
    [Space(10)]
    public float _detectionRange;
    [SerializeField] private float _maxDistance;

    [Range(0f, 10f)]
    [SerializeField] private float _time = 1f;

    public static TreePoint Instance { get; private set; }
    private void Awake()
    {
        Instance = GetComponent<TreePoint>();
    }
    public void EnemyAttackTree(int _damage)
    {
        if (DetectEnemies)
        {
            float _dmgCalculation = _damage * 0.01f;
            Vector3 _dmgScale = (new Vector3(_dmgCalculation, _dmgCalculation, _dmgCalculation));
            Debug.Log($"Dmg Scale {_dmgScale}");
            StartCoroutine(ScaleOverTime(_dmgScale));
        }
    }
    IEnumerator ScaleOverTime(Vector3 _reduceScale)
    {
        DetectEnemies = true;
        float _elapsedTime = 0f;
        Vector3 _minScale = new Vector3(0.5f, 0.5f, 0.5f);
        while(_elapsedTime < _time && DetectEnemies)
        {
            transform.localScale -= _reduceScale * (Time.deltaTime * _time);
            transform.localScale = Vector3.Max(transform.localScale, _minScale);

            _elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _detectionRange);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreePoint : MonoBehaviour
{
    public GameObject Self;

    [SerializeField] private Vector3 _firstScale = Vector3.one;
    [SerializeField] private Vector3 _secondScale;
    [SerializeField] private Vector3 _thirdScale;
    [SerializeField] private Vector3 _fourthScale;
    [SerializeField] private Vector3 _fifthScale;
    [Space(10)]
    public bool DetectEnemies = false;
    public float _detectionRange;
    [SerializeField] private float _maxDistance;
    public LayerMask _enemylayer;

    [Range(0f, 10f)]
    [SerializeField] private float _time = 1f;

    public static TreePoint Instance { get; private set; }
    private void Awake()
    {
        Instance = GetComponent<TreePoint>();
    }
    private void Start()
    {
        _secondScale = new Vector3(1.5f, 1.5f, 1.5f);
        _thirdScale = new Vector3(3f, 3f, 3f);
        _fourthScale = new Vector3(4f, 4f, 4f);
        _fifthScale = new Vector3(5f, 5f, 5f);
    }
    private void Update()
    {
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
       // transform.localScale = _minScale;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _detectionRange);
    }
}

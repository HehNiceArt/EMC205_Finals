using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class TreeItems 
{
    public string Name;
    public TreeGrowthItems ItemsSO;
}

public class TreeScaleCalculation : MonoBehaviour
{
    public List<TreeItems> TreeItem = new List<TreeItems>();

    public List<Vector3> TreeItemIteration = new List<Vector3>();
    public int ScaleCapacity;
    TreePoint _treePoint;

    Vector3 _treeScale = Vector3.one;
    public bool _isSecondIteration;
    public bool _isThirdIteration;
    public bool _isFourthIteration;
    public bool _isFifthIteration;
    private void Awake()
    {
        _treePoint = GetComponent<TreePoint>();
    }
    private void Update()
    {
        //Second Iteration
        CheckScale();
    }

    void CheckScale()
    {
        float _scaleX = transform.localScale.x;

        if(_scaleX >= TreeItemIteration[1].x && _scaleX <= TreeItemIteration[2].x && !_isSecondIteration)
        {
            _isSecondIteration = true;
            CheckIteration(2);
        }
        if(_scaleX >= TreeItemIteration[2].x && _scaleX <= TreeItemIteration[3].x && !_isThirdIteration)
        {
            _isThirdIteration = true;
            CheckIteration(3);
        }
        if(_scaleX >= TreeItemIteration[3].x && _scaleX <= TreeItemIteration[4].x && !_isFourthIteration)
        {
            _isFourthIteration = true;
            CheckIteration(4);
        }
        if(_scaleX >= TreeItemIteration[4].x  && !_isFifthIteration)
        {
            _isFifthIteration = true;
            CheckIteration(5);
        }
    }
    void CheckIteration(int _iteration)
    {
        if(_iteration == 2) { SecondIteration(_iteration); }
        else if(_iteration == 3) { ThirdIteration(_iteration);}
        else if( _iteration == 4) { FourthIteration(_iteration);}
        else if(_iteration == 5) { FifthIteration(_iteration);}
    }
    void SecondIteration(int _local)
    {
        Debug.Log("Second Iteration");
        IterateTreeScale(_local) ;
    }
    void ThirdIteration(int _local)
    {
        Debug.Log("Third Iteration");
        IterateTreeScale(_local);
    }
    void FourthIteration(int _local)
    {
        Debug.Log("Fourth Iteration");
        IterateTreeScale( _local);
    }
    void FifthIteration(int _local)
    {
        Debug.Log("Fifth Iteration");
        IterateTreeScale(_local);
    }
    void IterateTreeScale(int _index)
    {
        Vector3 _currentScale = transform.localScale;
        var _queue = new Queue<Vector3>();
        _queue.Enqueue(_currentScale);
        transform.localScale = _treeScale;

        TreeLSystem3D.Instance.Generate(_index);
        TreeLSystem3D.Instance._iteration = _index;

        transform.localScale = _queue.Dequeue();
    }
}

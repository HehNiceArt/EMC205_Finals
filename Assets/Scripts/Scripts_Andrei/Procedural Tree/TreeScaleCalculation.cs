using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[System.Serializable]
public class TreeIterationScale
{
    public string Name;
    public int Iteration;
    public Vector3 Scale;
}
[System.Serializable]
public class TreeItems 
{
    public string Name;
    public TreeGrowthItems ItemsSO;
}

public class TreeScaleCalculation : MonoBehaviour
{
    public List<TreeIterationScale> TreeIterationScale = new List<TreeIterationScale>();
    public List<TreeItems> TreeItem = new List<TreeItems>();

    public int ScaleCapacity;
    TreePoint _treePoint;

    Vector3 _treeScale = Vector3.one;
    int _local = 1;
    private void Awake()
    {
        _treePoint = GetComponent<TreePoint>();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && _local <= 5)
        {
            _local += 1;
            CheckIteration(_local);
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

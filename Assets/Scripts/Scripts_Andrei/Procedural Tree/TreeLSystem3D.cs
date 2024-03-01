using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System;
using UnityEngine.UIElements;
using UnityEditorInternal;
using UnityEngine.ProBuilder;
using System.Linq;
using UnityEngine.Rendering.Universal;
using TMPro;
using TreeEditor;

public class TransformInfo3D
{
    public Vector3 Position3D;
    public Quaternion Rotation3D;
}
public class TreeLSystem3D : MonoBehaviour
{
    [SerializeField][Range(0, 5)] private int _iteration = 0;

    [Header("Tree Values")]
    [SerializeField] [Range(0, 1)] private float _age;
    [SerializeField] [Range(0, 2)] private float _maxLength;
    [SerializeField] [Range(0, 1)] private float _diameter;
    [SerializeField] [Range(0, 1)] private float _flowerVariance;
    [SerializeField] [Range(0, 1)] private float _leafVariance;
    [SerializeField] private float _angle;

    [Header("Tree Parts")]
    [SerializeField] private GameObject _treeParentBranch;
    [SerializeField] private GameObject _treeParentLeaf;
    [SerializeField] private GameObject _treeParentFlower;
    [SerializeField] private GameObject _treeBranch;
    [SerializeField] private GameObject _treeLeaf;
    [SerializeField] private GameObject _treeFlower;

    [Header("Rules")]
    [SerializeField] private string _initialState;
    [SerializeField] private string _productionRule;

    private const string _axiom = "X";

    private Dictionary<char, string> _rules;
    private Stack<TransformInfo3D> _transformStack;
    private string _currentString = string.Empty;


    //FF[&+F]F[-<F][->F][+^FF]
    //[-<F]F[+^F]F[&F][>F[+&F][-^F][<+F]]
    private void Start()
    {
        _transformStack = new Stack<TransformInfo3D>();

        _rules = new Dictionary<char, string>
        {
            {'X', _initialState },
            {'F', _productionRule }
        };
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(_iteration <= 5)
            {
            _iteration++;
            Generate(_iteration);
            }

        }
    }

    void Generate(int _updateIteration)
    {
        // _tree = _parent;
        _currentString = _axiom;

        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < _updateIteration; i++)
        {
            foreach (char c in _currentString)
            {
                sb.Append(_rules.ContainsKey(c) ? _rules[c] : c.ToString());
            }
            _currentString = sb.ToString();
            sb = new StringBuilder();
        }
        print(_currentString);

        for (int i = 0; i < _currentString.Length; i++)
        {
            switch (_currentString[i])
            {
                case 'f':
                    ResetRotation();
                    break;
                case 'F':   // Move forward a step of length d. A line segment between points (X,Y,Z) and (X', Y', Z') is drawn;
                    CreateBranch();
                    break;
                case 'X':
                    break;
                case '+':       // Turn left by angle Delta, Using rotation matrix R_U(Delta).
                    TurnLeft();
                    break;
                case '-':       // Turn right by angle Delta, Using rotation matrix R_U(-Delta).
                    TurnRight();
                    break;
                case '^':       // Pitch by angle Delta, Using rotation matrix R_L(-Delta).
                    PitchUp();
                    break;
                case '&':
                    PitchDown();
                    break;
                case 'l':
                    RollLeft();
                    break;
                case '/':
                    RollRight();
                    break;
                case 'h':
                    Flower();
                    break;
                case 'a':
                    Leaf();
                    break;
                case '[':       // Push the current state 
                    _transformStack.Push(new TransformInfo3D()
                    {
                        Position3D = transform.position,
                        Rotation3D = transform.rotation,
                    });
                    break;
                case ']':       // Pop the current state
                    TransformInfo3D ti = _transformStack.Pop();
                    transform.position = ti.Position3D;
                    transform.rotation = ti.Rotation3D;
                    break;
                default:
                    throw new InvalidOperationException("Invalid L-system operaion");
            }
        }
    }
    Quaternion _initialRotation;
    GameObject _branch;
    Quaternion _rotation;
    void CreateBranch() //Create the tree branch
    {
        _branch = Instantiate(_treeBranch, transform.position, transform.rotation);              //Instantiate the branch at (0,0 + _posOffset,0)
        _initialRotation = Quaternion.identity;                                                               //Gets the initial Rotation of the Branch

        _branch.transform.localScale = new Vector3(_diameter, _maxLength, _diameter);

        _branch.transform.position = transform.position;

        transform.Translate(Vector3.up * _maxLength);

        _branch.transform.parent = _treeParentBranch.transform;

        _maxLength -= UnityEngine.Random.Range(0, 0.5f);
        if(_maxLength <= 0)
        {
            _maxLength = 2;
        }
        //Debug.Log("Position: " + _branch.transform.position + " Scale: " + _branch.transform.localScale);
    }
    void Flower()
    {
        float _rand = UnityEngine.Random.Range(0, _flowerVariance);
        GameObject _flower = Instantiate(_treeFlower, transform.position, transform.rotation);
        _flower.transform.localScale = new Vector3(_rand, _rand, _rand);
        _flower.transform.parent = _treeParentFlower.transform;
    }
    void Leaf()
    {
        float _rand = UnityEngine.Random.Range(0, _leafVariance);
        GameObject _leaf = Instantiate(_treeLeaf, transform.position, transform.rotation);
        _leaf.transform.localScale = new Vector3(_rand, _rand, _rand);  
        _leaf.transform.parent = _treeParentLeaf.transform;
    }

    void ResetRotation()   //Move the postion upwards without instantiating a branch
    {
        float _random = UnityEngine.Random.Range(-90, 90);
        transform.rotation = Quaternion.Euler(0f, 0f, _random);
    }

    /// <summary> 3x3 RotationUp
    ///   Row             0             1          2    Column 
    ///             |  Cos(angle) ,  Sin(angle) ,  0  |   0
    /// Ru(angle) = | -Sin(angle) ,  Cos(angle) ,  0  |   1
    ///             |     0       ,     0       ,  1  |   2
    /// </summary>
    /// <param name="angle">Rotation 3x3</param>
    /// <returns>Gets the 3x3 rotation</returns>
    Matrix4x4 RotationUp(float angle)
    {
        Matrix4x4 _rotationUp = Matrix4x4.zero;

        //First row
        _rotationUp[0, 0] = Mathf.Cos(angle);
        _rotationUp[0, 1] = Mathf.Sin(angle);
        _rotationUp[0, 2] = 0f;
        _rotationUp[0, 3] = 0f;
        //Second row
        _rotationUp[1, 0] = -Mathf.Sin(angle);
        _rotationUp[1, 1] = Mathf.Cos(angle);
        _rotationUp[1, 2] = 0f;
        _rotationUp[1, 3] = 0f;
        //Third row
        _rotationUp[2, 0] = 0f;
        _rotationUp[2, 1] = 0f;
        _rotationUp[2, 2] = 1;
        _rotationUp[2, 3] = 0f;
        // Fourth row
        _rotationUp[3, 0] = 0f;
        _rotationUp[3, 1] = 0f;
        _rotationUp[3, 2] = 0f;
        _rotationUp[3, 3] = 1f;
        return _rotationUp;
    } 

    /// <summary> 3x3 RotationPitch
    ///     Row         0        1             2         Column  
    ///             |   1   ,    0       ,     0       |   0
    /// Rl(angle) = |   0   , Cos(angle) , -Sin(angle) |   1
    ///             |   0   , Sin(angle) ,  Cos(angle) |   2
    /// </summary>
    /// <param name="angle">Parameter value to pass</param>
    /// <returns></returns>
    Matrix4x4 RotationPitch(float angle)
    {
        Matrix4x4 _rotationPitch = Matrix4x4.zero;

        //First row
        _rotationPitch[0, 0] = 1f;
        _rotationPitch[0, 1] = 0f;
        _rotationPitch[0, 2] = 0f;
        _rotationPitch[0, 3] = 0f;
        //Second row
        _rotationPitch[1, 0] = 0f;
        _rotationPitch[1, 1] = Mathf.Cos(angle);
        _rotationPitch[1, 2] = -Mathf.Sin(angle);
        _rotationPitch[1, 3] = 0f;
        //Third row
        _rotationPitch[2, 0] = 0f;
        _rotationPitch[2, 1] = Mathf.Sin(angle);
        _rotationPitch[2, 2] = Mathf.Cos(angle);
        _rotationPitch[2, 3] = 0f;
        // Fourth row
        _rotationPitch[3, 0] = 0f;
        _rotationPitch[3, 1] = 0f;
        _rotationPitch[3, 2] = 0f;
        _rotationPitch[3, 3] = 1f;

        return _rotationPitch;
    }
    /// <summary>
    /// 3x3 RotationRoll
    ///     Row          0          1        2          Column
    ///             | Cos(angle) ,  0  , -Sin(angle) |    0
    /// Rh(angle) = |    0       ,  1  ,     0       |    1
    ///             | Sin(angle) ,  0  ,  Cos(angle) |    2
    /// </summary>
    /// <param name="angle"></param>
    /// <returns></returns>
    Matrix4x4 RotationHeading(float angle)
    {
        Matrix4x4 _rotationHeading = Matrix4x4.zero;

        //First row
        _rotationHeading[0, 0] = Mathf.Cos(angle);
        _rotationHeading[0, 1] = 0f;
        _rotationHeading[0, 2] = -Mathf.Sin(angle);
        _rotationHeading[0, 3] = 0f;
        //Second row
        _rotationHeading[1, 0] = 0f;
        _rotationHeading[1, 1] = 1f;
        _rotationHeading[1, 2] = 0f;
        _rotationHeading[1, 3] = 0f;
        //Third row
        _rotationHeading[2, 0] = Mathf.Sin(angle);
        _rotationHeading[2, 1] = 0f;
        _rotationHeading[2, 2] = Mathf.Cos(angle);
        _rotationHeading[2, 3] = 0f;
        // Fourth row
        _rotationHeading[3, 0] = 0f;
        _rotationHeading[3, 1] = 0f;
        _rotationHeading[3, 2] = 0f;
        _rotationHeading[3, 3] = 1f;
        return _rotationHeading;
    }
    void TurnLeft()                     // +
    {
        Quaternion _currentRotation = transform.rotation;
        Matrix4x4 _rotationUp = Matrix4x4.TRS(Vector3.zero, _currentRotation, Vector3.one) * RotationUp(_angle);
        Quaternion _rotation = Quaternion.LookRotation(_rotationUp.GetColumn(2), _rotationUp.GetColumn(1));

        transform.rotation = _rotation;
    }
    void TurnRight()                    // -
    {
        Quaternion _currentRotation = transform.rotation;
        Matrix4x4 _rotationDown = Matrix4x4.TRS(Vector3.zero, _currentRotation, Vector3.one) * RotationUp(-_angle);
        Quaternion _rotation = Quaternion.LookRotation(_rotationDown.GetColumn(2), _rotationDown.GetColumn(1));

        transform.rotation = _rotation;
    }

    void PitchUp()                      // ^
    {
        Quaternion _currentRotation = transform.rotation;
        Matrix4x4 _pitchUp =  Matrix4x4.TRS(Vector3.zero, _currentRotation, Vector3.one) * RotationPitch(_angle);
        Quaternion _rotation = Quaternion.LookRotation(_pitchUp.GetColumn(0), _pitchUp.GetColumn(1));

        transform.rotation = _rotation;
    }
    void PitchDown()                    // &
    {
        Quaternion _currentRotation = transform.rotation;
        Matrix4x4 _pitchDown = Matrix4x4.TRS(Vector3.zero, _currentRotation, Vector3.one) * RotationPitch(-_angle);
        Quaternion _rotation = Quaternion.LookRotation(_pitchDown.GetColumn(0), _pitchDown.GetColumn(1));

        transform.rotation = _rotation;
    }
    void RollLeft()                     // l
    {
        Quaternion _currentRotation = transform.rotation;
        Matrix4x4 _rollLeft = Matrix4x4.TRS(Vector3.zero, _currentRotation, Vector3.one) * RotationHeading(_angle);
        Quaternion _rotation = Quaternion.LookRotation(_rollLeft.GetColumn(0), _rollLeft.GetColumn(1));

        transform.rotation = _rotation;
    }

    void RollRight()                    // /
    {
        Quaternion _currentRotation = transform.rotation;
        Matrix4x4 _rollRight = Matrix4x4.TRS(Vector3.zero, _currentRotation, Vector3.one)* RotationHeading(-_angle);
        Quaternion _rotation = Quaternion.LookRotation(_rollRight.GetColumn(0), _rollRight.GetColumn(1));

        transform.rotation = _rotation;
    }

}

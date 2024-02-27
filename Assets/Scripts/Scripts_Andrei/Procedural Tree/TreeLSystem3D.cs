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
    [SerializeField] [Range(0, 1)] private float _maxLength;
    [SerializeField] [Range(0, 1)] private float _diameter;
    [SerializeField] private float _angle;
    [SerializeField] private float _yaw;
    [SerializeField] private float _pitch;
    [SerializeField] private float _roll;

    [Header("Tree Parts")]
    [SerializeField] private GameObject _treeParent;
    [SerializeField] private GameObject _treeBranch;
    [SerializeField] private GameObject _treeLeaf;
    [SerializeField] private GameObject _treeFlower;
    public ProBuilderMesh Quad;

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
                case 'x':       // Pitch by angle Delta, Using rotation matrix R_L(-Delta).
                    Pitch();
                    break;
                case 'y':
                    Yaw();
                    break;
                case 'z':       // Roll right by angle Delta, Using rotation matrix R_H(-Delta).
                    Roll();
                    break;
                case '|':       // Turn around, Using rotation matrix R_H(180).
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
    void CreateBranch() //Create the tree branch
    {
        _branch = Instantiate(_treeBranch, transform.position, transform.rotation);              //Instantiate the branch at (0,0 + _posOffset,0)
        _initialRotation = Quaternion.identity;                                                               //Gets the initial Rotation of the Branch

        _branch.transform.localScale = new Vector3(_diameter, _maxLength, _diameter);

        _branch.transform.position = transform.position;

        transform.Translate(Vector3.up * _maxLength);

        _branch.transform.parent = _treeParent.transform;

        _maxLength -= 0.1f;
        if(_maxLength <= 0)
        {
            _maxLength = 1;
        }
        //Debug.Log("Position: " + _branch.transform.position + " Scale: " + _branch.transform.localScale);
    }
    void ResetRotation()   //Move the postion upwards without instantiating a branch
    {
        float _random = UnityEngine.Random.Range(-90, 90);
        transform.rotation = Quaternion.Euler(0f, 0f, _random);
    }
    void TurnLeft()     // +
    {
        float _random = UnityEngine.Random.Range(0, _angle);
        transform.Rotate(Vector3.right * _random);
    }
    void TurnRight()    // -
    {
        float _random = UnityEngine.Random.Range(0, _angle);
        transform.Rotate(Vector3.left * _random);
    }
    void Pitch()    // x
    {
        float _random = UnityEngine.Random.Range(-_pitch, _pitch);
        transform.rotation = Quaternion.Euler(_random, 0, 0);
        //transform.Rotate(Vector3.right * _pitch);
    }
    void Yaw()      // y
    {
        float _random = UnityEngine.Random.Range(-_yaw, _yaw);
        transform.rotation = Quaternion.Euler(0, _random, 0);
        //transform.Rotate(Vector3.up * _yaw);
    }
    void Roll()     // z
    {
        float _random = UnityEngine.Random.Range(-_roll, _roll);
        transform.rotation = Quaternion.Euler(0, 0, _random);
        //transform.Rotate(Vector3.forward * _roll);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System;
using TMPro.EditorUtilities;

public class TransformInfo3D
{
    public Vector3 Position3D;
    public Quaternion Rotation3D;
}
public class TreeLSystem3D : MonoBehaviour
{
    [Range(0, 5)][SerializeField] private int _iteration;

    [Header("Tree Values")]
    [SerializeField] private float _rotation;
    [Range(0, 1)][SerializeField] private float _radius;
    [SerializeField] private float _width;
    [SerializeField] private float _length;
    [SerializeField] private float _variance;
    [SerializeField] private float _flowerRate;
    [SerializeField] private GameObject _tree = null;
    [Header("Angles")]
    [Tooltip("Pitch angle of the tree")]            // > denotes for MOVE towards!
    [SerializeField] private float _anglePitch;     // angle Y > Z || angle Y > -Z
    [Tooltip("Roll angle of the tree")]             //
    [SerializeField] private float _angleRoll;      // angle Y > X || angle Y > -X

    [Header("Rules")]
    [SerializeField] private string _initialState;
    [SerializeField] private string _productionRule;

    [Header("Tree Parts")]
    [SerializeField] private GameObject _parent;
    [SerializeField] private GameObject _branch;
    [SerializeField] private GameObject _leaf;
    [SerializeField] private GameObject _flower;

    private const string _axiom = "X";

    private Dictionary<char, string> _rules;
    private Stack<TransformInfo3D> _transformStack;
    private string _currentString = string.Empty;

    //FF[&+F]F[-<F][->F][+^FF]
    private void Start()
    {
        _transformStack = new Stack<TransformInfo3D>();

        _rules = new Dictionary<char, string>
        {
            {'X', _initialState },
            {'F', _productionRule }
        };
        Generate();
    }

    private void Update()
    {
        //if(_iteration == 0) { return; }
        //else if(_iteration == 1) { Generate(); }
        //else if(_iteration == 2) { Generate(); }
        //else if( _iteration == 3) { Generate(); }
        //else if( _iteration == 4) { Generate(); }
        //else if( _iteration == 5) { Generate(); }
    }

    Quaternion _initialRotation;
    void Generate()
    {
        Destroy(_tree);

        _tree = _parent;
        _currentString = _axiom;

        StringBuilder sb = new StringBuilder();

        for(int i = 0; i < _iteration; i++)
        {
            foreach(char c in _currentString)
            {
                sb.Append(_rules.ContainsKey(c) ? _rules[c] : c.ToString());
            }
            _currentString = sb.ToString();
            sb = new StringBuilder();
        }
        print(_currentString);

        for(int i = 0; i < _currentString.Length; i++)
        {
            switch(_currentString[i])
            {
                case 'F':       // Move forward a step of length d. A line segment between points (X,Y,Z) and (X', Y', Z') is drawn;
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
                case '&':       // Pitch down by angle Delta, Using rotation matrix R_L(Delta).
                    PitchDown();
                    break;
                case '^':       // Pitch up by angle Delta, Using rotation matrix R_L(-Delta).
                    PitchUp();
                    break;
                case '<':       // Roll left by angle Delta, Using rotation matrix R_H(Delta).
                    RollLeft();
                    break;
                case '>':       // Roll right by angle Delta, Using rotation matrix R_H(-Delta).
                    RollRight();
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

    void CreateBranch()
    {
        GameObject _treeBranch = Instantiate(_branch, transform.position, transform.rotation);      //Instantiate the branch at (0,0,0)
        _initialRotation = transform.rotation;                                                      //Gets the initial rotation of the _treeBranch
        _treeBranch.transform.position = transform.position;                                        //gets the position of world spawn to localspawn
        _treeBranch.transform.localScale = new Vector3(_radius * 2, _length, _radius * 2);          //Change the scale
        transform.Translate(Vector3.up * _length);                                                  //Moves the turtle upward
        _treeBranch.transform.parent = _parent.transform;                                           //sets the parent
    }
    void TurnLeft()
    {
        Quaternion _bottomRotation = _initialRotation;
        float _rand = UnityEngine.Random.Range(20f, _rotation);
        transform.Rotate(new Vector3(1, 0, 0), -_rand);
    }
    void TurnRight()
    {
        Quaternion _bottomRotation = _initialRotation;
        float _rand = UnityEngine.Random.Range(20f, _rotation);
        transform.Rotate(new Vector3(1, 0, 0), _rand);
    }
    void PitchDown()
    {
        transform.rotation = Quaternion.Euler(0, 0, _anglePitch);
    }
    void PitchUp()
    {
        transform.rotation = Quaternion.Euler(0,0, -_anglePitch);
    }
    void RollLeft()
    {
        transform.rotation = Quaternion.Euler(_angleRoll, 0, 0);
    }
    void RollRight()
    {
        transform.rotation = Quaternion.Euler(-_angleRoll, 0, 0);
    }
}

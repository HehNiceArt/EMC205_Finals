using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System;
using UnityEngine.UIElements;
using UnityEditorInternal;

public class TransformInfo3D
{
    public Vector3 Position3D;
    public Quaternion Rotation3D;
}
public class TreeLSystem3D : MonoBehaviour
{
    [SerializeField][Range(0, 5)] private int _iteration = 0;

    [Header("Tree Parts")]
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
            _iteration++;
            Generate(_iteration);
        }
    }

    Quaternion _initialRotation;
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
                    InitializeBranch();
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
    void InitializeBranch()
    {

    }
    void CreateBranch()
    {

    }
    void TurnLeft()
    {

    }
    void TurnRight()
    {

    }
    void PitchDown()
    {

    }
    void PitchUp()
    {

    }
    void RollLeft()
    {

    }
    void RollRight()
    {

    }
}

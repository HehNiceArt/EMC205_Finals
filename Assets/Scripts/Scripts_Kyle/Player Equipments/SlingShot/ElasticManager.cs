using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElasticManager : MonoBehaviour
{
    [Range(1, 15)] 
    [SerializeField] private int numberOfBones;

    [Range(0, 7)]   
    public float elasticResistance;

    public float spring = 3000f;
    public float minDistance = 0f;
    public float maxDistance = 0.15f;
    public float tolerance = 0.05f;
    public float elasticLinesWidth = 0.4f; 

    [SerializeField] private GameObject[] rightBones = new GameObject[15];
    [SerializeField] private GameObject[] leftBones = new GameObject[15];
    [SerializeField] private GameObject rightBoneEnd;
    [SerializeField] private GameObject leftBoneEnd;

    [SerializeField] private GameObject leather;
    [SerializeField] private GameObject leatherLine;
    [SerializeField] private SpringJoint knotR;
    [SerializeField] private SpringJoint knotL;
    private FixedJoint[] leatherJoints;
    private FixedJoint[] leatherLineJoints;

    [SerializeField] private LineRenderer rightElasticLine;
    [SerializeField] private LineRenderer leftElasticLine;
    
    private int i = 1; 
    
    void Awake()
    {
        leatherJoints = leather.GetComponents<FixedJoint>();
        leatherLineJoints = leatherLine.GetComponents<FixedJoint>();

        rightElasticLine.startWidth = elasticLinesWidth;
        rightElasticLine.endWidth = elasticLinesWidth;

        leftElasticLine.startWidth = elasticLinesWidth;
        leftElasticLine.endWidth = elasticLinesWidth;

        foreach (GameObject bone in rightBones)
        {
            if(i <= numberOfBones)
            {
                SpringJoint bsj = bone.GetComponent<SpringJoint>();
                bsj.spring = spring;
                bsj.minDistance = minDistance;
                bsj.maxDistance = maxDistance;
                bsj.tolerance = tolerance;

                if (i == numberOfBones)
                {
                    rightBoneEnd.transform.parent = bone.transform;
                    leatherJoints[1].connectedBody = bone.GetComponent<Rigidbody>();
                    leatherLineJoints[1].connectedBody = bone.GetComponent<Rigidbody>();
                    knotR.connectedBody = bone.GetComponent<Rigidbody>();
                }
            }
            else
            {
                bone.SetActive(false);
            }

            i++;
        }
        i = 1;

        foreach (GameObject bone in leftBones)
        {
            if (i <= numberOfBones)
            {
               
                SpringJoint bsj = bone.GetComponent<SpringJoint>();
                bsj.spring = spring;
                bsj.minDistance = minDistance;
                bsj.maxDistance = maxDistance;
                bsj.tolerance = tolerance;

                if (i == numberOfBones)
                {
                    leftBoneEnd.transform.parent = bone.transform;
                    leatherJoints[0].connectedBody = bone.GetComponent<Rigidbody>();
                    leatherLineJoints[0].connectedBody = bone.GetComponent<Rigidbody>();
                    knotL.connectedBody = bone.GetComponent<Rigidbody>();
                }
            }
            else if (i > numberOfBones)
            {
                bone.SetActive(false);
            }

            i++;
        }
    }   
}

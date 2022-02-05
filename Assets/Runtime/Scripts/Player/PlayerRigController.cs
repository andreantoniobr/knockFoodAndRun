using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;


public class PlayerRigController : MonoBehaviour
{
    [SerializeField] private TwoBoneIKConstraint leftHandIK;
    [SerializeField] private TwoBoneIKConstraint rightHandIK;
    [SerializeField] private RigBuilder rigBuilder;

    [SerializeField] private Transform rightHandTarget;
    [SerializeField] private Transform leftHandTarget;

    private void Awake()
    {
        
    }

    private void Start()
    {
        leftHandIK.data.target = leftHandTarget;
        rightHandIK.data.target = rightHandTarget;
        SetTwoBoneIKConstraintWeight(leftHandIK, 0);
        SetTwoBoneIKConstraintWeight(rightHandIK, 0);     
        rigBuilder.Build();
    }

    private void SetTwoBoneIKConstraintWeight(TwoBoneIKConstraint twoBoneIKConstraint, float weight)
    {
        twoBoneIKConstraint.data.hintWeight = weight;
        twoBoneIKConstraint.data.targetPositionWeight = weight;
        twoBoneIKConstraint.data.targetRotationWeight = weight;
    }

    private IEnumerator SlapCoroutine(Vector3 targetPosition, HandDirection handDirection)
    {
        TwoBoneIKConstraint hand = GetHandIKFromDirection(handDirection);
        Transform target = GetTargetFromDirection(handDirection);

        if (hand && target)
        {
            SetTwoBoneIKConstraintWeight(hand, 1f);
            Vector3 direction = targetPosition - hand.data.mid.position;
            //leftHandTarget.rotation = Quaternion.Euler(direction);
            //leftHandTarget.rotation = Quaternion.FromToRotation(Vector3.up, direction);
            hand.data.tip.rotation = Quaternion.FromToRotation(Vector3.up, direction);
            target.position = targetPosition;
            //leftHandTarget.transform.LookAt(targetPosition);
            yield return new WaitForSeconds(0.2f);
            SetTwoBoneIKConstraintWeight(hand, 0f);
        }        
    }

    private TwoBoneIKConstraint GetHandIKFromDirection(HandDirection handDirection)
    {
        TwoBoneIKConstraint handIK = leftHandIK;
        if (handDirection == HandDirection.Right)
        {
            handIK = rightHandIK;
        }
        return handIK;
    }

    private Transform GetTargetFromDirection(HandDirection handDirection)
    {
        Transform target = leftHandTarget;
        if (handDirection == HandDirection.Right)
        {
            target = rightHandTarget;
        }
        return target;
    }

    public void StartSlap(Vector3 targetPosition, HandDirection handDirection)
    {
        StartCoroutine(SlapCoroutine(targetPosition, handDirection));
    }    
}

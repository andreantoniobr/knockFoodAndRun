using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TargetConfig
{
    [SerializeField] private float gravity = 9.8f;
    
    [SerializeField] private float minDistanceX = 0.5f;
    [SerializeField] private float maxDistanceX = 5f;

    [Space]
    [SerializeField] private float minForwardForceZ = 15f;
    [SerializeField] private float maxForwardForceZ = 30f;

    [Space]
    [SerializeField] private float minUpForceY = 5f;
    [SerializeField] private float maxUpForceY = 20f;

    public float Gravity => gravity;

    public float MinForwardForceZ => minForwardForceZ;
    public float MaxForwardForceZ => maxForwardForceZ;

    public float MinUpForceY => minUpForceY;
    public float MaxUpForceY => maxUpForceY;
}

public class Target : MonoBehaviour
{/*
    [SerializeField] private float gravity = 40f;

    [Space]
    [SerializeField] private float minDistanceX = 0.5f;
    [SerializeField] private float maxDistanceX = 5f;

    [Space]
    [SerializeField] private float minForwardForceZ = 60f;
    [SerializeField] private float maxForwardForceZ = 60f;

    [Space]
    [SerializeField] private float minUpForceY = 14f;
    [SerializeField] private float maxUpForceY = 14f;*/

    [Space]
    [Range(0f, 1f)]
    [SerializeField] private float knockUpMovementPercent = 0.5f;

    [SerializeField] private TargetConfig targetConfigInNormalMovement;
    [SerializeField] private TargetConfig targetConfigInUpMovement;

    [Space]
    [SerializeField] private bool isKnocked = false;
    [SerializeField] private bool isknockUpMovement = false;

    private Vector3 currentPosition;
    private Vector3 initialPosition;
    private TargetConfig targetConfig = null;

    public static event System.Action OnknockedEvent;

    private void Awake()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        ProcessForwardMovement();
        ProcessFoodGravity();
        UpdateFoodPosition();
    }

    private void ProcessForwardMovement()
    {
        if (isKnocked && targetConfig != null)
        {
            currentPosition.z += targetConfig.MinForwardForceZ * Time.deltaTime;
        }            
    }

    private void ProcessFoodGravity()
    {
        if (isKnocked && targetConfig != null)
        {
            currentPosition.y -= targetConfig.Gravity * Time.deltaTime;
        }
    }    

    private void UpdateFoodPosition()
    {
        if (isKnocked)
        {
            transform.position += currentPosition * Time.deltaTime;
        }        
    }

    private void SetMovementConfig()
    {
        targetConfig = targetConfigInNormalMovement;
        if (knockUpMovementPercent >= Random.value)
        {
            //Debug.Log("Up");
            isknockUpMovement = true;
            targetConfig = targetConfigInUpMovement;
        }
    }

    private void SetInitialForces()
    {
        SetInitialUpForceY();
    }    

    private void SetInitialUpForceY()
    {
        if (targetConfig != null)
        {
            float upForceY = Random.Range(targetConfig.MinUpForceY, targetConfig.MaxUpForceY);
            currentPosition.y = upForceY;
        }        
    }

    public void Slap()
    {
        if (!isKnocked)
        {
            isKnocked = true;
            OnknockedEvent?.Invoke();
            SetMovementConfig();
            SetInitialForces();
        }        
    }
}

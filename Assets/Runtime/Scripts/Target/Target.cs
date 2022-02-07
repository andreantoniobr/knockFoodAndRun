using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TargetConfig
{
    [SerializeField] private float gravity = 9.8f;

    [Space]
    [SerializeField] private float minHorizontalForceX = 0.5f;
    [SerializeField] private float maxHorizontalForceX = 5f;

    [Space]
    [SerializeField] private float minForwardForceZ = 15f;
    [SerializeField] private float maxForwardForceZ = 30f;

    [Space]
    [SerializeField] private float minUpForceY = 5f;
    [SerializeField] private float maxUpForceY = 20f;

    public float Gravity => gravity;

    public float MinHorizontalForceX => minHorizontalForceX;
    public float MaxHorizontalForceX => maxHorizontalForceX;

    public float MinForwardForceZ => minForwardForceZ;
    public float MaxForwardForceZ => maxForwardForceZ;

    public float MinUpForceY => minUpForceY;
    public float MaxUpForceY => maxUpForceY;
}

public class Target : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] private float knockUpMovementPercent = 0.5f;

    [SerializeField] private TargetConfig targetConfigInNormalMovement;
    [SerializeField] private TargetConfig targetConfigInUpMovement;

    [Space]
    [SerializeField] private bool isKnocked = false;

    private Vector3 currentPosition;
    private TargetConfig targetConfig = null;

    private float forwardForceZ;
    private float horizontalForceX;

    public static event System.Action OnknockedEvent;   

    private void Update()
    {
        if (isKnocked && targetConfig != null)
        {
            ProcessHorizontalMovementX();
            ProcessForwardMovementZ();
            ProcessGravityY();
        }        
        UpdatePosition();
    }

    private void ProcessHorizontalMovementX()
    {
        currentPosition.x += horizontalForceX * Time.deltaTime;
    }

    private void ProcessForwardMovementZ()
    {
        currentPosition.z += forwardForceZ * Time.deltaTime;
    }

    private void ProcessGravityY()
    {
        currentPosition.y -= targetConfig.Gravity * Time.deltaTime;
    }    

    private void UpdatePosition()
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
            targetConfig = targetConfigInUpMovement;
        }
    }

    private void SetInitialForces()
    {
        if (targetConfig != null)
        {
            SetRandomHorizontalForceX();
            SetRamdomUpForceY();
            SetRandomForwardForceZ();
        }
    }

    private void SetRandomHorizontalForceX()
    {
        float ramdomHorizontalForceX = Random.Range(targetConfig.MinHorizontalForceX, targetConfig.MaxHorizontalForceX);
        int ramdomDirectionX = Random.Range(0, 2) * 2 - 1;
        horizontalForceX = ramdomHorizontalForceX * ramdomDirectionX;
    }

    private void SetRamdomUpForceY()
    {
        float upForceY = Random.Range(targetConfig.MinUpForceY, targetConfig.MaxUpForceY);
        currentPosition.y = upForceY;
    }

    private void SetRandomForwardForceZ()
    {
        forwardForceZ = Random.Range(targetConfig.MinForwardForceZ, targetConfig.MaxForwardForceZ);
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

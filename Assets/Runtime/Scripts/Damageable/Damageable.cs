using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] private TargetSpawner targetSpawner;
    
    private Target target;
    public Target Target => target;

    private void Awake()
    {
        InstantiateTarget();
    }

    private void InstantiateTarget()
    {
        if (targetSpawner)
        {
            target = targetSpawner.InstantiateRandomTarget();
            if (target)
            {
                target.transform.localPosition = Vector3.zero;
            }
        }        
    }
}

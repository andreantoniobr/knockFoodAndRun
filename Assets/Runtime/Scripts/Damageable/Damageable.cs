using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] private TargetSpawner[] targetSpawners;
    
    [SerializeField] private List<Target> targets;
    public List<Target> Targets => targets;

    private void Awake()
    {
        InstantiateAllTargets();
    }

    private void InstantiateAllTargets()
    {
        int targetSpawnersAmount = targetSpawners.Length;
        if (targetSpawnersAmount > 0)
        {
            foreach (TargetSpawner targetSpawner in targetSpawners)
            {
                InstantiateTarget(targetSpawner);
            }
        }
    }

    private void InstantiateTarget(TargetSpawner targetSpawner)
    {
        if (targetSpawner)
        {
            Target target = targetSpawner.InstantiateRandomTarget();
            if (target)
            {
                targets.Add(target);
                target.transform.localPosition = Vector3.zero;
            }
        }        
    }
}

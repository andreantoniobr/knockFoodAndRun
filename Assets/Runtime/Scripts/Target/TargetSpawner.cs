using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [SerializeField] private Target[] targets;   

    private Target InstantiateTarget(int randomTargetIndex)
    {
        return Instantiate(targets[randomTargetIndex], transform);       
    }

    public Target InstantiateRandomTarget()
    {
        Target target = null;
        int targetsAmount = targets.Length;
        if (targetsAmount > 0)
        {
            int randomTargetIndex = Random.Range(0, targetsAmount);
            if (randomTargetIndex >= 0 && randomTargetIndex <= targetsAmount - 1)
            {
                target = InstantiateTarget(randomTargetIndex);
            }
        }
        return target;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HandDirection 
{
    Left,
    Right
}

public class PlayerHandColliderController : MonoBehaviour
{
    [SerializeField] private HandDirection handDirection;
    [SerializeField] private PlayerRigController playerRigController;    

    private void OnTriggerEnter(Collider other)
    {
        Damageable damageable = other.GetComponent<Damageable>();
        if (damageable)
        {
            KnockAllTargets(damageable);
        }
    }

    private void KnockAllTargets(Damageable damageable)
    {
        int targetsAmount = damageable.Targets.Count;
        if (targetsAmount > 0)
        {
            foreach (Target target in damageable.Targets)
            {
                KnockTarget(target);
            }
        }
    }

    private void KnockTarget(Target target)
    {
        if (target)
        {
            playerRigController.StartSlap(target.transform.position, handDirection);
            target.Slap();
        }        
    }
}

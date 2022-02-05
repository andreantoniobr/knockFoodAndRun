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
            //Debug.Log("Collidiu");
            playerRigController.StartSlap(damageable.Target.transform.position, handDirection);
            damageable.Target.Slap();
        }
    }
}

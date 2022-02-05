using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private float gravity = 9.8f;
    [SerializeField] private float forwardForceOnSlap = 30f;    
    [SerializeField] private float upForceOnSlap = 20f;
    [SerializeField] private bool inMovement = false;

    private void Update()
    {
        if (inMovement)
        {
            Vector3 currentPosition = transform.position;
            currentPosition.z += forwardForceOnSlap * Time.deltaTime;
            //currentPosition.y += upForceOnSlap * Time.deltaTime;
            currentPosition.y -= gravity * Time.deltaTime;            
            transform.position = currentPosition;
        }        
    }
    
    private void SetUpForce()
    {
        Vector3 currentPosition = transform.position;
        currentPosition.y += upForceOnSlap;
        transform.position = currentPosition;
    }

    public void Slap()
    {
        inMovement = true;
        //SetUpForce();
    }
}

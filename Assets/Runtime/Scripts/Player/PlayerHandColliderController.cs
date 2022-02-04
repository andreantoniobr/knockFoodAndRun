using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandColliderController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collidiu");
        Target target = other.GetComponent<Target>();
        if (target)
        {
            Debug.Log("Collidiu");
        }
    }
}

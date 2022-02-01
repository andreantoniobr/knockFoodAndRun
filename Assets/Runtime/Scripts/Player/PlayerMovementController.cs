using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] float forwardSpeedZ = 10f;

    private void Update()
    {
        Vector3 currentPosition = transform.position;
        currentPosition += ProcessPlayerMovementZ();
        transform.position = currentPosition;
    }

    private Vector3 ProcessPlayerMovementZ()
    {
        return Vector3.forward * forwardSpeedZ * Time.deltaTime;
    }
}

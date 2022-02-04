using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private float armZ;

    private void LateUpdate()
    {
        Vector3 currentPosition = transform.position;
        currentPosition.z = playerController.transform.position.z - armZ;
        transform.position = currentPosition;
    }
}

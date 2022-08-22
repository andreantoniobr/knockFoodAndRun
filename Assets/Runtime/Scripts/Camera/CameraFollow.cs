using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private float armZ;

    private void LateUpdate()
    {
        if (playerController)
        {
            Vector3 currentPosition = transform.position;
            currentPosition.x = playerController.transform.position.x;
            currentPosition.z = playerController.transform.position.z - armZ;
            transform.position = Vector3.Lerp(transform.position, currentPosition, speed);
            //transform.position = currentPosition;
        }        
    }
}

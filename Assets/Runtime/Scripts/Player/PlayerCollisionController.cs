using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
    public static event Action<Vector3> OnDeathEvent;
    public static event Action OnFinishEvent;

    private void OnTriggerEnter(Collider other)
    {
        CheckColliderIsObstacle(other);
        CheckColliderIsFinishLine(other);
    }

    private void CheckColliderIsObstacle(Collider other)
    {
        Obstacle obstacle = other.GetComponent<Obstacle>();
        if (obstacle)
        {
            OnDeathEvent?.Invoke(transform.position);
        }
    }

    private void CheckColliderIsFinishLine(Collider other)
    {
        FinishLine finishLine = other.GetComponent<FinishLine>();
        if (finishLine)
        {
            OnFinishEvent?.Invoke();
        }
    }
}

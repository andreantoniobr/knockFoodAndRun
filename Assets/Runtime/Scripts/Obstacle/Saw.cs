using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    [SerializeField] private Transform sawModel;
    [SerializeField] private float rotationSpeed = 1f;

    private void FixedUpdate()
    {
        sawModel.Rotate(Vector3.forward * rotationSpeed * Time.fixedDeltaTime);
    }
}

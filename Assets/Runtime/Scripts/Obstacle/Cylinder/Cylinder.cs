using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cylinder : MonoBehaviour
{
    [SerializeField] private Transform cylinderModel;
    [SerializeField] private float rotationSpeed = 1f;

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.fixedDeltaTime);
    }
}

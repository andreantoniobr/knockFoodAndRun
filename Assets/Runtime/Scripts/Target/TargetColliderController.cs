using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetColliderController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        TrackSegment trackSegment = other.GetComponent<TrackSegment>();
        if (trackSegment)
        {
            Debug.Log(transform.position);
            Vector3 position = transform.position;
            position.y = 0.025f;
            FoodSplattersManager.Instance.InstantiateRandomFoodSplatter(position);
        }
    }
}

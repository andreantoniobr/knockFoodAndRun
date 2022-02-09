using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetColliderController : MonoBehaviour
{
    [SerializeField] private float timeToDestroy = 1.5f;

    private void OnTriggerEnter(Collider other)
    {
        TrackSegment trackSegment = other.GetComponent<TrackSegment>();
        if (trackSegment)
        {
            InstantiateFoodSplatter();
            StartCoroutine(SelfDestroyCoroutine());
        }
    }

    private void InstantiateFoodSplatter()
    {
        Vector3 position = transform.position;
        position.y = 0.025f;
        FoodSplattersManager.Instance.InstantiateRandomFoodSplatter(position);
    }

    private IEnumerator SelfDestroyCoroutine()
    {
        yield return new WaitForSeconds(timeToDestroy);
        Destroy(this.transform.gameObject);
    }
}

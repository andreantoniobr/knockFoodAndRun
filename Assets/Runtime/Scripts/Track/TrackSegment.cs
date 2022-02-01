using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackSegment : MonoBehaviour
{
    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform endPosition;

    public Vector3 StartPosition => startPosition.position;
    public Vector3 EndPosition => endPosition.position;
}

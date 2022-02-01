using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackSpawner : MonoBehaviour
{
    [SerializeField] private TrackSegment trackSegmentModel;
    [SerializeField] private int initialTrackSegmentsAmount;

    private TrackSegment lastTrackSegment;

    private void Start()
    {
        InstantiateInitialTrackSegments();
    }

    private void InstantiateInitialTrackSegments()
    {
        if (initialTrackSegmentsAmount > 0)
        {
            for (int i = 0; i < initialTrackSegmentsAmount; i++)
            {
                InstantiateTrackSegment();
            }
        }               
    }

    private void InstantiateTrackSegment()
    {
        TrackSegment currentTrackSegment = Instantiate(trackSegmentModel, transform);
        if (currentTrackSegment)
        {
            currentTrackSegment.transform.position = GetTrackSegmentPosition(currentTrackSegment);
            lastTrackSegment = currentTrackSegment;
        }
    }

    private Vector3 GetTrackSegmentPosition(TrackSegment currentTrackSegment)
    {
        Vector3 position = Vector3.zero;
        if (lastTrackSegment)
        {
            position = lastTrackSegment.EndPosition + ((currentTrackSegment.EndPosition - currentTrackSegment.StartPosition) / 2);
        }
        return position;
    }
}

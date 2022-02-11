using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackSpawner : MonoBehaviour
{
    [SerializeField] private TrackSegment startTrackSegment;

    [Header("Finish")]
    [SerializeField] private TrackSegment[] finishTrackSegments;

    [Space]
    [SerializeField] private TrackSegment trackSegmentModel;
    [SerializeField] private int initialTrackSegmentsAmount;

    private TrackSegment lastTrackSegment;

    private void Start()
    {
        InstantiateTrackSegment(startTrackSegment);
        InstantiateInitialTrackSegments();
        InstantiateTrackSegments(finishTrackSegments);
    }

    private void InstantiateTrackSegments(TrackSegment[] trackSegments)
    {
        int trackSegmentsAmount = trackSegments.Length;
        if (trackSegmentsAmount > 0)
        {
            for (int i = 0; i < trackSegmentsAmount; i++)
            {
                TrackSegment currentTrackSegment = trackSegments[i];
                if (currentTrackSegment)
                {
                    InstantiateTrackSegment(currentTrackSegment);
                }                
            }
        }
    }

    private void InstantiateInitialTrackSegments()
    {
        if (initialTrackSegmentsAmount > 0)
        {
            for (int i = 0; i < initialTrackSegmentsAmount; i++)
            {
                InstantiateTrackSegment(trackSegmentModel);
            }
        }               
    }

    private void InstantiateTrackSegment(TrackSegment trackSegmentModel)
    {
        if (trackSegmentModel)
        {
            TrackSegment currentTrackSegment = Instantiate(trackSegmentModel, transform);
            if (currentTrackSegment)
            {
                currentTrackSegment.transform.position = GetTrackSegmentPosition(currentTrackSegment);
                lastTrackSegment = currentTrackSegment;
            }
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

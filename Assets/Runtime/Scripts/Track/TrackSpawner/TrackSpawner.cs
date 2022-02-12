using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrackSegmentType
{
    Level,
    Obstacle
}

[System.Serializable]
public class RandomTrackSegment
{
    public TrackSegmentType trackSegmentType;
}

public class TrackSpawner : MonoBehaviour
{
    [SerializeField] private TrackSegment startTrackSegment;
    [SerializeField] private TrackSegment[] levelTrackSegments;

    [Header("Ramdom TrackSegments")]
    [SerializeField] private TrackSegment[] TrackSegmentsLevelModels;
    [SerializeField] private TrackSegment[] TrackSegmentsObstacleModels;
    [SerializeField] private RandomTrackSegment[] randomTrackSegments;

    [Header("Finish")]
    [SerializeField] private TrackSegment[] finishTrackSegments;

    [Space]
    [SerializeField] private TrackSegment trackSegmentModel;
    [SerializeField] private int initialTrackSegmentsAmount;

    private TrackSegment lastTrackSegment;

    private void Start()
    {
        InstantiateTrackSegment(startTrackSegment);
        //InstantiateInitialTrackSegments();
        //InstantiateTrackSegments(levelTrackSegments);
        InstantiateRandomTrackSegment(randomTrackSegments);
        InstantiateTrackSegments(finishTrackSegments);
    }

    private void InstantiateRandomTrackSegment(RandomTrackSegment[] randomTrackSegments)
    {
        int randomTrackSegmentsAmount = randomTrackSegments.Length;
        if (randomTrackSegmentsAmount > 0)
        {
            for (int i = 0; i < randomTrackSegmentsAmount; i++)
            {
                RandomTrackSegment randomTrackSegment = randomTrackSegments[i];
                if (randomTrackSegment != null)
                {
                    TrackSegmentType randomTrackSegmentType = randomTrackSegment.trackSegmentType;

                    TrackSegment trackSegment = GetRandomTrackSegment(randomTrackSegmentType);
                    if (trackSegment)
                    {
                        InstantiateTrackSegment(trackSegment);
                    }
                }
            }
        }
    }

    private TrackSegment GetRandomTrackSegment(TrackSegmentType randomTrackSegmentType)
    {
        TrackSegment trackSegment = null;
        if (randomTrackSegmentType == TrackSegmentType.Level)
        {
            trackSegment = GetRandomTrackSegment(TrackSegmentsLevelModels);
        }
        else if (randomTrackSegmentType == TrackSegmentType.Obstacle)
        {
            trackSegment = GetRandomTrackSegment(TrackSegmentsObstacleModels);
        }
        return trackSegment;
    }

    private TrackSegment GetRandomTrackSegment(TrackSegment[] trackSegments)
    {
        TrackSegment trackSegment = null;
        int trackSegmentsAmount = trackSegments.Length;
        if (trackSegmentsAmount > 0)
        {
            int randomTrackSegmentIndex = Random.Range(0, trackSegmentsAmount);
            if (randomTrackSegmentIndex >= 0 && randomTrackSegmentIndex <= trackSegmentsAmount - 1)
            {
                trackSegment = trackSegments[randomTrackSegmentIndex];
            }
        }
        return trackSegment;
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

    private void InstantiateTrackSegment(TrackSegment trackSegment)
    {
        if (trackSegment)
        {
            TrackSegment currentTrackSegment = Instantiate(trackSegment, transform);
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

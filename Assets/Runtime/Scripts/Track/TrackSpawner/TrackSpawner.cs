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
    [SerializeField] private PlayerController playerController;
    [SerializeField] private int minTrackSegmentsInFrontPlayerAmount = 6;
    [SerializeField] private int maxTrackSegmentsAfterPlayerAmount = 1;
    [SerializeField] private float timeToRemoveTrackSegmentsAfterPlayer = 0.5f;

    [Header("TrackSegments")]
    [SerializeField] private TrackSegment startTrackSegment;
    [SerializeField] private TrackSegment[] levelTrackSegments;
    [SerializeField] private TrackSegment[] finishTrackSegments;

    [Header("Random TrackSegments")]
    [SerializeField] private TrackSegment[] TrackSegmentsLevelModels;
    [SerializeField] private TrackSegment[] TrackSegmentsObstacleModels;
    [SerializeField] private RandomTrackSegment[] randomTrackSegments;
    
    private TrackSegment lastTrackSegment;
    private TrackSegment currentTrackSegment;
    private int currentTrackSegmentIndex;

    private List<TrackSegment> spawnedTrackSegments = new List<TrackSegment>();

    private int TrackSegmentsAfterPlayerAmount => currentTrackSegmentIndex;
    private int TrackSegmentsInFrontPlayerAmount => spawnedTrackSegments.Count - (currentTrackSegmentIndex + 1);

    private void Start()
    {
        InstantiateTrackSegment(startTrackSegment);
        //InstantiateInitialTrackSegments();
        //InstantiateTrackSegments(levelTrackSegments);
        InstantiateRandomTrackSegment(randomTrackSegments);
        InstantiateTrackSegments(finishTrackSegments);
        StartCoroutine(RemoveTrackSegmentsAfterPlayerCotroutine());
    }    

    private IEnumerator RemoveTrackSegmentsAfterPlayerCotroutine()
    {
        while (true)
        {
            RemoveTrackSegmentsAfterPlayer();
            yield return new WaitForSeconds(timeToRemoveTrackSegmentsAfterPlayer);
        }
    }

    private void RemoveTrackSegmentsAfterPlayer()
    {
        if (playerController)
        {            
            float playerPositionZ = playerController.transform.position.z;
            //currentTrackSegment = GetCurrentTrackSegment(playerPositionZ);
            currentTrackSegmentIndex = GetCurrentTrackSegmentIndex(playerPositionZ);
            List <TrackSegment> trackSegmentsToRemove = GetTrackSegmentsToRemove(playerPositionZ);
            RemoveTrackSegments(trackSegmentsToRemove);
        }
    }

    private void RemoveTrackSegments(List<TrackSegment> trackSegmentsToRemove)
    {
        int trackSegmentsToRemoveAmount = trackSegmentsToRemove.Count;
        if (trackSegmentsToRemoveAmount > 0)
        {
            for (int i = 0; i < trackSegmentsToRemoveAmount; i++)
            {
                TrackSegment trackSegment = trackSegmentsToRemove[i];
                RemoveTrackSegment(trackSegment);
            }
        }
    }

    private void RemoveTrackSegment(TrackSegment trackSegment)
    {        
        if (trackSegment)
        {
            Destroy(trackSegment.gameObject);
            spawnedTrackSegments.Remove(trackSegment);
        }
    }

    private List<TrackSegment> GetTrackSegmentsToRemove(float playerPositionZ)
    {
        List<TrackSegment> trackSegmentsToRemove = new List<TrackSegment>();
        //Debug.Log(TrackSegmentsAfterPlayerAmount + "-" + maxTrackSegmentsAfterPlayerAmount);

        int trackSegmentsToRemoveAmount = TrackSegmentsAfterPlayerAmount - maxTrackSegmentsAfterPlayerAmount;

        if (trackSegmentsToRemoveAmount > 0)
        {
            for (int i = 0; i < trackSegmentsToRemoveAmount; i++)
            {
                TrackSegment trackSegment = spawnedTrackSegments[i];
                if (IsTrackSegmentAfterPlayer(playerPositionZ, trackSegment))
                {
                    trackSegmentsToRemove.Add(trackSegment);
                }
            }
        }      

        return trackSegmentsToRemove;
    }

    private bool IsTrackSegmentAfterPlayer(float playerPositionZ, TrackSegment trackSegment)
    {
        return trackSegment && playerPositionZ > trackSegment.transform.position.z;
    }

    private TrackSegment GetCurrentTrackSegment(float playerPositionZ)
    {
        TrackSegment currentTrackSegment = null;
        int spawnedTrackSegmentsAmount = spawnedTrackSegments.Count;

        if (spawnedTrackSegmentsAmount > 0)
        {
            for (int i = 0; i < spawnedTrackSegmentsAmount; i++)
            {
                TrackSegment trackSegment = spawnedTrackSegments[i];
                if (IsCurrentTrackSegment(playerPositionZ, trackSegment))
                {
                    currentTrackSegment = trackSegment;
                }
            }
        }

        return currentTrackSegment;
    }

    private int GetCurrentTrackSegmentIndex(float playerPositionZ)
    {
        int trackSegmentIndex = 0;
        int spawnedTrackSegmentsAmount = spawnedTrackSegments.Count;

        if (spawnedTrackSegmentsAmount > 0)
        {
            for (int i = 0; i < spawnedTrackSegmentsAmount; i++)
            {
                TrackSegment trackSegment = spawnedTrackSegments[i];
                if (IsCurrentTrackSegment(playerPositionZ, trackSegment))
                {
                    trackSegmentIndex = i;
                }
            }
        }

        return trackSegmentIndex;
    }

    private bool IsCurrentTrackSegment(float playerPositionZ, TrackSegment trackSegment)
    {
        return trackSegment && playerPositionZ >= trackSegment.StartPosition.z && playerPositionZ <= trackSegment.EndPosition.z;
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

    private void InstantiateTrackSegment(TrackSegment trackSegment)
    {
        if (trackSegment)
        {
            TrackSegment currentTrackSegment = Instantiate(trackSegment, transform);
            if (currentTrackSegment)
            {
                spawnedTrackSegments.Add(currentTrackSegment);
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

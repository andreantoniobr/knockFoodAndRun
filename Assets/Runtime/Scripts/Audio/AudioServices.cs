using UnityEngine;

public class AudioServices : AudioPlayerMain
{
    [SerializeField] private AudioClip knockFoodAudioClip;    

    private void Awake()
    {
        Target.OnknockedEvent += OnknockFood;
    }

    private void OnDestroy()
    {
        Target.OnknockedEvent -= OnknockFood;
    }

    private void OnknockFood()
    {
        PlayAudioCue(knockFoodAudioClip);
    }
}

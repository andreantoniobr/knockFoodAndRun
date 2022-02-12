using UnityEngine;

public class AudioServices : AudioPlayerMain
{
    [SerializeField] private AudioClip knockFoodAudioClip;    
    [SerializeField] private AudioClip playerDeathAudioClip;    

    private void Awake()
    {
        Target.OnknockedEvent += OnknockFood;
        PlayerCollisionController.OnDeathEvent += OnPlayerDeath;
    }

    private void OnDestroy()
    {
        Target.OnknockedEvent -= OnknockFood;
        PlayerCollisionController.OnDeathEvent -= OnPlayerDeath;
    }

    private void OnknockFood()
    {
        PlayAudioCue(knockFoodAudioClip);
    }

    private void OnPlayerDeath(Vector3 position)
    {
        PlayAudioCue(playerDeathAudioClip);
    }
}

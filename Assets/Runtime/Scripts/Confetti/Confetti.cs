using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class Confetti : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;

    private void Awake()
    {
        PlayerCollisionController.OnFinishEvent += OnFinish;
        SetVariables();
    }

    private void OnDestroy()
    {
        PlayerCollisionController.OnFinishEvent -= OnFinish;
    }

    private void OnValidate()
    {
        SetVariables();
    }

    private void OnFinish()
    {
        if (particle)
        {
            particle.Play();
        }
    }

    private void SetVariables()
    {
        if (!particle)
        {
            particle = GetComponent<ParticleSystem>();
        }
    }
}

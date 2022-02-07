using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableSpawner : MonoBehaviour
{
    [SerializeField] private Damageable damageable;

    private void Start()
    {
        if (damageable)
        {
            Damageable damadeableObject = Instantiate(damageable, transform);
            if (damadeableObject)
            {
                Vector3 currentPosition = damadeableObject.transform.position;
                currentPosition.x = 3f * Random.value;
                damadeableObject.transform.position = currentPosition;

                //rotate
                RotateDamageable(damadeableObject, currentPosition);
            }
        }
    }

    private void RotateDamageable(Damageable damadeableObject, Vector3 currentPosition)
    {
        float randomRotatePercent = Random.value;
        if (randomRotatePercent > 0.5f)
        {
            int angle = 90;
            damadeableObject.transform.RotateAround(currentPosition, Vector3.up, angle);
        }
    }
}

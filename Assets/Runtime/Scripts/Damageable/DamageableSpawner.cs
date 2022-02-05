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
            }
        }
    }
}

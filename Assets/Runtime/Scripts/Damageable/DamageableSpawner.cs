using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableSpawner : MonoBehaviour
{
    [SerializeField] private Damageable[] damageables;

    private void Start()
    {
        InstantiateRandomDamageable();
    }

    private void InstantiateRandomDamageable()
    {
        int damageablesAmount = damageables.Length;
        int randomDamageableIndex = Random.Range(0, damageablesAmount);
        if (randomDamageableIndex >= 0 && randomDamageableIndex <= damageablesAmount - 1)
        {
            InstantiateDamageable(damageables[randomDamageableIndex]);
        }
    }

    private void InstantiateDamageable(Damageable damageable)
    {
        Damageable damadeableObject = Instantiate(damageable, transform);
        if (damadeableObject)
        {
            DamageableSetRandomPosition(damadeableObject);
            RotateDamageable(damadeableObject);
        }
    }

    private static void DamageableSetRandomPosition(Damageable damadeableObject)
    {
        Vector3 currentPosition = damadeableObject.transform.position;
        currentPosition.x = 3f * Random.value;
        damadeableObject.transform.position = currentPosition;
    }

    private void RotateDamageable(Damageable damadeableObject)
    {
        int angle = 30;
        float randomRotatePercent = Random.value;
        if (randomRotatePercent > 0.5f)
        {
            angle = 120;            
        }
        damadeableObject.transform.RotateAround(damadeableObject.transform.position, Vector3.up, angle);
    }
}

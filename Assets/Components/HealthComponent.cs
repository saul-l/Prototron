using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour, IDamageable, ISpawnable
{
    [SerializeField] int health = 2;

    public void ApplyDamage(int damageAmount)
    {
        
       
        health -= damageAmount;

        if (health < 0)
        {
            ReturnToPool();
        }
    }

    public void ReturnToPool()
    {
        gameObject.SetActive(false);
    }

    public void SpawnFromPool()
    {
        health = 2;
    }
}

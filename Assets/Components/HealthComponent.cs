using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour, IDamageable
{
    [SerializeField] int health = 2;

    public void ApplyDamage(int damageAmount)
    {
        
       
        health -= damageAmount;
        Debug.Log("damaged " + damageAmount + " current health " + health);
        if (health < 0)
        {
            Destroy(gameObject);
        }
    }
}

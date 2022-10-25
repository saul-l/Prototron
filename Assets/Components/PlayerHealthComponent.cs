using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthComponent : MonoBehaviour, IPlayerDamageable
{
    public float health = 10;

    public void ApplyDamage(int damageAmount)
    {
        health=health-damageAmount;
        if(health<0)
        {
            gameObject.SetActive(false);
        }
    }

}

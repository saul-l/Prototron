/* Health Component handles character health 
 * 
 * Health will reset when object is activated.
 * 
 * No need to be ISpawnable anymore. Waiting for refactoring.
 * 
 * Usage:
 * Set health to desired health.
 * Set spawned damage particle effect prefab to damageEffect
 * Set spawned death particle effect prefab to deathEffect;
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour, IDamageable, ISpawnable
{
    [SerializeField] int health = 2;
    public GameObject deathEffect;
    public GameObject damageEffect;

    private Pool damageEffectPool;
    private Pool deathEffectPool;
    
    private int originalHealth;
    void Awake ()
    {   
        damageEffectPool = PoolHandler.instance.GetPool(damageEffect.gameObject.name, PoolType.ForcedRecycleObjectPool);
        damageEffectPool.PopulatePool(damageEffect, 1, PopulateStyle.Add);

        deathEffectPool = PoolHandler.instance.GetPool(deathEffect.gameObject.name, PoolType.ForcedRecycleObjectPool);
        deathEffectPool.PopulatePool(deathEffect, 1, PopulateStyle.Add);
    }

    void Start ()
    {
        originalHealth = health;
    }

    public void ApplyDamage(int damageAmount)
    {    
        health -= damageAmount;
        if (health < 0)
        {
            ReturnToPool();
        }

        GameObject newDamageEffect = damageEffectPool.GetPooledObject();
         if (newDamageEffect != null)
         {
             newDamageEffect.SetActive(false);
             newDamageEffect.transform.position = transform.position;
             newDamageEffect.transform.rotation = Quaternion.identity;
             newDamageEffect.SetActive(true);    
         }
    }

    public void ReturnToPool()
    {
        GameObject newDeathEffect = deathEffectPool.GetPooledObject();
        if (newDeathEffect != null)
        {
            newDeathEffect.SetActive(false);
            newDeathEffect.transform.position = transform.position;
            newDeathEffect.transform.rotation = Quaternion.identity;
            newDeathEffect.SetActive(true);
        }

        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        health = originalHealth;
    }
    public void SpawnFromPool()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour, IDamageable, ISpawnable
{
    [SerializeField] int health = 2;

    public Pool damageEffectPool;
    public Pool deathEffectPool;
    public GameObject deathEffect;
    public GameObject damageEffect;

    void Awake ()
    {
        damageEffectPool = PoolHandler.instance.GetPool(damageEffect.gameObject.name, PoolTypes.PoolType.ForcedRecycleObjectPool);
        damageEffectPool.PopulatePool(damageEffect, 1);

        deathEffectPool = PoolHandler.instance.GetPool(deathEffect.gameObject.name, PoolTypes.PoolType.ForcedRecycleObjectPool);
        deathEffectPool.PopulatePool(deathEffect, 1);
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
            //newBulletBulletComponent.SpawnFromPool();
            //newBulletBulletComponent.velocity = shootingDirection * bulletSpeed;




        }
    }

    public void ReturnToPool()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        SpawnFromPool();
    }
    public void SpawnFromPool()
    {
        health = 2;
    }
}

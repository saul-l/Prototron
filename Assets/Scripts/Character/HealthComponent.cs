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
 * If deactivateOnDeath is true, the gameobject will be deactivated.
 * If deactivateOnDeath is false, components in destroyonDeath will be destroyed.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour, IDamageable, ISpawnable
{   
    public int health = 2;
    [SerializeField] private bool destroyComponentsOnDeath = true;
    [SerializeField] private Component[] destroyOnDeath;
    [SerializeField] private bool sendMessageOnDamage;
    [SerializeField] private GameObject deathEffect;
    [SerializeField] private GameObject damageEffect;
    [SerializeField] private GameObject deathDecal;
    private Pool damageEffectPool;
    private Pool deathEffectPool;
    private Pool deathDecalPool;
    
    private int originalHealth;

    public event DeathNotify EventDead;

    private Quaternion localUp = Quaternion.Euler(90f, 0f, 0f);
    void Awake ()
    {
        originalHealth = health;
    }

    void Start ()
    {
        damageEffectPool = GameObjectDependencyManager.instance.GetGameObject("PoolHandler").GetComponent<PoolHandler>().GetPool(damageEffect.gameObject.name, PoolType.ForcedRecycleObjectPool);
        damageEffectPool.PopulatePool(damageEffect, 15);

        deathEffectPool = GameObjectDependencyManager.instance.GetGameObject("PoolHandler").GetComponent<PoolHandler>().GetPool(deathEffect.gameObject.name, PoolType.ForcedRecycleObjectPool);
        deathEffectPool.PopulatePool(deathEffect, 15);

        deathDecalPool = GameObjectDependencyManager.instance.GetGameObject("PoolHandler").GetComponent<PoolHandler>().GetPool(deathDecal.gameObject.name, PoolType.ForcedRecycleObjectPool);
        deathDecalPool.PopulatePool(deathDecal, 100);
    }
    public void ApplyDamage(int damageAmount)
    {   
        health -= damageAmount;

        if (sendMessageOnDamage)
        {
            gameObject.SendMessage("OnDamage");
        }

        GameObject newDamageEffect = damageEffectPool.GetPooledObject();
         if (newDamageEffect != null)
         {
             newDamageEffect.SetActive(false);
             newDamageEffect.transform.position = transform.position;
             newDamageEffect.transform.rotation = localUp;
             newDamageEffect.SetActive(true);    
         }

        if (health <= 0)
        {
            Death();
        }

    }

    public void Death()
    {
        if (EventDead != null)
        {
            EventDead.Invoke();
            EventDead = null;
        }

        GameObject newDeathEffect = deathEffectPool.GetPooledObject();
        if (newDeathEffect != null)
        {
            newDeathEffect.SetActive(false);
            newDeathEffect.transform.position = transform.position;
            newDeathEffect.transform.rotation = localUp;
            newDeathEffect.SetActive(true);
        }

        GameObject newDeathDecal = deathDecalPool.GetPooledObject();

        if (newDeathDecal != null)
        {
            // localPosition doesn't work for pooled things. Need to store them somewhere on awake. Using hardcoded tempvalue here for now.

            newDeathDecal.SetActive(false);
            newDeathDecal.transform.position = new Vector3 (transform.position.x, -3.0f, transform.position.z); //+newDeathDecal.transform.localPosition 
            newDeathDecal.transform.rotation = transform.rotation * newDeathDecal.transform.localRotation;
            newDeathDecal.SetActive(true);
        }

        if (destroyComponentsOnDeath)
        {
            foreach (Component component in destroyOnDeath)
            {
                Destroy(component);
            }            
        }        
        else if (TryGetComponent<EnemyController>(out EnemyController enemyController))
        {
            enemyController.dead = true;
        }
        else
        {
            gameObject.SetActive(false);
        }
       
    }

    private void OnEnable()
    {
        health = originalHealth;
    }
    public void ReturnToPool()
    {

    }
    public void SpawnFromPool()
    {
        
    }
}

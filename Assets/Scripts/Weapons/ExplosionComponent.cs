using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionComponent : MonoBehaviour
{

    [SerializeField] private float explosionForce = 5.0f;
    [SerializeField] private float explosionRadius = 15.0f;
    [SerializeField] private float explosionUpwardsForce = 1.0f;
    [SerializeField] private int explosionDamage = 1;
    [SerializeField] private bool active;
    [SerializeField] private float explosionTime = 0.2f;
    [SerializeField] Collider myCollider;
    private float activationTime;
    private void Awake()
    {
        myCollider = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        active = true;
        activationTime = Time.time;
        myCollider.enabled = true;
    }
    private void Update()
    {
        if (Time.time > activationTime + explosionTime)
        {
            myCollider.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.TryGetComponent<IDamageable>(out IDamageable collisionHealthComponent))
        {
            collisionHealthComponent.ApplyDamage(explosionDamage);
        }
       
        if(other.gameObject.TryGetComponent<EnemyController>(out EnemyController enemyController))
        {
            enemyController.knockBack = true;
        }

        other.attachedRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius,explosionUpwardsForce,ForceMode.VelocityChange);
        
    }
    


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionComponent : MonoBehaviour
{

    [SerializeField] private float explosionForce = 5.0f;
    [SerializeField] private float explosionRadius = 15.0f;
    [SerializeField] private float explosionUpwardsForce = 1.0f;
    [SerializeField] private bool active;
    
    [SerializeField] Collider myCollider;
    private void Awake()
    {
        myCollider = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        active = true;
    }
    private void Update()
    {
        if(!active)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
       /* if(other.gameObject.TryGetComponent<IDamageable>(out IDamageable collisionHealthComponent))
        {
         
        }
       */
        if(other.gameObject.TryGetComponent<EnemyController>(out EnemyController enemyController))
        {
            enemyController.knockBack = true;
        }

        other.attachedRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius,explosionUpwardsForce,ForceMode.VelocityChange);
        
    }
    


}

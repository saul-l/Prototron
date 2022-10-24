using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletComponent : MonoBehaviour, ISpawnable
{
    
    [SerializeField] int damage = 1;
    private Vector3 newPosition;
    private RaycastHit hit;
    public Vector3 velocity;
    public bool explosive;
    public float explosionRadius;
    public float bulletForce = 10.0f;
    [SerializeField] private TrailRenderer trailRenderer;

    void Start()
    {
      
    }
    void FixedUpdate()
    {
        TraceToNewPosition();
    }
    public void ReturnToPool()
    {
  
        gameObject.SetActive(false);
        
    }

    public void SpawnFromPool()
    {
        trailRenderer.Clear();

    }

    private void TraceToNewPosition()
    {
        newPosition = transform.position+velocity;
        if(Physics.Linecast(transform.position,newPosition,out hit))
        {
            transform.position = hit.point;
            
            if(hit.collider.attachedRigidbody!=null)
            {             
                hit.collider.attachedRigidbody.AddForce(velocity * bulletForce, ForceMode.VelocityChange);
            }

            if (hit.collider.gameObject.GetComponent<IDamageable>() != null)
            {
                hit.collider.gameObject.GetComponent<IDamageable>().ApplyDamage(damage);
                
            }
            ReturnToPool();
        }
        else
        {
            transform.position = newPosition;
        }
    }

    private void explode(RaycastHit hit)
    {

    }

    private void kineticBulletHit(RaycastHit hit)
    {

    }
}

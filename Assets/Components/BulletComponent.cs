using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletComponent : MonoBehaviour, ISpawnable
{
    
    [SerializeField] private int damage = 1;
    [SerializeField] private TrailRenderer trailRenderer;

    private Vector3 newPosition;
    private RaycastHit hit;

    public float trailRendererTime;
    public Vector3 velocity;
    public bool explosive;
    public float explosionRadius;
    public float bulletForce = 10.0f;
    public bool alive = true;

    void Awake()
    {
        trailRendererTime = trailRenderer.time;

    }

    void FixedUpdate()
    {
        if(alive)TraceToNewPosition();
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

    public void ReturnToPool()
    {
        trailRenderer.time = trailRendererTime * .20f;
        alive = false;
        StartCoroutine(waitAndDeactivateCoroutine());
    }

    public void SpawnFromPool()
    {
        alive = true;
        trailRenderer.time = trailRendererTime;
        trailRenderer.Clear();

    }

    private void explode(RaycastHit hit)
    {

    }

    private void kineticBulletHit(RaycastHit hit)
    {

    }

    IEnumerator waitAndDeactivateCoroutine()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}

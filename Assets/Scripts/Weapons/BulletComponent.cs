using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BulletComponent : MonoBehaviour, ISpawnable
{
    
    
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private string hitAudioevent;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameObject hitEffect;

    private Pool hitEffectPool;

    private Vector3 newPosition;
    private RaycastHit hit;
    public int damage = 1;
    public float trailRendererTime;
    public Vector3 velocity;
    public float explosionRadius;
    public float bulletForce = 10.0f;
    private bool alive = true;
    private Vector3 prevFramePosition;
    void Awake()
    {
        trailRendererTime = trailRenderer.time;
        if(hitEffect != null)
        {           
            hitEffect = Instantiate(hitEffect, transform.position, quaternion.identity);
        }
    }

    void FixedUpdate()
    {
        if(alive)TraceToNewPosition();
    }


    private void TraceToNewPosition()
    {
        newPosition = transform.position+velocity;

        
        if (Physics.Linecast(prevFramePosition, newPosition, out hit, layerMask))
        {
            hitSomething();
        }
        else if (Physics.Linecast(newPosition, prevFramePosition, out hit, layerMask))
        {
            hitSomething();
        }
        else
        {
            Debug.DrawLine(transform.position, newPosition, Color.red, 25);
            prevFramePosition = transform.position;
            transform.position = newPosition;
        }
    }

    private void hitSomething()
    {
        if(hitEffect != null)
        {
            hitEffect.SetActive(false);
            hitEffect.transform.position = transform.position;
            hitEffect.SetActive(true);
        }
        transform.position = hit.point;
        if (hit.collider.attachedRigidbody != null)
        {
            hit.collider.attachedRigidbody.AddForce(velocity * bulletForce, ForceMode.Impulse);
        }
        if (hit.collider.gameObject.GetComponent<IDamageable>() != null)
        {
            SASSimpleAudioSystem.PlayAudioEvent(hitAudioevent, gameObject);
            hit.collider.gameObject.GetComponent<IDamageable>().ApplyDamage(damage);
        }
        ReturnToPool();
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
        prevFramePosition = transform.position;
    }

    IEnumerator waitAndDeactivateCoroutine()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}

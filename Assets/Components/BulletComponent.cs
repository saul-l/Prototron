using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletComponent : MonoBehaviour, ISpawnable
{
    
    [SerializeField] int damage = 1;
    private Vector3 newPosition;
    private RaycastHit hit;
    public Vector3 velocity;
    private TrailRenderer trailRenderer;

    void Start()
    {
        trailRenderer = this.GetComponent<TrailRenderer>();
    }
    void FixedUpdate()
    {
        TraceToNewPosition();
    }
    public void ReturnToPool()
    {   
        gameObject.SetActive(false);
        trailRenderer.Clear();
    }

    private void TraceToNewPosition()
    {
        newPosition = transform.position+velocity;
        if(Physics.Linecast(transform.position,newPosition,out hit))
        {
            transform.position = hit.point;
            if(hit.collider.gameObject.GetComponent<IDamageable>() != null)
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
}

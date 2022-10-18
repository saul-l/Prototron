using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletComponent : MonoBehaviour, ISpawnable
{
    [SerializeField] int damage = 1;

    private void OnCollisionEnter(Collision collision)
    {
 
        if(collision.gameObject.GetComponent<IDamageable>() != null)
        {
            collision.gameObject.GetComponent<IDamageable>().ApplyDamage(damage);
            ReturnToPool();
        }
    }

    public void ReturnToPool()
    {
        gameObject.SetActive(false);
    }
}

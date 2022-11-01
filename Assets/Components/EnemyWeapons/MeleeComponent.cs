using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeComponent : MonoBehaviour, IWeapon
{

    

    private int damage = 1;
    private float knockBack = 1.0f;
    private float rateOfFire = 0.3f;
    public float attackDistance = 2.0f;
    private float attackDuration=0.4f;
    private bool hasHit = false;
    [SerializeField] private Collider myCollider;

    void Start()
    {
        myCollider=GetComponent<Collider>();
        myCollider.enabled = false;
    }
    public void Attack(Vector3 attackDirection)
    {
        myCollider.enabled = true;
    }

    IEnumerator AttackActive()
    {
        float attackEndtime = Time.time + attackDuration;
        while(Time.time < attackEndtime || !hasHit)
        {
            yield return null;
        };
        myCollider.enabled = false;
        hasHit = false;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("attack success");
        if (other.gameObject.GetComponent<IDamageable>() != null)
        {
            other.gameObject.GetComponent<IDamageable>().ApplyDamage(damage);
            hasHit = true;
        }
    }
    
    
    public float RateOfFire
    {
        get
        {
            return rateOfFire;
        }
        set
        {
            rateOfFire = value;
        }
    }



}

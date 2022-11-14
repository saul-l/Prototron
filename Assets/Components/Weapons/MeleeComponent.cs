using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeComponent : MonoBehaviour, IWeapon
{

    

    private int damage = 1;
    private float knockBack = 1.0f;
    private float rateOfFire = 0.3f;
    public float attackDistance = 3.0f;
    public float attackDuration=0.6f;
    private float attackEndtime;
    private bool attacking = false;
    private bool hasHit = false;

    [SerializeField] private string hitAudioevent;
    [SerializeField] private Collider myCollider;
    [SerializeField] private GameObject effectGameObject;
    void Start()
    {
        myCollider=GetComponent<Collider>();
        myCollider.enabled = false;
        if (effectGameObject != null)
            effectGameObject.SetActive(false);
    }
    public void Attack(Vector3 attackDirection)
    {
        if(!attacking)
        {
            hasHit = false;
            attacking = true;
            myCollider.enabled = true;
            if (effectGameObject != null)
            { 
                effectGameObject.SetActive(true);
            }
            attackEndtime = Time.time + attackDuration;
            StartCoroutine(AttackActive());
        }
    }

    IEnumerator AttackActive()
    {
        
        while(Time.time < attackEndtime)
        {
         
            yield return null;
        };
        
        if (effectGameObject != null)
            effectGameObject.SetActive(false);
        myCollider.enabled = false;
        attacking = false;
    }

    void OnTriggerEnter(Collider other)
    {
    
        if (!hasHit && other.gameObject.GetComponent<IDamageable>() != null)
        {
            other.gameObject.GetComponent<IDamageable>().ApplyDamage(damage);
            SimpleAudioWrapper.PlayAudioEvent(hitAudioevent, gameObject);
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

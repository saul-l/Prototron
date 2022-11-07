using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
       
    public MovementComponent movementComponent;
    public GameObject meleeWeapon;
    public GameObject player;
    public Object weapon;
    private MeleeComponent meleeWeaponComponent;
    private Transform myTransform;
    private Transform playerTransform;
    private float lastAttackTime = 0.0f;
    void Awake()
    {
        meleeWeaponComponent = meleeWeapon.GetComponent<MeleeComponent>();
        movementComponent = this.GetComponent<MovementComponent>();
        player = GameObject.Find("Player");      
        playerTransform = player.transform;
        myTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
       
        MovementLogicFollow();
        AttackLogicCloseCombatMelee();


    }

    void AttackLogicStopAndShoot()
    {
        
    }

    void AttackLogicCloseCombatMelee()
        {
            if(meleeWeapon!=null && Time.time >= lastAttackTime+meleeWeaponComponent.RateOfFire)
        {
            if(Vector3.Distance(playerTransform.position, myTransform.position) < meleeWeaponComponent.attackDistance)
            {
                Debug.Log("attack");
                lastAttackTime = Time.time;
                meleeWeaponComponent.Attack(playerTransform.position - transform.position);
            }
        }
        }

    void MovementLogicFollow()
    {
        movementComponent.movementDirection = (playerTransform.position-myTransform.position).normalized;
       
    }
}

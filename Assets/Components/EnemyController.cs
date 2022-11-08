/* EnemyController
 * Holds enemy AI logic and related things.
 * Communicates with MovementComponent, ShootingComponent and MeleeComponent 
 */

using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
       
    public MovementComponent movementComponent;
    public GameObject meleeWeapon;
    public GameObject player;
    public Object weapon;
    [SerializeField] float attackInterval = 1.0f;
    [SerializeField] ShootingComponent shootingComponent;
    [SerializeField] bool shooter;
    [SerializeField] bool melee;
    private MeleeComponent meleeWeaponComponent;
    private Transform myTransform;
    private Transform playerTransform;
    private float lastAttackTime = 0.0f;
    private bool canShoot = true;
    
    void Awake()
    {
        meleeWeaponComponent = meleeWeapon.GetComponent<MeleeComponent>();
        movementComponent = this.GetComponent<MovementComponent>();
        shootingComponent = this.GetComponent<ShootingComponent>();
        player = GameObject.Find("Player");      
        playerTransform = player.transform;
        myTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
       
        MovementLogicFollow();
        if(melee) AttackLogicCloseCombatMelee();
        if (shooter && canShoot) AttackLogicStopAndShoot();


    }

    void AttackLogicStopAndShoot()
    {
        if(Time.time >= lastAttackTime+attackInterval)
        {
            shootingComponent.shootingDirection = (playerTransform.position - myTransform.position).normalized;
            shootingComponent.fire = true;
            lastAttackTime = Time.time;
            canShoot = false;            
        }
    }

    void AttackLogicCloseCombatMelee()
        {
            if(meleeWeapon!=null && Time.time >= lastAttackTime+meleeWeaponComponent.RateOfFire)
        {
            if(Vector3.Distance(playerTransform.position, myTransform.position) < meleeWeaponComponent.attackDistance)
            {
             
                lastAttackTime = Time.time;
                meleeWeaponComponent.Attack(playerTransform.position - transform.position);
            }
        }
        }

    void MovementLogicFollow()
    {
        movementComponent.movementDirection = (playerTransform.position-myTransform.position).normalized;
       
    }

    public void HasShot()
    {
        shootingComponent.fire = false;
        canShoot = true;
    }
}

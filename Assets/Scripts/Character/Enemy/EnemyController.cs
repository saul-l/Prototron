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
    private IShooting shootingComponent;
    private IMovement movementComponent;
    public GameObject meleeWeapon;

    public Object weapon;
    [SerializeField] float attackInterval = 1.0f;
    [SerializeField] bool shooter;
    [SerializeField] bool melee;
    private MeleeComponent meleeWeaponComponent;

    private float lastAttackTime = 0.0f;
    private bool canShoot = true;
    private Vector3 shootingDirection = Vector3.zero;
    public EnemySpawner mySpawner;
    private GameManager gameManager;
    private GameObject targetGameObject;
    private Transform myTransform;
    private Transform targetTransform;
    void Awake()
    {
        meleeWeaponComponent = meleeWeapon.GetComponent<MeleeComponent>();
        movementComponent = this.GetComponent<IMovement>();
        shootingComponent = this.GetComponent<IShooting>();
        myTransform = transform;
        gameManager = GameObjectDependencyManager.instance.GetGameObject("GameManager").GetComponent<GameManager>();
        // save list of players

    }



    void Start()
    {
        SetNearestPlayerAsTarget();
    }
    // Update is called once per frame
    void Update()
    {
        if(targetGameObject != null)
        { 
            MovementLogicFollow();
        
            if (melee)
            {
                AttackLogicCloseCombatMelee();
            }
            if (shooter && canShoot)
            {
                AttackLogicStopAndShoot();
            }
        }
        else
        {
            movementComponent.movementDirection = Vector3.zero;
            SetNearestPlayerAsTarget();
        }
    }

    private void OnDisable()
    {
        if(targetGameObject!=null)
        { 
        targetGameObject.GetComponent<IDamageable>().EventDead -= TargetDeath;
        targetGameObject = null;
        targetTransform = null;
        }

        if (mySpawner != null)
        {
            mySpawner.EnemyDied();
        }
    }

    void SetNearestPlayerAsTarget()
    {

        List<GameObject> players = GameObjectDependencyManager.instance.GetGameObjects("Player");
        if (players != null)
        {
            float distanceToPlayer;
            float? previousDistanceToPlayer = null;

            int asdf = 0;

            foreach (GameObject playerTmp in players)
            {
                Debug.Log("players size: " + asdf + "gameobject " + gameObject.name);
                asdf++;
                distanceToPlayer = Mathf.Abs((playerTmp.GetComponent<Transform>().position - transform.position).sqrMagnitude);

                if (previousDistanceToPlayer.HasValue)
                {
                    if (distanceToPlayer < previousDistanceToPlayer)
                    {
                        previousDistanceToPlayer = distanceToPlayer;
                        targetTransform = playerTmp.transform;
                        targetGameObject = playerTmp.gameObject;
                    }
                }
                else
                {
                    previousDistanceToPlayer = distanceToPlayer;
                    targetTransform = playerTmp.transform;
                    targetGameObject = playerTmp.gameObject;
                }
            }

            if (targetGameObject != null)
            {
                targetGameObject.GetComponent<IDamageable>().EventDead += TargetDeath;
            }
        }
    }

    void TargetDeath()
    {
        targetGameObject.GetComponent<IDamageable>().EventDead -= TargetDeath;
        targetGameObject = null;
        targetTransform = null;
    }
    void AttackLogicStopAndShoot()
    {
        if(Time.time >= lastAttackTime+attackInterval)
        {
            shootingDirection = (targetTransform.position - myTransform.position).normalized;
            shootingDirection.y = 0.0f;
            shootingComponent.shootingDirection = shootingDirection;
            shootingComponent.fire = true;
            lastAttackTime = Time.time;
            canShoot = false;            
        }
        else
        {
            shootingComponent.shootingDirection = Vector3.zero;
        }
    }

    void AttackLogicCloseCombatMelee()
        {
            if(meleeWeapon!=null && Time.time >= lastAttackTime+meleeWeaponComponent.RateOfFire)
        {
            if(Vector3.Distance(targetTransform.position, myTransform.position) < meleeWeaponComponent.attackDistance)
            {
             
                lastAttackTime = Time.time;
                meleeWeaponComponent.Attack(targetTransform.position - transform.position);
            }
        }
        }

    void MovementLogicFollow()
    {
        movementComponent.movementDirection = (targetTransform.position-myTransform.position).normalized;
    }

    public void HasShot()
    {
        shootingComponent.fire = false;
        canShoot = true;
    }
}

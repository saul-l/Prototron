/* EnemyController
 * Holds values needed by enemy AI state machine
 * Communicates with MovementComponent, ShootingComponent and MeleeComponent and EnemyState
 */

using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyState enemyState;
    public EnemyState previousEnemyState;
    private EnemyState lastExecutedEnemyState;
    public GameObject meleeWeapon;

    public Object weapon;
    
    [SerializeField] bool shooter;
    [SerializeField] bool melee;
    public MeleeComponent meleeWeaponComponent;


    private bool canShoot = true;
    private Vector3 shootingDirection = Vector3.zero;
    public EnemySpawner mySpawner;
    private GameManager gameManager;

    // Used by state machine
    public GameObject targetGameObject;
    public Transform myTransform;
    public Transform targetTransform;
    public IShooting shootingComponent;
    public IMovement movementComponent;
    public bool attackActivated = false;
    public bool dead = false;

    // Maybe these should be inside the state machine
    public float attackInterval = 1.0f;
    public float lastAttackTime = 0.0f;
    
    void Awake()
    {
        movementComponent = this.GetComponent<IMovement>();
        if (melee)
        {
            meleeWeaponComponent = meleeWeapon.GetComponent<MeleeComponent>();
        }
        if (shooter)
        {
            shootingComponent = this.GetComponent<IShooting>();
        }
        myTransform = transform;
        gameManager = GameObjectDependencyManager.instance.GetGameObject("GameManager").GetComponent<GameManager>();
    }



    void Start()
    {
      //  SetNearestPlayerAsTarget();
    }
    // Update is called once per frame
    void Update()
    {
        if (enemyState != null)
        {
            lastExecutedEnemyState = enemyState;
            enemyState.Execute(ref enemyState);

            if(lastExecutedEnemyState!=enemyState)
            {
                previousEnemyState = lastExecutedEnemyState;
            }
        }
        /*
        if (targetGameObject != null)
        { 
            MovementLogicFollow();
        
            if (melee && attackActivated)
            {
                AttackLogicCloseCombatMelee();
            }
            if (shooter && canShoot && attackActivated)
            {
                AttackLogicStopAndShoot();
            }
        }
        else
        {
            movementComponent.movementDirection = Vector3.zero;
            SetNearestPlayerAsTarget();
        }
        */
    }
    private void OnTriggerEnter(Collider other)
    {
        if(!attackActivated && other.CompareTag("EnemyAttackActivation"))
        {
            attackActivated = true;
            lastAttackTime = Time.time;
        }
    }
    private void OnDisable()
    {
        attackActivated = false;

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


        }
    }

    
    public void TargetDeath()
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
       /* else
        {
            shootingComponent.shootingDirection = Vector3.zero;
        }
       */
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



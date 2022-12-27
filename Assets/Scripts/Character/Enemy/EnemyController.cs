/* EnemyController
 * Holds values needed by enemy AI state machine
 * Communicates with MovementComponent, ShootingComponent and MeleeComponent and EnemyState
 */

using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyState enemyState;
    public EnemyState previousEnemyState;
    public EnemyState lastExecutedEnemyState;
    public GameObject meleeWeapon;

    public Object weapon;
    
    [SerializeField] bool shooter;
    [SerializeField] bool melee;
    public MeleeComponent meleeWeaponComponent;

    public bool canShoot = true;
    public Vector3 shootingDirection = Vector3.zero;
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
    public bool enterState = true;
    // Maybe these should be inside the state machine
    public float attackInterval = 1.0f;
    public float lastAttackTime = 0.0f;

    public bool knockBack = false;
    public float knockBackTime = 0.0f;
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

        // ensin last executedista tulee enemystate
        // sitten jos last executed on sama kuin enemystate
        // niin previousista tulee lastexecuted
        // 
        if (enemyState != null)
        {
            lastExecutedEnemyState = enemyState;

            if(enterState)
            {
                enemyState.EnterState();
                enterState = false;
            }
            enemyState.Execute(ref enemyState);
            if (lastExecutedEnemyState!=enemyState)
            {
            //    Debug.Log("enemyState " + enemyState + " last executed = " + lastExecutedEnemyState + " previous " + previousEnemyState);
                previousEnemyState = lastExecutedEnemyState;
                lastExecutedEnemyState.ExitState();
                enterState = true;
            }           
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(!attackActivated && other.CompareTag("EnemyAttackActivation"))
        {
            gameObject.layer = LayerMask.NameToLayer("ActivatedEnemies");
            attackActivated = true;
            lastAttackTime = Time.time;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (attackActivated && other.CompareTag("EnemyAttackActivation"))
        {
            attackActivated = false;
            gameObject.layer = LayerMask.NameToLayer("DeactivatedEnemies");
        }
    }
    private void OnDisable()
    {
        attackActivated = false;
        gameObject.layer = LayerMask.NameToLayer("DeactivatedEnemies");
        if (mySpawner != null)
        {
            mySpawner.EnemyDied();
        }
    }

    public void TargetDeath()
    {
        if(targetGameObject!=null)
        { 
            targetGameObject.GetComponent<IDamageable>().EventDead -= TargetDeath;
        }

        targetGameObject = null;
        targetTransform = null;
    }

    public void HasShot()
    {
        shootingComponent.fire = false;
        canShoot = true;
    }
}



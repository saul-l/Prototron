using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class Follow4DirectionMovementEnemyState : EnemyState
{
    [SerializeField] private EnemyState deathState;
    [SerializeField] private EnemyState idleState;
    [SerializeField] private EnemyState actionState;
    [SerializeField] private float actionRadius;
    [SerializeField] private float actionInterval;
    [SerializeField] private float directionChangeDistance;

    private float previousActionTime;

    private bool directionX;
    private float xTarget;
    private float yTargeT;
    private EnemyController enemyController;
    
    private void Start()
    {
        enemyController = GetComponent<EnemyController>();
    }

    public override void EnterState()
    {
        previousActionTime = Time.time;

        if(Random.Range(0, 2) != 0)
        {
            directionX = true;
        }
        else
        {
            directionX = false;
        }

    }
    public override void Execute(ref EnemyState nextState)
    {
        if (enemyController.dead)
        {
            nextState = deathState;
            gameObject.SetActive(false);
        }
        else if (enemyController.targetGameObject == null)
        {            
            nextState = idleState;
        }
        else 
        {            
            if(Time.time >= (previousActionTime + actionInterval ) && Vector3.Distance(enemyController.targetTransform.position, enemyController.myTransform.position) <= actionRadius)
            {        
                nextState = actionState;
            }
            else
            {
                if (directionX == true)
                { 
                    enemyController.movementComponent.movementDirection = (new Vector3 (enemyController.targetTransform.position.x, enemyController.myTransform.position.y, enemyController.transform.position.z) - enemyController.myTransform.position).normalized;
                
                    if(Mathf.Abs(enemyController.targetTransform.position.x - enemyController.myTransform.position.x) < 2)
                    {
                        directionX = false;
                    }
                }
                else
                {
                    enemyController.movementComponent.movementDirection = (new Vector3(enemyController.myTransform.position.x, enemyController.myTransform.position.y, enemyController.targetTransform.position.z) - enemyController.myTransform.position).normalized;
                    
                    if (Mathf.Abs(enemyController.targetTransform.position.z - enemyController.myTransform.position.z) < 2)
                    {
                        directionX = true;
                    }
                }


                nextState = this;
            }
        }
    }
}

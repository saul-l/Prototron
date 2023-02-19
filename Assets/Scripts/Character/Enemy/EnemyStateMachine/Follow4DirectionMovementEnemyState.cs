using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class Follow4DirectionMovementEnemyState : EnemyState
{
    [SerializeField] private EnemyState actionState;
    [SerializeField] private float actionRadius;
    [SerializeField] private float actionInterval;
    [SerializeField] private float directionChangeDistance;

    private float previousActionTime;

    private bool directionX;
    private float xTarget;
    private float yTargeT;

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
        }
        else if (enemyController.targetGameObject == null)
        {            
            nextState = idleState;
        }
        else 
        {            
            if(enemyController.attackActivated && Time.time >= (previousActionTime + actionInterval ) && Vector3.Distance(enemyController.targetTransform.position, enemyController.myTransform.position) <= actionRadius)
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

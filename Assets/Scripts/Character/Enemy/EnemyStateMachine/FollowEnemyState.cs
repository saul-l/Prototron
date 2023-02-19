using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class FollowEnemyState : EnemyState
{
    [SerializeField] private EnemyState actionState;
    [SerializeField] private float actionRadius;
    [SerializeField] private float actionInterval;
    private float previousActionTime;

    
    public override void EnterState()
    {
        previousActionTime = Time.time;
    }
    public override void Execute(ref EnemyState nextState)
    {
        
        if (enemyController.dead)
        {
            nextState = deathState;
        }
        else if (enemyController.knockBack)
        {
            nextState = knockBackState;
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
                enemyController.movementComponent.movementDirection = (enemyController.targetTransform.position - enemyController.myTransform.position).normalized;
                nextState = this;
            }
        }
    }
}

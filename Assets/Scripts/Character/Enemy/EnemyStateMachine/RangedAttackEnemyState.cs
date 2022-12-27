using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.InputSystem;
using UnityEngine;
using UnityEngine.UIElements;

public class RangedAttackEnemyState : EnemyState
{
    [SerializeField] private float stayInRangeDistance;

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
            enemyController.movementComponent.movementDirection = Vector3.zero;
            enemyController.shootingDirection = (enemyController.targetTransform.position - enemyController.myTransform.position).normalized;
            enemyController.shootingDirection.y = 0.0f;
            enemyController.shootingComponent.shootingDirection = enemyController.shootingDirection;

            if (enemyController.shootingComponent.bulletsLeft > 0)
            {
                enemyController.shootingComponent.fire = true;
            }
            else
            {
                if (stayInRangeDistance > 0)
                {
                    if (Vector3.Distance(enemyController.targetTransform.position, enemyController.myTransform.position) > stayInRangeDistance)
                    {
                        nextState = enemyController.previousEnemyState;
                    }
                }
                else
                {
                    nextState = enemyController.previousEnemyState;
                }
            }
        }
    }

    public override void ExitState()
    {
        enemyController.shootingComponent.shootingDirection = Vector3.zero;
        enemyController.shootingDirection = Vector3.zero;
    }
}

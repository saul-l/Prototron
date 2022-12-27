using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MeleeAttackEnemyState : EnemyState
{

    // Update is called once per frame

    public override void Execute(ref EnemyState nextState)
    {        
        if(enemyController.dead)
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
            enemyController.meleeWeaponComponent.Attack(enemyController.targetTransform.position - enemyController.myTransform.position);
            enemyController.movementComponent.movementDirection = (enemyController.targetTransform.position - enemyController.myTransform.position).normalized;
            nextState = enemyController.previousEnemyState;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class DeathNormalEnemyState : EnemyState
{
    public override void EnterState()
    {
        if (enemyController.targetGameObject != null)
        {
            enemyController.targetGameObject.GetComponent<IDamageable>().EventDead -= enemyController.TargetDeath;
            enemyController.targetGameObject = null;
        }

        enemyController.targetTransform = null;
        enemyController.dead = false;
        enemyController.knockBack = false;
        gameObject.layer = LayerMask.NameToLayer("DeactivatedEnemies");
        gameObject.SetActive(false);
    }

    public override void Execute(ref EnemyState nextState)
    {
        nextState = idleState;
    }
}

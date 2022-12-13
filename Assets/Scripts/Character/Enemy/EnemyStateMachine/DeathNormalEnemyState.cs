using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class DeathNormalEnemyState : EnemyState
{

    [SerializeField] private EnemyState idleState;
    private EnemyController enemyController;
    
    private void Start()
    {
        enemyController = GetComponent<EnemyController>();
    }
    public override void Execute(ref EnemyState nextState)
    {        
        if (enemyController.targetGameObject != null)
        {
            enemyController.targetGameObject.GetComponent<IDamageable>().EventDead -= enemyController.TargetDeath;
            enemyController.targetGameObject = null;
        }

            enemyController.targetTransform = null;
            enemyController.dead = false;
            nextState = idleState;
            gameObject.SetActive(false);               
    }
}

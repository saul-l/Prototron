using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MeleeAttackEnemyState : EnemyState
{
    [SerializeField] private EnemyState deathState;
    [SerializeField] private EnemyState idleState;
    private EnemyController enemyController;
    // Update is called once per frame
    private void Start()
    {
        enemyController = GetComponent<EnemyController>();
    }
    public override void Execute(ref EnemyState nextState)
    {        
        if(enemyController.dead)
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
            enemyController.meleeWeaponComponent.Attack(enemyController.targetTransform.position - enemyController.myTransform.position);
            enemyController.movementComponent.movementDirection = (enemyController.targetTransform.position - enemyController.myTransform.position).normalized;
            nextState = enemyController.previousEnemyState;
        }
    }
}

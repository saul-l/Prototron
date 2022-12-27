using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class KnockBackEnemyState : EnemyState
{

    [SerializeField] private float knockBackTime = 0.5f;
    // Update is called once per frame

    public override void EnterState()
    {
        Debug.Log("Enterstate " + Time.time);
        enemyController.knockBackTime = Time.time + knockBackTime;
        enemyController.knockBack = false;
        enemyController.movementComponent.movementDirection = Vector3.zero;

        if (enemyController.shootingComponent != null)
        {
            enemyController.shootingComponent.shootingDirection = Vector3.zero;
        }
    }
    public override void Execute(ref EnemyState nextState)
    {
        enemyController.knockBack = false;
        if(enemyController.dead)
        {
            nextState = deathState;
        }
        else if (enemyController.knockBack)
        {
            enemyController.knockBackTime += knockBackTime;
            enemyController.knockBack = false;
        }        
        else 
        {
            if(Time.time > enemyController.knockBackTime)
            {
                nextState = idleState;
            }
        }
    }
}

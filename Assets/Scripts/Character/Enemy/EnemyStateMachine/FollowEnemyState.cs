using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class FollowEnemyState : EnemyState
{
    [SerializeField] private EnemyState deathState;
    [SerializeField] private EnemyState idleState;
    [SerializeField] private EnemyState actionState;
    [SerializeField] private float actionRadius;

    private EnemyController enemyController;
    // Update is called once per frame
    private void Start()
    {
        enemyController = GetComponent<EnemyController>();
    }
    public override void Execute(ref EnemyState nextState)
    {
        Debug.Log("Execute follow state");
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
            if(Vector3.Distance(enemyController.targetTransform.position, enemyController.myTransform.position) <= actionRadius)
            {
                Debug.Log(Vector3.Distance(enemyController.targetTransform.position, enemyController.myTransform.position));
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

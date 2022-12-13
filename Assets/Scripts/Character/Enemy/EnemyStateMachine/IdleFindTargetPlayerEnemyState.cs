using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleFindTargetPlayerEnemyState : EnemyState
{
    // Idle state where enemy tries to find player target

    [SerializeField] private EnemyState targetFoundState;
    private EnemyController enemyController;

    private void Start()
    {
        enemyController = GetComponent<EnemyController>();
    }

    public override void Execute(ref EnemyState nextState)
    {
        Debug.Log("Execute find new target state");
        // We'll just scan for new players every turn we are idle.
        // This is stupid slow. Should only happen when new players enter the game.
        SetNearestPlayerAsTarget();

        if (enemyController.targetGameObject != null)
        {
            enemyController.targetGameObject.GetComponent<IDamageable>().EventDead += enemyController.TargetDeath;
            nextState = targetFoundState;
        }
        else
        {
            nextState = this;
        }
    }

    void SetNearestPlayerAsTarget()
    {

        List<GameObject> players = GameObjectDependencyManager.instance.GetGameObjects("Player");
        if (players != null)
        {
            float distanceToPlayer;
            float? previousDistanceToPlayer = null;

            int asdf = 0;

            foreach (GameObject playerTmp in players)
            {
                Debug.Log("players size: " + asdf + "gameobject " + gameObject.name);
                asdf++;
                distanceToPlayer = Mathf.Abs((playerTmp.GetComponent<Transform>().position - transform.position).sqrMagnitude);

                if (previousDistanceToPlayer.HasValue)
                {
                    if (distanceToPlayer < previousDistanceToPlayer)
                    {
                        previousDistanceToPlayer = distanceToPlayer;
                        enemyController.targetTransform = playerTmp.transform;
                        enemyController.targetGameObject = playerTmp.gameObject;
                    }
                }
                else
                {
                    previousDistanceToPlayer = distanceToPlayer;
                    enemyController.targetTransform = playerTmp.transform;
                    enemyController.targetGameObject = playerTmp.gameObject;
                }
            }        
        }
    }

}

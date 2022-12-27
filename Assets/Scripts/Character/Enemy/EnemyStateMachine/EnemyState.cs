using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : MonoBehaviour
{
    private string testString;
    public EnemyController enemyController;
    public EnemyState knockBackState;
    public EnemyState deathState;
    public EnemyState idleState;

    public abstract void Execute(ref EnemyState nextState);

    public virtual void EnterState() {}

    public virtual void ExitState() {}

    public virtual void Start()
    {
        if(knockBackState == null) knockBackState = GetComponent<KnockBackEnemyState>();
        
        if(deathState == null) deathState = GetComponent<DeathNormalEnemyState>();
        
        if(idleState == null) idleState = GetComponent<IdleFindTargetPlayerEnemyState>();
        
        enemyController = GetComponent<EnemyController>();
    }

}

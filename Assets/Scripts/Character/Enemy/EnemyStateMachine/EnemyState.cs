using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : MonoBehaviour
{
    public abstract void Execute(ref EnemyState nextState);

    public virtual void EnterState() {}

    public virtual void ExitState() {}
}

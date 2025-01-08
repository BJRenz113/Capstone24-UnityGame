using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemyState
{
    public abstract void EnterState(EnemyStateManager enemyStateManager);
    public abstract void FixedUpdateState(EnemyStateManager enemyStateManager);
    public abstract void ExitState(EnemyStateManager enemyStateManager);
    public virtual void OnColliderEnter2D(EnemyStateManager enemyStateManager, Collider2D other)
    {

    }
}

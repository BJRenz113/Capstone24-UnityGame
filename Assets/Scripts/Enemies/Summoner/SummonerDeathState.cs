using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonerDeathState : BaseEnemyState
{
    public override void EnterState(EnemyStateManager enemyStateManager)
    {
        Object.Destroy(enemyStateManager.GetEnemy().gameObject);
    }

    public override void FixedUpdateState(EnemyStateManager enemyStateManager)
    {

    }

    public override void ExitState(EnemyStateManager enemyStateManager)
    {

    }
}

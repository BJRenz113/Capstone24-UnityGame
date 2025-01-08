using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrathBossSpeedupState : BaseEnemyState
{
    private WrathBoss _boss;
    private bool _ready;

    public override void EnterState(EnemyStateManager enemyStateManager)
    {
        _boss = ((WrathBoss)enemyStateManager.GetEnemy());
        _ready = false;
    }

    public override void FixedUpdateState(EnemyStateManager enemyStateManager)
    {

    }

    public override void ExitState(EnemyStateManager enemyStateManager)
    {

    }
}
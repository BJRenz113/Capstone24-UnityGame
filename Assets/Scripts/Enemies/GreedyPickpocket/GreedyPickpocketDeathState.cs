using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreedyPickpocketDeathState : GenericEnemyDeathState
{
    public override void EnterState(EnemyStateManager enemyStateManager)
    {
        GreedyPickpocket pickpocket = ((GreedyPickpocket)enemyStateManager.GetEnemy());
        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
        player.AddMoney(((int) (pickpocket.StealRecoveryFactor * pickpocket.TotalMoneyStolen)));

        base.EnterState(enemyStateManager);
    }

    public override void FixedUpdateState(EnemyStateManager enemyStateManager)
    {

    }

    public override void ExitState(EnemyStateManager enemyStateManager)
    {

    }
}

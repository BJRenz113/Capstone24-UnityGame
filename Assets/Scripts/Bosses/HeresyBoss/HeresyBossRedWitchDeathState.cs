using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeresyBossRedWitchDeathState : GenericEnemyDeathState
{
    public Player player;
    public override void EnterState(EnemyStateManager enemyStateManager)
    {   
        player.CurrentMoney = player.CurrentMoney + 10;
        player.MaxMoney = player.MaxMoney + 10;
        base.EnterState(enemyStateManager);
    }

    public override void FixedUpdateState(EnemyStateManager enemyStateManager)
    {

    }

    public override void ExitState(EnemyStateManager enemyStateManager)
    {

    }
}
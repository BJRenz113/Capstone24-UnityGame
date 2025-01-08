using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodMosquitoDeathState : GenericEnemyDeathState
{
    public override void EnterState(EnemyStateManager enemyStateManager)
    {
        BloodMosquito mosquito = ((BloodMosquito)enemyStateManager.GetEnemy());
        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
        player.HealHealth(((int) (mosquito.BloodRecoveryFactor * mosquito.TotalDamageDealt)));

        base.EnterState(enemyStateManager);
    }

    public override void FixedUpdateState(EnemyStateManager enemyStateManager)
    {

    }

    public override void ExitState(EnemyStateManager enemyStateManager)
    {

    }
}

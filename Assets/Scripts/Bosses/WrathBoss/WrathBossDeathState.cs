using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrathBossDeathState : GenericEnemyDeathState
{

    public Player player;

    public override void EnterState(EnemyStateManager enemyStateManager)
    {
        WrathBoss boss = (WrathBoss)enemyStateManager.GetEnemy();
        player.CurrentMoney = player.CurrentMoney + 20;
        player.MaxMoney = player.MaxMoney + 30;

        // Destroy active rings
        for (int i = 0; i < boss.ActiveRings.Count; i++)
        {
            GameObject.Destroy(boss.ActiveRings[i]);
        }

        base.EnterState(enemyStateManager);
    }

    public override void FixedUpdateState(EnemyStateManager enemyStateManager)
    {
        // Implement if needed
    }

    public override void ExitState(EnemyStateManager enemyStateManager)
    {
        // Implement if needed
    }
}

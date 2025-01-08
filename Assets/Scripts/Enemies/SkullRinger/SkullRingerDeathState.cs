using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SkullRingerDeathState : GenericEnemyDeathState
{
    public override void EnterState(EnemyStateManager enemyStateManager)
    {
        SkullRinger ringer = (SkullRinger) enemyStateManager.GetEnemy();

        for(int i = 0; i < ringer.Rings.Count; i++)
        {
            ringer.Rings[i].GetComponent<SkullRing>().DestroyRing();
            GameObject.Destroy(ringer.Rings[i]);
        }

        base.EnterState(enemyStateManager);
    }

    public override void FixedUpdateState(EnemyStateManager enemyStateManager)
    {

    }

    public override void ExitState(EnemyStateManager enemyStateManager)
    {

    }
}

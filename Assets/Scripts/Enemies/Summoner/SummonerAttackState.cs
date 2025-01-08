using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonerAttackState : BaseEnemyState
{
    private float summonTimer = 0f;
    private float summonInterval = 1f; // Summon interval in seconds

    public override void EnterState(EnemyStateManager enemyStateManager)
    {
        Debug.Log("Entered Attack State");
    }

    public override void FixedUpdateState(EnemyStateManager enemyStateManager)
    {
        // Increment the summon timer
        summonTimer += Time.fixedDeltaTime;

        // Check if it's time to summon
        if (summonTimer >= summonInterval)
        {
            SummonEnemy();


            // Reset the summon timer
            summonTimer = 0f;
        }
    }

    public override void ExitState(EnemyStateManager enemyStateManager)
    {

    }

    private void SummonEnemy()
    {
        // Summon your enemy here
    }
}

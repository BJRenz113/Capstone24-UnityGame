using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SoulboundWraithDeathState : GenericEnemyDeathState
{
    public override void EnterState(EnemyStateManager enemyStateManager)
    {
        SoulboundWraith wraith = (SoulboundWraith)enemyStateManager.GetEnemy();
        Player playerGameObject = GameObject.FindWithTag("Player").GetComponent<Player>();

        // Delayed spawning of the health pack
        float delay = 0.5f; // Delay time in seconds
        wraith.StartCoroutine(SpawnHealthPackAfterDelay(delay, wraith));

        base.EnterState(enemyStateManager);
    }

    private IEnumerator SpawnHealthPackAfterDelay(float delay, SoulboundWraith wraith)
    {
        yield return new WaitForSeconds(delay);

        // Find the health pack object with the "HealthPack" tag
        GameObject healthPack = GameObject.FindGameObjectWithTag("HealthPack");

        // Check if the health pack is found
        if (healthPack != null)
        {
            // Clone the health pack at the position of the enemy
            GameObject clonedHealthPack = GameObject.Instantiate(healthPack, wraith.transform.position, Quaternion.identity);
        }
    }

    public override void FixedUpdateState(EnemyStateManager enemyStateManager)
    {

    }

    public override void ExitState(EnemyStateManager enemyStateManager)
    {

    }
}
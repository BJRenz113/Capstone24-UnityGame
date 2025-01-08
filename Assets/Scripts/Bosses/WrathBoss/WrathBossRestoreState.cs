using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrathBossRestoreState : BaseEnemyState
{
    private WrathBoss _boss;
    private bool _ready;

    public override void EnterState(EnemyStateManager enemyStateManager)
    {
        _boss = ((WrathBoss)enemyStateManager.GetEnemy());
        _ready = false;

        if (_boss.restoreSound != null)
        {
            AudioSource audioSource = _boss.gameObject.GetComponent<AudioSource>();
            if (audioSource)
            {
                audioSource.PlayOneShot(_boss.restoreSound);
            }
        }
    }

    public override void FixedUpdateState(EnemyStateManager enemyStateManager)
    {

    }

    public override void ExitState(EnemyStateManager enemyStateManager)
    {

    }
}
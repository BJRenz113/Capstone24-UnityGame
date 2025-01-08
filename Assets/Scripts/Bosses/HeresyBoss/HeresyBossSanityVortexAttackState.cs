using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// completely unused
public class HeresyBossSanityVortexAttackState : BaseEnemyState
{
    private HeresyBossBlueWitch _witch;
    private bool _ready;

    public override void EnterState(EnemyStateManager enemyStateManager)
    {
        _witch = ((HeresyBossBlueWitch)enemyStateManager.GetEnemy());
    }

    public override void FixedUpdateState(EnemyStateManager enemyStateManager)
    {
        enemyStateManager.TransitionToState(new HeresyBossBlueWitchPassiveState());
    }

    public override void ExitState(EnemyStateManager enemyStateManager)
    {

    }
}

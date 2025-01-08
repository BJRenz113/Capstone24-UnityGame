using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideBomberPatrolState : BaseEnemyState
{
    private SuicideBomber _bomber;

    public override void EnterState(EnemyStateManager enemyStateManager)
    {
        _bomber = ((SuicideBomber)enemyStateManager.GetEnemy());
        _bomber.gameObject.GetComponent<Animator>().SetInteger("AnimationIndex", 0);
    }

    public override void FixedUpdateState(EnemyStateManager enemyStateManager)
    {
        GameObject playerObject = GameObject.FindWithTag("Player");

        if (Vector2.Distance(playerObject.transform.position, _bomber.gameObject.transform.position) < _bomber.ChaseRadius)
        {
            enemyStateManager.TransitionToState(new SuicideBomberChaseState());
        }
    }

    public override void ExitState(EnemyStateManager enemyStateManager)
    {

    }
}

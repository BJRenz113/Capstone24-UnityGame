using UnityEngine;
using UnityEngine.AI;

public class SuicideBomberChaseState : BaseEnemyState
{
    private NavMeshAgent _agent;
    private SuicideBomber _bomber;

    public override void EnterState(EnemyStateManager enemyStateManager)
    {
        _bomber = ((SuicideBomber)enemyStateManager.GetEnemy());
        _bomber.gameObject.GetComponent<Animator>().SetInteger("AnimationIndex", 1);
        _agent = _bomber.gameObject.GetComponent<NavMeshAgent>();
    }

    public override void FixedUpdateState(EnemyStateManager enemyStateManager)
    {
        Vector3 playerPos = GameObject.FindWithTag("Player").transform.position;
        _agent.SetDestination(playerPos);
        Vector3 enemyPosition = _bomber.gameObject.transform.position;

        if (Vector2.Distance(playerPos, enemyPosition) < _bomber.ExplosionRadius)
        {
            _bomber.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            enemyStateManager.TransitionToState(new SuicideBomberExplodeState());
        }
    }

    public override void ExitState(EnemyStateManager enemyStateManager)
    {
        _agent.ResetPath();
    }
}

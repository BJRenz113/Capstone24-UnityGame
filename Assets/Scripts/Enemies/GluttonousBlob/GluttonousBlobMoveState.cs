using UnityEngine;
using UnityEngine.AI;

public class GluttonousBlobMoveState : BaseEnemyState
{
    private NavMeshAgent _agent;
    private GluttonousBlob _blob;

    public override void EnterState(EnemyStateManager enemyStateManager)
    {
        _blob = ((GluttonousBlob)enemyStateManager.GetEnemy());
        _blob.gameObject.GetComponent<Animator>().SetInteger("AnimationIndex", 1);
        _agent = _blob.gameObject.GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }

    public override void FixedUpdateState(EnemyStateManager enemyStateManager)
    {
        Vector3 playerPos = GameObject.FindWithTag("Player").transform.position;
        _agent.SetDestination(playerPos);
        Vector3 enemyPosition = _blob.gameObject.transform.position;

        if (Vector2.Distance(playerPos, enemyPosition) < _blob.AttackRadius)
        {
            _blob.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            enemyStateManager.TransitionToState(new GluttonousBlobAttackState());
        }
    }

    public override void ExitState(EnemyStateManager enemyStateManager)
    {
        _agent.ResetPath();
    }
}

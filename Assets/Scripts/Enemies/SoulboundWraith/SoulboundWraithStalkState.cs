using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SoulboundWraithStalkState : BaseEnemyState
{
    private int _playerInRangeDuration;
    private float _stalkProximity;
    private int _attackHesitation;
    private float _stalkSpeed;

    public override void EnterState(EnemyStateManager enemyStateManager)
    {
        _attackHesitation = ((SoulboundWraith) enemyStateManager.GetEnemy()).AttackHesitation;
        _stalkProximity = ((SoulboundWraith) enemyStateManager.GetEnemy()).StalkProximity;
        _stalkSpeed = ((SoulboundWraith) enemyStateManager.GetEnemy()).StalkSpeed;

        _playerInRangeDuration = _attackHesitation;
    }

    public override void FixedUpdateState(EnemyStateManager enemyStateManager)
    {
        Enemy enemy = enemyStateManager.GetEnemy();
        GameObject playerGameObject = GameObject.FindWithTag("Player");
        GameObject enemyGameObject = enemy.gameObject;

        Vector2 direction = (playerGameObject.transform.position - enemyGameObject.transform.position).normalized;
        Rigidbody2D rb = enemyGameObject.GetComponent<Rigidbody2D>();

        if (Vector2.Distance(playerGameObject.transform.position, enemyGameObject.transform.position) < _stalkProximity)
        {
            if (_playerInRangeDuration <= 0)
            {
                enemyStateManager.TransitionToState(new SoulboundWraithAttackState(direction, rb));
            }
            else
            {
                _playerInRangeDuration--;
                rb.velocity = direction * _stalkSpeed * Time.fixedDeltaTime;
            }
        }
        else
        {
            enemyStateManager.TransitionToState(new SoulboundWraithPatrolState());
        }
    }

    public override void ExitState(EnemyStateManager enemyStateManager)
    {

    }
}

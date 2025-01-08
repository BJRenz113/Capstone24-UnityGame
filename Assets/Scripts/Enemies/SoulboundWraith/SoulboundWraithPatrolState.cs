using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SoulboundWraithPatrolState : BaseEnemyState
{
    private int _playerInRangeDuration;
    private float _patrolProximity;
    private float _patrolSpeed;
    private int _stalkHesitation;
    private Vector2 _homePoint;
    private Vector2 _patrolPoint;

    public override void EnterState(EnemyStateManager enemyStateManager)
    {
        _stalkHesitation = ((SoulboundWraith) enemyStateManager.GetEnemy()).StalkHesitation;
        _patrolProximity = ((SoulboundWraith) enemyStateManager.GetEnemy()).PatrolProximity;
        _patrolSpeed = ((SoulboundWraith) enemyStateManager.GetEnemy()).PatrolSpeed;
        _playerInRangeDuration = _stalkHesitation;
        _homePoint = ((SoulboundWraith) enemyStateManager.GetEnemy()).HomePosition;
        _patrolPoint = GetRandomPatrolDestination(_homePoint);
    }

    public override void FixedUpdateState(EnemyStateManager enemyStateManager)
    {
        Enemy enemy = enemyStateManager.GetEnemy();
        GameObject playerGameObject = GameObject.FindWithTag("Player");
        GameObject enemyGameObject = enemy.gameObject;

        Vector2 enemyPosition = enemyGameObject.transform.position;

        if (Vector2.Distance(playerGameObject.transform.position, enemyPosition) < _patrolProximity)
        {
            if (_playerInRangeDuration <= 0)
            {
                enemyStateManager.TransitionToState(new SoulboundWraithStalkState());
            }
            else
            {
                _playerInRangeDuration--;
            }
        }
        else
        {
            _playerInRangeDuration = _stalkHesitation;
        }

        if (Vector2.Distance(enemyPosition, _patrolPoint) < 0.1f)
        {
            _patrolPoint = GetRandomPatrolDestination(_homePoint);
        }

        Vector2 temp = new Vector2(enemyPosition.x, enemyPosition.y);
        Vector2 direction = (_patrolPoint - temp).normalized;
        Rigidbody2D rb = enemyGameObject.GetComponent<Rigidbody2D>();

        rb.velocity = direction * _patrolSpeed * Time.fixedDeltaTime;
    }

    private Vector2 GetRandomPatrolDestination(Vector2 initPosition)
    {
        float randomX = Random.Range(initPosition.x - _patrolProximity, initPosition.x + _patrolProximity);
        float randomY = Random.Range(initPosition.y - _patrolProximity, initPosition.y + _patrolProximity);

        while (Vector2.Distance(initPosition, new Vector2(randomX, randomY)) < 0.1f)
        {
            randomX = Random.Range(initPosition.x - _patrolProximity, initPosition.x + _patrolProximity);
            randomY = Random.Range(initPosition.y - _patrolProximity, initPosition.y + _patrolProximity);
        }

        return new Vector2(randomX, randomY);
    }

    public override void ExitState(EnemyStateManager enemyStateManager)
    {

    }
}

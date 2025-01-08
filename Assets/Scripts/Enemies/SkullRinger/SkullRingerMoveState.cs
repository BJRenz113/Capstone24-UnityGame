using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SkullRingerMoveState : BaseEnemyState
{
    private SkullRinger _skullRinger;
    private float _patrolRadius;
    private float _patrolSpeed;
    private Vector2 _homePoint;
    private Vector2 _patrolPoint;
    private float _throwRangeMin;
    private float _throwRangeMax;
    private float _throwSpeed;

    public override void EnterState(EnemyStateManager enemyStateManager)
    {
        _skullRinger = ((SkullRinger) enemyStateManager.GetEnemy());
        _patrolRadius = _skullRinger.PatrolRadius;
        _patrolSpeed = _skullRinger.PatrolSpeed;
        _throwRangeMin = _skullRinger.ThrowRangeMin;
        _throwRangeMax = _skullRinger.ThrowRangeMax;
        _throwSpeed = _skullRinger.ThrowSpeed;

        _homePoint = _skullRinger.HomePosition;
        _patrolPoint = GetRandomPatrolDestination(_homePoint);
    }

    public override void FixedUpdateState(EnemyStateManager enemyStateManager)
    {
        Enemy enemy = enemyStateManager.GetEnemy();
        GameObject playerGameObject = GameObject.FindWithTag("Player");
        GameObject enemyGameObject = enemy.gameObject;

        Vector2 enemyPosition = enemyGameObject.transform.position;

        bool changedStates = false;

        for (int i = 0; i < _skullRinger.Rings.Count; i++)
        {
            _skullRinger.Rings[i].transform.position = enemyGameObject.transform.position + _skullRinger.RingOffset;
        }

        if (Vector2.Distance(enemyPosition, _patrolPoint) < 0.1f)
        {
            _patrolPoint = GetRandomPatrolDestination(_homePoint);

            float dist = Vector2.Distance(enemyPosition, playerGameObject.transform.position);

            if (dist >= _throwRangeMin && dist < _throwRangeMax && _skullRinger.Rings.Count > 0)
            {
                int ringIndex = UnityEngine.Random.Range(0, _skullRinger.Rings.Count);

                GameObject ringObject = _skullRinger.Rings[ringIndex];
                Vector2 ringDirection = (playerGameObject.transform.position - ringObject.transform.position).normalized;

                enemyStateManager.TransitionToState(new SkullRingerThrowState(ringObject, ringDirection, _throwSpeed, _throwRangeMax));
                changedStates = true;
            }
        }

        Vector2 temp = new Vector2(enemyPosition.x, enemyPosition.y);
        Vector2 direction = (_patrolPoint - temp).normalized;
        Rigidbody2D rb = enemyGameObject.GetComponent<Rigidbody2D>();

        if(changedStates)
        {
            rb.velocity = Vector3.zero;
        }
        else
        {
            rb.velocity = direction * _patrolSpeed * Time.fixedDeltaTime;
        }
    }

    private Vector2 GetRandomPatrolDestination(Vector2 initPosition)
    {
        float randomX = UnityEngine.Random.Range(initPosition.x - _patrolRadius, initPosition.x + _patrolRadius);
        float randomY = UnityEngine.Random.Range(initPosition.y - _patrolRadius, initPosition.y + _patrolRadius);

        while(Vector2.Distance(initPosition, new Vector2(randomX, randomY)) < 0.1f)
        {
            randomX = UnityEngine.Random.Range(initPosition.x - _patrolRadius, initPosition.x + _patrolRadius);
            randomY = UnityEngine.Random.Range(initPosition.y - _patrolRadius, initPosition.y + _patrolRadius);
        }

        return new Vector2(randomX, randomY);
    }

    public override void ExitState(EnemyStateManager enemyStateManager)
    {

    }
}

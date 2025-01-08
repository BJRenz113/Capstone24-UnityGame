using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullRingerThrowState : BaseEnemyState
{
    private GameObject _ringObject;
    private Vector2 _direction;
    private float _speed;
    private float _range;
    private bool _returning;


    public SkullRingerThrowState(GameObject ringObject, Vector2 direction, float speed, float range)
    {
        _ringObject = ringObject;
        _direction = direction;
        _speed = speed;
        _range = range;
        _returning = false;
    }

    public override void EnterState(EnemyStateManager enemyStateManager)
    {
    }

    public override void FixedUpdateState(EnemyStateManager enemyStateManager)
    {
        SkullRinger skullRinger = ((SkullRinger)enemyStateManager.GetEnemy());
        GameObject enemyGameObject = skullRinger.gameObject;

        _ringObject.GetComponent<Rigidbody2D>().velocity = _direction * _speed * Time.fixedDeltaTime;

        float dist = Vector2.Distance(enemyGameObject.transform.position + skullRinger.RingOffset, _ringObject.transform.position);

        if (_returning && dist < 0.1)
        {
            _ringObject.transform.position = enemyGameObject.transform.position + skullRinger.RingOffset;
            _ringObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

            enemyStateManager.TransitionToState(new SkullRingerMoveState());
        }
        else if(!_returning && dist >= _range)
        {
            _returning = true;
            _direction *= -1;
        }
    }

    public override void ExitState(EnemyStateManager enemyStateManager)
    {

    }
}

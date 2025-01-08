using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class EnemyStateManager
{
    private BaseEnemyState _currentState;
    private Enemy _enemy;

    // Start is called before the first frame update
    public void Start(Enemy enemy)
    {
        _enemy = enemy;
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        _currentState.FixedUpdateState(this);
    }

    public void OnColliderEnter2D(Collider2D other)
    {
        _currentState.OnColliderEnter2D(this, other);
    }

    public void TransitionToState(BaseEnemyState newState)
    {
        if(_currentState != null)
        {
            _currentState.ExitState(this);
        }

        _currentState = newState;
        _currentState.EnterState(this);
    }

    public Enemy GetEnemy()
    {
        return _enemy;
    }
}
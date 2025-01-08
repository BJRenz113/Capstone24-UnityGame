using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerStateManager
{
    private BasePlayerState _currentState;
    private Player _player;

    // Start is called before the first frame update
    public void Start(Player player)
    {
        _player = player;
        this.TransitionToState(new PlayerMovingState());
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        _currentState.FixedUpdateState(this);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        _currentState.OnTriggerEnter2D(this, other);
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        _currentState.OnTriggerStay2D(this, other);
    }

    public void TransitionToState(BasePlayerState newState)
    {
        if(_currentState != null)
        {
            _currentState.ExitState(this);
        }

        _currentState = newState;
        _currentState.EnterState(this);
    }

    public Player GetPlayer()
    {
        return _player;
    }

    public BasePlayerState GetCurrentState()
    {
        return _currentState;
    }

    public string GetStateName()
    {
        return _currentState.GetType().Name;
    }
}

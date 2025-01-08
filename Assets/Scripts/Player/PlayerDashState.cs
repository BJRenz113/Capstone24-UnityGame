using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : BasePlayerState
{
    private Player _player;
    private bool _ready;
    private Vector2 _direction;
    private List<Enemy> _enemiesHit;

    public PlayerDashState(Vector2 direction)
    {
        _direction = direction;
        _enemiesHit = new List<Enemy>();
    }

    public override void EnterState(PlayerStateManager playerStateManager)
    {
        _player = playerStateManager.GetPlayer();



        int angle = (int)Vector2.SignedAngle(Vector2.right, _direction);
        while (angle < 0)
        {
            angle += 360;
        }
        int directionIndex = ((angle + 45) / 90) % 4;

        _player.gameObject.GetComponent<Animator>().SetInteger("StateIndex", 1);
        _player.gameObject.GetComponent<Animator>().SetInteger("DirectionIndex", directionIndex);

        _player.IsHealthInvulnerable = true;
        _player.CurrentDashes -= 1;
        _ready = false;
        _player.StartCoroutine(PerformDash());

        _player.PlayDashSound();
    }

    private IEnumerator PerformDash()
    {
        _player.gameObject.GetComponent<Rigidbody2D>().velocity = _direction * _player.DashDistance / _player.DashDuration;
        yield return new WaitForSeconds(_player.DashDuration);
        _player.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        _ready = true;
    }

    public override void FixedUpdateState(PlayerStateManager playerStateManager)
    {
        if (_ready) playerStateManager.TransitionToState(new PlayerMovingState());
    }

    public override void ExitState(PlayerStateManager playerStateManager)
    {
        _player.IsHealthInvulnerable = false;
        _player.StartDashCooldown();
    }

    public override void OnTriggerStay2D(PlayerStateManager playerStateManager, Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy == null) return;

        if(!_enemiesHit.Contains(enemy))
        {
            enemy.TakeDamage(_player.DashDamage);
            _enemiesHit.Add(enemy);
        }
    }
}

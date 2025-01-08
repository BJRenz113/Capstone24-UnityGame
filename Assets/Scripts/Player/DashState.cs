/*using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : BasePlayerState
{
    private float _dashDuration;
    private int _dashCount;
    private float _dashSpeed;
    private Vector2 _direction;
    private Rigidbody2D _rb;
    private bool _done;

    public DashState(float dashDuration, int dashCount, float dashSpeed, Vector2 direction, Rigidbody2D rb)
    {
        _dashDuration = dashDuration;
        _dashCount = dashCount - 1;
        _dashSpeed = dashSpeed;
        _direction = direction;
        _rb = rb;
    }

    public override void EnterState(PlayerStateManager playerStateManager)
    {
        playerStateManager.GetPlayer().IsHealthInvulnerable = true;
        _done = false;
        playerStateManager.GetPlayer().StartCoroutine(DashDuration());
    }

    private IEnumerator DashDuration()
    {
        yield return new WaitForSeconds(_dashDuration);
        _done = true;
    }

    public override void FixedUpdateState(PlayerStateManager playerStateManager)
    {
        Player player = playerStateManager.GetPlayer();
        PlayerInputManager input = player.GetPlayerInputManager();

        if (_done)
        {
            _rb.velocity = Vector2.zero;
            input.ClearButtonPressed("Dash");
            playerStateManager.TransitionToState(new PlayerMovingState());
            return;
        }

        float horizontalInput = input.GetHorizontal();
        float verticalInput = input.GetVertical();
        bool directionExists = input.GetDirectionExists();

        // Calculate movement direction
        Vector2 movement = new Vector2(horizontalInput, verticalInput).normalized;

        _rb.velocity = _direction * _dashSpeed * Time.fixedDeltaTime;

        if (input.GetButtonPressed("Dash") && directionExists && _dashCount > 0)
        {
            input.ClearButtonPressed("Dash");
            playerStateManager.TransitionToState(new DashState(player.DashDuration, _dashCount, player.DashSpeed, movement, _rb));
        }
    }

    public override void ExitState(PlayerStateManager playerStateManager)
    {
        playerStateManager.GetPlayer().IsHealthInvulnerable = false;
    }

    public override void OnTriggerEnter2D(PlayerStateManager playerStateManager, Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null) enemy.TakeDamage(10);
    }
}
*/
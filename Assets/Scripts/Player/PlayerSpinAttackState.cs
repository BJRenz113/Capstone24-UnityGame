using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpinAttackState : BasePlayerState
{
    private Player _player;
    private bool _ready;
    private bool _isMoving;
    private Vector2 _direction;
    private Rigidbody2D _rb;
    private GameObject _spinWeaponObject;

    public PlayerSpinAttackState(bool isMoving, Vector2 direction)
    {
        _isMoving = isMoving;
        _direction = direction;
    }

    public override void EnterState(PlayerStateManager playerStateManager)
    {
        _player = playerStateManager.GetPlayer();
        _player.gameObject.GetComponent<Animator>().SetInteger("StateIndex", 5);
        _rb = _player.gameObject.GetComponent<Rigidbody2D>();
        _ready = false;
        _player.StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        _spinWeaponObject = GameObject.Instantiate(_player.SpinAttackWeaponObject);
        _spinWeaponObject.transform.position = _player.gameObject.transform.position;

        yield return new WaitForSeconds(_player.SpinAttackTime);

        GameObject.Destroy(_spinWeaponObject);
        _ready = true;
    }

    public override void FixedUpdateState(PlayerStateManager playerStateManager)
    {
        if (_isMoving) _rb.velocity = _direction * _player.MoveSpeed * _player.SpinAttackMoveSpeedMultiplier * Time.fixedDeltaTime;
        if (_spinWeaponObject != null) _spinWeaponObject.transform.position = _player.gameObject.transform.position;
        if (_ready) playerStateManager.TransitionToState(new PlayerMovingState());
    }

    public override void ExitState(PlayerStateManager playerStateManager)
    {
        _rb.velocity = Vector2.zero;
        _player.StartSpinAttackCooldown();
    }
}

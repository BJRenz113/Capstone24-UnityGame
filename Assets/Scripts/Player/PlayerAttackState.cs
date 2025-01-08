using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : BasePlayerState
{
    private Player _player;
    private bool _ready;
    private Vector2 _direction;

    public PlayerAttackState(Vector2 direction)
    {
        _direction = direction;
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

        _player.gameObject.GetComponent<Animator>().SetInteger("StateIndex", 2);
        _player.gameObject.GetComponent<Animator>().SetInteger("DirectionIndex", directionIndex);

        _ready = false;
        _player.CurrentAttacks -= 1;
        _player.StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        GameObject meleeWeaponObject = GameObject.Instantiate(_player.MeleeWeaponObject);
        Vector3 playerPos = _player.gameObject.transform.position;

        meleeWeaponObject.transform.position = new Vector3(playerPos.x + _direction.x * _player.MeleeWeaponOffset,
            playerPos.y + _direction.y * _player.MeleeWeaponOffset,
            0);

        yield return new WaitForSeconds(_player.MeleeAttackTime);

        GameObject.Destroy(meleeWeaponObject);
        _ready = true;
    }

    public override void FixedUpdateState(PlayerStateManager playerStateManager)
    {
        if (_ready) playerStateManager.TransitionToState(new PlayerMovingState());
    }

    public override void ExitState(PlayerStateManager playerStateManager)
    {
        _player.StartMeleeCooldown();
    }
}

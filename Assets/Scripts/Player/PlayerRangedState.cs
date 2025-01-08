using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangedState : BasePlayerState
{
    private Player _player;
    private bool _ready;
    private Vector2 _direction;

    public PlayerRangedState(Vector2 direction)
    {
        _direction = direction;
    }

    public override void EnterState(PlayerStateManager playerStateManager)
    {
        _player = playerStateManager.GetPlayer();
        _ready = false;

        _player.PlayRangedSound();

        GameObject proj = GameObject.FindWithTag("PlayerRangeProjectile");

        if(proj == null)
        {
            _player.StartCoroutine(ThrowObject());
    
        }
        else
        {
            _player.StartCoroutine(MoveToObject(proj));
        }
    }

    private IEnumerator ThrowObject()
    {
        int angle = (int)Vector2.SignedAngle(Vector2.right, _direction);
        while (angle < 0)
        {
            angle += 360;
        }
        int directionIndex = ((angle + 45) / 90) % 4;

        _player.gameObject.GetComponent<Animator>().SetInteger("StateIndex", 3);
        _player.gameObject.GetComponent<Animator>().SetInteger("DirectionIndex", directionIndex);

        GameObject projectileObject = GameObject.Instantiate(_player.RangedObject);

        float speedX = _direction.x * _player.RangedThrowSpeed;
        float speedY = _direction.y * _player.RangedThrowSpeed;



        projectileObject.transform.position = _player.gameObject.transform.position;
        projectileObject.GetComponent<Rigidbody2D>().velocity = new Vector3(speedX, speedY, 0f) * Time.fixedDeltaTime;

        yield return new WaitForSeconds(_player.RangedThrowStateDuration);
        _ready = true;
    }

    private IEnumerator MoveToObject(GameObject proj)
    {
        int angle = (int)Vector2.SignedAngle(Vector2.right, _direction);
        while (angle < 0)
        {
            angle += 360;
        }
        int directionIndex = ((angle + 45) / 90) % 4;

        _player.gameObject.GetComponent<Animator>().SetInteger("StateIndex", 0);
        _player.gameObject.GetComponent<Animator>().SetInteger("DirectionIndex", directionIndex);

        proj.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        float dist = Vector2.Distance(proj.transform.position, _player.gameObject.transform.position);
        Vector2 direction = (proj.transform.position - _player.gameObject.transform.position).normalized;

        _player.GetComponent<Rigidbody2D>().velocity = direction * dist / _player.RangedMoveStateDuration;

        _player.IsHealthInvulnerable = true;
        yield return new WaitForSeconds(_player.RangedMoveStateDuration);
        _player.IsHealthInvulnerable = false;

        _player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GameObject.Destroy(proj);
        _ready = true;
    }

    public override void FixedUpdateState(PlayerStateManager playerStateManager)
    {
        if (_ready) playerStateManager.TransitionToState(new PlayerMovingState());
    }

    public override void ExitState(PlayerStateManager playerStateManager)
    {

    }
}

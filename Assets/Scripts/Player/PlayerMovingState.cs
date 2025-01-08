using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerMovingState : BasePlayerState
{
    private Rigidbody2D _rb;
    public float maxZPosition = 0f;

    public override void EnterState(PlayerStateManager playerStateManager)
    {
        _rb = playerStateManager.GetPlayer().GetComponent<Rigidbody2D>();
        playerStateManager.GetPlayer().gameObject.GetComponent<Animator>().SetInteger("StateIndex", 0);
    }

    public override void FixedUpdateState(PlayerStateManager playerStateManager)
    {
        // get player and input manager
        Player player = playerStateManager.GetPlayer();
        PlayerInputManager input = player.GetPlayerInputManager();

        bool directionExists = input.GetDirectionExists();
        Vector2 currentDirection = input.GetCurrentDirection();
        Vector2 recentExistsDirection = input.GetRecentExistsDirection();
        int recentExistsDirectionAsIndex = input.GetRecentExistsDirectionAsIndex();

        if (input.GetButtonHeld("HeavyWalk") && directionExists)
        {
            playerStateManager.TransitionToState(new HeavyMovingState());
        }

        else if (input.GetButtonPressed("Dash") && directionExists && player.CurrentDashes > 0)
        {
            input.ClearButtonPressed("Dash");
            playerStateManager.TransitionToState(new PlayerDashState(recentExistsDirection));
        }

        else if (input.GetButtonPressed("MeleeAttack") && player.CurrentAttacks > 0)
        {
            input.ClearButtonPressed("MeleeAttack");
            playerStateManager.TransitionToState(new PlayerAttackState(recentExistsDirection));
        }

        else if (input.GetButtonPressed("RangeAttack"))
        {
            input.ClearButtonPressed("RangeAttack");
            playerStateManager.TransitionToState(new PlayerRangedState(recentExistsDirection));
        }

        else if (input.GetButtonPressed("SpinAttack") && player.CanSpinAttack)
        {
            input.ClearButtonPressed("SpinAttack");
            playerStateManager.TransitionToState(new PlayerSpinAttackState(directionExists, recentExistsDirection));
        }

        else if (input.GetButtonHeld("SelfStabAttack"))
        {
            playerStateManager.TransitionToState(new PlayerSelfStabAttackState());
        }

        else
        {
            if (directionExists)
            {
                player.gameObject.GetComponent<Animator>().SetInteger("StateIndex", 1);
            }
            else
            {
                player.gameObject.GetComponent<Animator>().SetInteger("StateIndex", 0);
            }
            player.gameObject.GetComponent<Animator>().SetInteger("DirectionIndex", recentExistsDirectionAsIndex);
            _rb.velocity = currentDirection * player.MoveSpeed * Time.fixedDeltaTime;
        }


        // Clamp the Z position to ensure the player stays above or at the specified Z position
        Vector3 newPosition = _rb.position;
        newPosition.z = Mathf.Max(newPosition.z, maxZPosition);
        _rb.position = newPosition;

    }

    public override void ExitState(PlayerStateManager playerStateManager)
    {
        _rb.velocity = Vector2.zero;
    }
}

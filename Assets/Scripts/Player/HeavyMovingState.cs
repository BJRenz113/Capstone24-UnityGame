using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class HeavyMovingState : BasePlayerState
{
    private Rigidbody2D rb;
    private int _appliedResistPercentage;
    private int _appliedMinDamageBonus;

    public override void EnterState(PlayerStateManager playerStateManager)
    {
        Player player = playerStateManager.GetPlayer();
        rb = player.GetComponent<Rigidbody2D>();

        _appliedResistPercentage = player.HeavyMoveResistPercentageBonus;
        _appliedMinDamageBonus = player.HeavyMoveMinDamageBonus;
        player.ResistHealthPercentage += _appliedResistPercentage;
        player.MinHealthToHurt += _appliedMinDamageBonus;
    }

    public override void FixedUpdateState(PlayerStateManager playerStateManager)
    {
        // get player and input manager
        Player player = playerStateManager.GetPlayer();
        PlayerInputManager input = player.GetPlayerInputManager();

        float horizontalInput = input.GetHorizontal();
        float verticalInput = input.GetVertical();
        bool directionExists = input.GetDirectionExists();

        // Calculate movement direction
        Vector2 movement = new Vector2(horizontalInput, verticalInput).normalized;

        int angle = (int)Vector2.SignedAngle(Vector2.right, movement);
        while (angle < 0)
        {
            angle += 360;
        }
        int directionIndex = ((angle + 45) / 90) % 4;

        player.gameObject.GetComponent<Animator>().SetInteger("StateIndex", 1);
        player.gameObject.GetComponent<Animator>().SetInteger("DirectionIndex", directionIndex);

        if (input.GetButtonHeld("HeavyWalk") && directionExists)
        {
            rb.velocity = movement * player.HeavyMoveSpeed * Time.fixedDeltaTime;
        }
        else
        {
            playerStateManager.TransitionToState(new PlayerMovingState());
        }
    }

    public override void ExitState(PlayerStateManager playerStateManager)
    {
        rb.velocity = Vector2.zero;
        playerStateManager.GetPlayer().ResistHealthPercentage -= _appliedResistPercentage;
        playerStateManager.GetPlayer().MinHealthToHurt -= _appliedMinDamageBonus;
    }
}

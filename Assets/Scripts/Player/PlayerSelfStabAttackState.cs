using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelfStabAttackState : BasePlayerState
{
    private Player _player;
    private bool _ready;
    private AudioSource audioSource; // Reference to the AudioSource component
    public AudioClip selfStabSound; // Sound effect to play when performing the self stab attack

    public override void EnterState(PlayerStateManager playerStateManager)
    {
        _player = playerStateManager.GetPlayer();
        _player.gameObject.GetComponent<Animator>().SetInteger("StateIndex", 4);
        _ready = false;
        _player.StartCoroutine(DoStab());

        audioSource = _player.GetComponent<AudioSource>(); // Get the AudioSource component attached to the player

        // Play the self stab sound effect
        if (selfStabSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(selfStabSound);
        }
    }

    private IEnumerator DoStab()
    {
        _player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        yield return new WaitForSeconds(_player.SelfStabDuration / 2);
        _player.TakeHealthDamage(_player.SelfStabDamage, true);
        yield return new WaitForSeconds(_player.SelfStabDuration / 2);
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

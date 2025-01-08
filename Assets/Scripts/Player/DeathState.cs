using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathState : BasePlayerState
{
    private Player _player;
    private bool _ready = false;

    public override void EnterState(PlayerStateManager playerStateManager)
    {
        _player = playerStateManager.GetPlayer();
        _player.IsDead = true;
        _player.gameObject.GetComponent<Animator>().SetInteger("StateIndex", 0);
        _player.gameObject.GetComponent<Animator>().SetInteger("DirectionIndex", 3);
        _ready = false;
        _player.StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        // do death shit up here
        yield return new WaitForSeconds(3f);
        _ready = true;
    }

    public override void FixedUpdateState(PlayerStateManager playerStateManager)
    {
        if (_ready) SceneManager.LoadScene(0);
    }

    public override void ExitState(PlayerStateManager playerStateManager)
    {

    }
}

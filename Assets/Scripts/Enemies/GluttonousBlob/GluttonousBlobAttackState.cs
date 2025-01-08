using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GluttonousBlobAttackState : BaseEnemyState
{
    private bool _ready;
    private GluttonousBlob _blob;
    private Animator _animator;
    public AudioClip attackSound;

    public override void EnterState(EnemyStateManager enemyStateManager)
    {
        _ready = false;
        _blob = ((GluttonousBlob)enemyStateManager.GetEnemy());
        _animator = _blob.gameObject.GetComponent<Animator>();
        _blob.StartCoroutine(PerformAttack());

        AudioClip attackSound = ((GluttonousBlob)enemyStateManager.GetEnemy()).attackSound;
        if (attackSound != null)
        {
            AudioSource audioSource = ((GluttonousBlob)enemyStateManager.GetEnemy()).gameObject.GetComponent<AudioSource>();
            if (audioSource)
            {
                audioSource.PlayOneShot(attackSound);
            }
        }

    }

    private IEnumerator PerformAttack()
    {
        _animator.SetInteger("AnimationIndex", 2);

        AudioSource audioSource = _blob.gameObject.GetComponent<AudioSource>();
        if (audioSource && attackSound != null)
        {
            audioSource.PlayOneShot(attackSound);
        }

        yield return new WaitForSeconds(_blob.AttackWait);

        _blob.CanHurt = true;
        yield return new WaitForSeconds(_blob.AttackDuration);

        _blob.CanHurt = false;
        _animator.SetInteger("AnimationIndex", 0);
        yield return new WaitForSeconds(_blob.AttackCooldown);
        _ready = true;
    }

    public override void FixedUpdateState(EnemyStateManager enemyStateManager)
    {
        if (_ready) enemyStateManager.TransitionToState(new GluttonousBlobMoveState());
    }

    public override void ExitState(EnemyStateManager enemyStateManager)
    {

    }
}

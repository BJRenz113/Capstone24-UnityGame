using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeresyBossSkeletonAttackState : BaseEnemyState
{
    private HeresyBossSkeleton _skeleton;
    private bool _ready;

    public AudioClip attackSound;


    public override void EnterState(EnemyStateManager enemyStateManager)
    {
        _skeleton = ((HeresyBossSkeleton)enemyStateManager.GetEnemy());
        _skeleton.CanAttack = true;
        _ready = false;
        _skeleton.StartCoroutine(WaitForStateDuration());

        if (_skeleton.attackSound != null)
        {
            AudioSource audioSource = _skeleton.gameObject.GetComponent<AudioSource>();
            if (audioSource)
            {
                audioSource.PlayOneShot(_skeleton.attackSound);
            }
        }
    }

    private IEnumerator WaitForStateDuration()
    {
        yield return new WaitForSeconds(_skeleton.AttackDuration);
        _ready = true;
    }

    public override void FixedUpdateState(EnemyStateManager enemyStateManager)
    {
        if (_ready) enemyStateManager.TransitionToState(new HeresyBossSkeletonWalkState());
    }

    public override void ExitState(EnemyStateManager enemyStateManager)
    {

    }
}

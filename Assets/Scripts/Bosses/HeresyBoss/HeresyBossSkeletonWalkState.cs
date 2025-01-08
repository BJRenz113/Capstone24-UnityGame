using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HeresyBossSkeletonWalkState : BaseEnemyState
{
    private HeresyBossSkeleton _skeleton;
    private float _delay;
    private bool _ready;

    public override void EnterState(EnemyStateManager enemyStateManager)
    {
        _skeleton = ((HeresyBossSkeleton)enemyStateManager.GetEnemy());
        _skeleton.gameObject.GetComponent<Animator>().SetBool("IsWalking", true);
        _delay = (float) Math.Round(UnityEngine.Random.Range(_skeleton.MinAttackDelay, _skeleton.MaxAttackDelay), 1);
        _ready = false;

        _skeleton.StartCoroutine(WaitForStateDuration());
    }

    private IEnumerator WaitForStateDuration()
    {
        yield return new WaitForSeconds(_delay);
        _ready = true;
    }

    public override void FixedUpdateState(EnemyStateManager enemyStateManager)
    {
        GameObject playerObject = GameObject.FindWithTag("Player");

        Vector2 direction = (playerObject.transform.position - _skeleton.gameObject.transform.position).normalized;
        Rigidbody2D rb = _skeleton.gameObject.GetComponent<Rigidbody2D>();

        if (_ready)
        {
            enemyStateManager.TransitionToState(new HeresyBossSkeletonAttackState());
            rb.velocity = Vector3.zero;
        }
        else
        {
            rb.velocity = direction * _skeleton.WalkSpeed * Time.fixedDeltaTime;
        }
    }

    public override void ExitState(EnemyStateManager enemyStateManager)
    {
        _skeleton.gameObject.GetComponent<Animator>().SetBool("IsWalking", false);
    }
}

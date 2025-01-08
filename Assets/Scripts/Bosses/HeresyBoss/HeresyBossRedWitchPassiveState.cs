using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class HeresyBossRedWitchPassiveState : BaseEnemyState
{
    private HeresyBossRedWitch _witch;
    private bool _minDone;
    private bool _maxDone;

    public override void EnterState(EnemyStateManager enemyStateManager)
    {
        _witch = ((HeresyBossRedWitch) enemyStateManager.GetEnemy());
        _witch.gameObject.GetComponent<Animator>().SetInteger("AnimationIndex", 1);
        _minDone = false;
        _maxDone = false;
        _witch.StartCoroutine(WaitForStateDuration());
    }

    private IEnumerator WaitForStateDuration()
    {
        yield return new WaitForSeconds(_witch.PassiveDurationMin);
        _minDone = true;
        yield return new WaitForSeconds(_witch.PassiveDurationMax - _witch.PassiveDurationMin);
        _maxDone = true;
    }

    public override void FixedUpdateState(EnemyStateManager enemyStateManager)
    {
        GameObject playerObject = GameObject.FindWithTag("Player");

        Vector3 playerPosition = playerObject.transform.position;
        Vector3 enemyPosition = _witch.gameObject.transform.position;

        Vector2 direction = (playerPosition - enemyPosition).normalized;
        Rigidbody2D rb = _witch.gameObject.GetComponent<Rigidbody2D>();

        float dist = Vector2.Distance(enemyPosition, playerPosition);

        rb.velocity = direction * _witch.WalkSpeed * Time.fixedDeltaTime;

        if(_minDone && (_maxDone || dist < _witch.ForceAttackRadius))
        {
            rb.velocity = Vector3.zero;

            if(dist < _witch.FireballRange)
            {
                enemyStateManager.TransitionToState(new HeresyBossFireWaveAttackState());
            }
            else
            {
                enemyStateManager.TransitionToState(new HeresyBossFireballAttackState());
            }
        }
    }

    public override void ExitState(EnemyStateManager enemyStateManager)
    {

    }
}

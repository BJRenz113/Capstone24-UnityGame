using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class HeresyBossWhiteWitchPassiveState : BaseEnemyState
{
    private HeresyBossWhiteWitch _witch;
    private bool _ready;

    public override void EnterState(EnemyStateManager enemyStateManager)
    {
        _witch = ((HeresyBossWhiteWitch) enemyStateManager.GetEnemy());
        _witch.gameObject.GetComponent<Animator>().SetInteger("AnimationIndex", 1);
        _ready = false;
        _witch.StartCoroutine(WaitForStateDuration());
    }

    private IEnumerator WaitForStateDuration()
    {
        yield return new WaitForSeconds(_witch.PassiveDuration);
        _ready = true;
    }

    public override void FixedUpdateState(EnemyStateManager enemyStateManager)
    {
        GameObject playerObject = GameObject.FindWithTag("Player");

        Vector3 playerPosition = playerObject.transform.position;
        Vector3 enemyPosition = _witch.gameObject.transform.position;

        Vector2 direction = (playerPosition - enemyPosition).normalized;
        Rigidbody2D rb = _witch.gameObject.GetComponent<Rigidbody2D>();

        float dist = Vector2.Distance(enemyPosition, playerPosition);

        if (dist < _witch.PlayerAvoidDistance)
        {
            rb.velocity = -direction * _witch.WalkSpeed * Time.fixedDeltaTime;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }

        if(_ready)
        {
            rb.velocity = Vector3.zero;

            System.Random rng = new System.Random();
            int attack = rng.Next(10);

            if(attack == 0 || Vector3.Distance(enemyPosition, Vector3.zero) >= _witch.ForceTeleportRadius)
            {
                enemyStateManager.TransitionToState(new HeresyBossTeleportState());
            }
            else
            {
                enemyStateManager.TransitionToState(new HeresyBossSkeletonSummonState());
            }
        }
    }

    public override void ExitState(EnemyStateManager enemyStateManager)
    {

    }
}

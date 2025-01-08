using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreacheryBossPlayerVoidAttackState : BaseEnemyState
{
    private TreacheryBoss _treacheryBoss;
    private GameObject _voidObject;
    private float _startTime;
    private float _stateDuration;
    private bool _ready;

    public override void EnterState(EnemyStateManager enemyStateManager)
    {
        _treacheryBoss = ((TreacheryBoss)enemyStateManager.GetEnemy());
        _treacheryBoss.gameObject.GetComponent<Animator>().SetInteger("AnimationIndex", 1);
        _voidObject = _treacheryBoss.MediumPlayerVoidObject;
        _stateDuration = _treacheryBoss.PlayerVoidDuration;
        _ready = false;
        _startTime = Time.time;

        _treacheryBoss.StartCoroutine(WaitForStateDuration());

        if (_treacheryBoss.playerVoidAttackSound!= null)
        {
            AudioSource audioSource = _treacheryBoss.gameObject.GetComponent<AudioSource>();
            if (audioSource)
            {
                audioSource.PlayOneShot(_treacheryBoss.playerVoidAttackSound);
            }
        }
    }

    private IEnumerator WaitForStateDuration()
    {
        GameObject voidObject = GameObject.Instantiate(_voidObject);
        GameObject playerObject = GameObject.FindWithTag("Player");

        voidObject.transform.position = playerObject.transform.position;

        yield return new WaitForSeconds(_stateDuration);
        _ready = true;
    }

    public override void FixedUpdateState(EnemyStateManager enemyStateManager)
    {
        if (_ready) enemyStateManager.TransitionToState(new TreacheryBossIdleState());
    }

    public override void ExitState(EnemyStateManager enemyStateManager)
    {

    }
}

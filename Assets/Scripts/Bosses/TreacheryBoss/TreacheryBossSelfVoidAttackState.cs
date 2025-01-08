using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreacheryBossSelfVoidAttackState : BaseEnemyState
{
    private TreacheryBoss _treacheryBoss;
    private GameObject _voidObject;
    private float _startTime;
    private float _stateDuration;
    private bool _ready;

    public override void EnterState(EnemyStateManager enemyStateManager)
    {
        _treacheryBoss = ((TreacheryBoss)enemyStateManager.GetEnemy());
        _treacheryBoss.gameObject.GetComponent<Animator>().SetInteger("AnimationIndex", 2);
        _voidObject = _treacheryBoss.LargeSelfVoidObject;
        _stateDuration = _treacheryBoss.SelfVoidDuration;
        _ready = false;
        _startTime = Time.time;

        _treacheryBoss.StartCoroutine(WaitForStateDuration());

        
        if (_treacheryBoss.selfVoidAttackSound!= null)
        {
            AudioSource audioSource = _treacheryBoss.gameObject.GetComponent<AudioSource>();
            if (audioSource)
            {
                audioSource.PlayOneShot(_treacheryBoss.selfVoidAttackSound);
            }
        }

    }

    private IEnumerator WaitForStateDuration()
    {
        GameObject voidObject = GameObject.Instantiate(_voidObject);
        voidObject.transform.SetParent(_treacheryBoss.gameObject.transform);
        voidObject.transform.localPosition = new Vector3(0, 0, 0);

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

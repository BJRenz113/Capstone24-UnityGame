using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreacheryBossTorchExtinguishState : BaseEnemyState
{
    private TreacheryBoss _treacheryBoss;
    private GameObject _voidObject;
    private float _stateDuration;
    private bool _ready;

    public override void EnterState(EnemyStateManager enemyStateManager)
    {
        _treacheryBoss = ((TreacheryBoss)enemyStateManager.GetEnemy());
        _treacheryBoss.gameObject.GetComponent<Animator>().SetInteger("AnimationIndex", 2);
        _voidObject = _treacheryBoss.MediumPlayerVoidObject;
        _stateDuration = _treacheryBoss.TorchExtinguishCooldown;

        GameObject[] torches = GameObject.FindGameObjectsWithTag("TreacheryBossTorch");

        for (int i = 0; i < torches.Length; i++)
        {
            torches[i].GetComponent<TreacheryBossTorch>().CurrentLightPercentage = 0;
            GameObject voidObject = GameObject.Instantiate(_voidObject);
            voidObject.transform.position = torches[i].transform.position;
        }

        _treacheryBoss.StartCoroutine(WaitForStateDuration());
    }

    private IEnumerator WaitForStateDuration()
    {
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreacheryBossScatterVoidAttackState : BaseEnemyState
{
    private TreacheryBoss _treacheryBoss;
    private GameObject _voidObject;
    private float _radius;
    private float _rate;
    private int _count;
    private int _playerFrequency;
    private bool _ready;

    public override void EnterState(EnemyStateManager enemyStateManager)
    {
        _treacheryBoss = ((TreacheryBoss)enemyStateManager.GetEnemy());
        _treacheryBoss.gameObject.GetComponent<Animator>().SetInteger("AnimationIndex", 1);
        _voidObject = _treacheryBoss.SmallMiscVoidObject;

        _radius = _treacheryBoss.ScatterVoidRadius;
        _rate = _treacheryBoss.ScatterVoidRate;
        _count = _treacheryBoss.ScatterVoidCount;
        _playerFrequency = _treacheryBoss.ScatterVoidPlayerFrequency;

        _ready = false;
        _treacheryBoss.StartCoroutine(PerformAttack());

        
        if (_treacheryBoss.scatterVoidAttackSound!= null)
        {
            AudioSource audioSource = _treacheryBoss.gameObject.GetComponent<AudioSource>();
            if (audioSource)
            {
                audioSource.PlayOneShot(_treacheryBoss.scatterVoidAttackSound);
            }
        }

    }

    private IEnumerator PerformAttack()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");

        for(int i = 0; i < _count; i++)
        {
            Vector3 playerPosition = playerObject.transform.position;

            GameObject voidObject = GameObject.Instantiate(_voidObject);

            if (i % _playerFrequency == 0)
            {
                voidObject.transform.position = playerPosition;
            }
            else
            {
                float randomX = Random.Range(playerPosition.x - _radius, playerPosition.x + _radius);
                float randomY = Random.Range(playerPosition.y - _radius, playerPosition.y + _radius);

                voidObject.transform.position = new Vector3(randomX, randomY, 0);
            }

            yield return new WaitForSeconds(_rate);
        }

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

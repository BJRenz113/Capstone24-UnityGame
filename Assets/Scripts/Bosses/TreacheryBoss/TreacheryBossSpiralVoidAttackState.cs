using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreacheryBossSpiralVoidAttackState : BaseEnemyState
{
    private TreacheryBoss _treacheryBoss;
    private GameObject _voidObject;
    private bool _ready;
    private int _loops;
    private int _angleGranularity;
    private int _simultaneousSpawns;
    private float _rate;
    private float _cooldown;
    private float _spacing;

    public override void EnterState(EnemyStateManager enemyStateManager)
    {
        _treacheryBoss = ((TreacheryBoss)enemyStateManager.GetEnemy());
        _treacheryBoss.gameObject.GetComponent<Animator>().SetInteger("AnimationIndex", 1);
        _voidObject = _treacheryBoss.SmallMiscVoidObject;
        _loops = _treacheryBoss.SpiralVoidLoops;
        _angleGranularity = _treacheryBoss.SpiralVoidAngleGranularity;
        _rate = _treacheryBoss.SpiralVoidRate;
        _spacing = _treacheryBoss.SpiralVoidSpacing;
        _cooldown = _treacheryBoss.SpiralVoidCooldown;
        _simultaneousSpawns = _treacheryBoss.SpiralVoidSimultaneousSpawns;

        _ready = false;
        _treacheryBoss.StartCoroutine(PerformAttack());

        
        if (_treacheryBoss.spiralVoidAttack!= null)
        {
            AudioSource audioSource = _treacheryBoss.gameObject.GetComponent<AudioSource>();
            if (audioSource)
            {
                audioSource.PlayOneShot(_treacheryBoss.spiralVoidAttack);
            }
        }
    }

    private IEnumerator PerformAttack()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");

        System.Random rng = new System.Random();
        int angle = rng.Next(360) % _angleGranularity * (360 / _angleGranularity);

        float bossX = _treacheryBoss.gameObject.transform.position.x;
        float bossY = _treacheryBoss.gameObject.transform.position.y;

        for (int i = 0; i < _loops * _angleGranularity; i += _simultaneousSpawns)
        {
            for(int j = 0; j < _simultaneousSpawns; j++)
            {
                GameObject voidObject = GameObject.Instantiate(_voidObject);

                float xOffset = Mathf.Cos(angle * Mathf.PI / 180) * i / _angleGranularity * _spacing;
                float yOffset = Mathf.Sin(angle * Mathf.PI / 180) * i / _angleGranularity * _spacing;

                angle = (angle + (360 / _angleGranularity)) % 360;

                voidObject.transform.position = new Vector3(bossX + xOffset, bossY + yOffset, 0);
            }

            yield return new WaitForSeconds(_rate);
        }

        yield return new WaitForSeconds(_cooldown);

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

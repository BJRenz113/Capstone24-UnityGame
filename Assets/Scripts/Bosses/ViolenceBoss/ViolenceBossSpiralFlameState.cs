using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViolenceBossSpiralFlameState : BaseEnemyState
{
    private ViolenceBoss _boss;
    private bool _ready;

    public override void EnterState(EnemyStateManager enemyStateManager)
    {
        _boss = ((ViolenceBoss)enemyStateManager.GetEnemy());
        _ready = false;
        _boss.StartCoroutine(PerformState());

        if (_boss.spiralFlameSound != null)
        {
            AudioSource audioSource = _boss.gameObject.GetComponent<AudioSource>();
            if (audioSource)
            {
                audioSource.PlayOneShot(_boss.spiralFlameSound);
            }
        }

    }

    private IEnumerator PerformState()
    {
        _boss.gameObject.GetComponent<Animator>().SetInteger("AnimationIndex", 2);
        Vector3 bossPos = _boss.gameObject.transform.position;
        float angle = UnityEngine.Random.Range(0f, 360f);
        float angleGranularity = _boss.SpiralFlameAngleGranularity;
        int loops = _boss.SpiralFlameLoops;
        int spirals = _boss.SpiralFlameSpirals;
        float spacing = _boss.SpiralFlameLoopSpacing;

        float batchTime = _boss.SpiralFlameBatchTime;

        for (int i = 0; i < angleGranularity * loops; i++)
        {
            for (int j = 0; j < spirals; j++)
            {
                GameObject flameObject = GameObject.Instantiate(_boss.SpiralFlameObject);

                float xOffset = Mathf.Cos(angle * Mathf.PI / 180) * (angleGranularity * loops - i) / angleGranularity * spacing;
                float yOffset = Mathf.Sin(angle * Mathf.PI / 180) * (angleGranularity * loops - i) / angleGranularity * spacing;
                angle = (angle + (360f / spirals)) % 360f;

                flameObject.transform.position = new Vector3(bossPos.x + xOffset, bossPos.y + yOffset, 0);
            }

            angle = (angle + (360f / angleGranularity)) % 360f;

            yield return new WaitForSeconds(batchTime);
            batchTime *= _boss.SpiralFlameSpeedupFactor;
        }

        yield return new WaitForSeconds(_boss.SpiralFlameCooldown);
        _ready = true;
    }

    public override void FixedUpdateState(EnemyStateManager enemyStateManager)
    {
        if (_ready) enemyStateManager.TransitionToState(new ViolenceBossErraticState());
    }

    public override void ExitState(EnemyStateManager enemyStateManager)
    {

    }
}

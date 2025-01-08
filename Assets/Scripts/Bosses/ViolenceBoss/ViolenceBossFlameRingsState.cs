using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViolenceBossFlameRingsState : BaseEnemyState
{
    private ViolenceBoss _boss;
    private bool _ready;

    public override void EnterState(EnemyStateManager enemyStateManager)
    {
        _boss = ((ViolenceBoss)enemyStateManager.GetEnemy());
        _ready = false;
        _boss.StartCoroutine(PerformState());

        if (_boss.flameRingsSound != null)
        {
            AudioSource audioSource = _boss.gameObject.GetComponent<AudioSource>();
            if (audioSource)
            {
                audioSource.PlayOneShot(_boss.flameRingsSound);
            }
        }
    }

    private IEnumerator PerformState()
    {
        _boss.gameObject.GetComponent<Animator>().SetInteger("AnimationIndex", 2);
        float waveTime = _boss.FlameRingsSummonTime;

        GameObject playerObject = GameObject.FindWithTag("Player");
        Vector3 playerPos = playerObject.transform.position;
        float flameRingsRadius = _boss.FlameRingsRadiusMax;

        for (int i = 0; i < _boss.FlameRingsCounts.Count; i++)
        {
            float angle = UnityEngine.Random.Range(0f, 360f);
            int counts = _boss.FlameRingsCounts[i];

            for (int j = 0; j < counts; j++)
            {
                GameObject wispObject = GameObject.Instantiate(_boss.FlameRingsWispObject);

                float xOffset = Mathf.Cos(angle * Mathf.PI / 180) * flameRingsRadius;
                float yOffset = Mathf.Sin(angle * Mathf.PI / 180) * flameRingsRadius;

                angle = (angle + (360f / counts)) % 360f;

                wispObject.transform.position = new Vector3(playerPos.x + xOffset, playerPos.y + yOffset, 0);
            }

            flameRingsRadius -= (_boss.FlameRingsRadiusMax - _boss.FlameRingsRadiusMin) / (_boss.FlameRingsCounts.Count - 1);
            yield return new WaitForSeconds(waveTime);
            waveTime *= _boss.FlameRingsSpeedupFactor;
        }

        yield return new WaitForSeconds(_boss.FlameRingsCooldown);
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

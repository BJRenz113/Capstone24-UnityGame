using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViolenceBossOHKOAttackState : BaseEnemyState
{
    private ViolenceBoss _boss;
    private bool _ready;

    // Define an AudioSource variable to play sound effects
    public AudioSource soundEffect;

    public override void EnterState(EnemyStateManager enemyStateManager)
    {
        _boss = ((ViolenceBoss)enemyStateManager.GetEnemy());
        _ready = false;
        _boss.StartCoroutine(PerformState());

        // Play the sound effect when entering the state
        if (soundEffect != null)
        {
            soundEffect.Play();
        }
    }

    private IEnumerator PerformState()
    {
        Vector3 bossPos = _boss.gameObject.transform.position;
        float shakeTime = _boss.OHKOAttackShakeTime;

        for(int i = 0; i < _boss.OHKOAttackShakes; i++)
        {
            float angle = UnityEngine.Random.Range(0f, 360f);
            float xOffset = Mathf.Cos(angle * Mathf.PI / 180) * _boss.OHKOAttackShakeDist;
            float yOffset = Mathf.Sin(angle * Mathf.PI / 180) * _boss.OHKOAttackShakeDist;

            _boss.gameObject.transform.position = new Vector3(bossPos.x + xOffset, bossPos.y + yOffset, bossPos.z);

            yield return new WaitForSeconds(shakeTime);
            shakeTime *= _boss.OHKOAttackShakeSpeedupFactor;
        }

        _boss.gameObject.GetComponent<Animator>().SetInteger("AnimationIndex", 4);
        _boss.OHKOAttackIsActive = true;
        yield return new WaitForSeconds(_boss.OHKOAttackCooldown);
        _boss.OHKOAttackIsActive = false;

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeresyBossFireWaveAttackState : BaseEnemyState
{
    private HeresyBossRedWitch _witch;
    private bool _ready;
    public AudioClip fireWaveSound;

    public override void EnterState(EnemyStateManager enemyStateManager)
    {
        _witch = ((HeresyBossRedWitch)enemyStateManager.GetEnemy());
        _witch.gameObject.GetComponent<Animator>().SetInteger("AnimationIndex", 3);
        _ready = false;
        _witch.StartCoroutine(DoAttack());

        if (_witch.fireballSound != null)
        {
            AudioSource audioSource = _witch.gameObject.GetComponent<AudioSource>();
            if (audioSource)
            {
                audioSource.PlayOneShot(_witch.fireWaveSound);
            }
        }
    }

    private IEnumerator DoAttack()
    {
        for(int i = 0; i < _witch.FireWaves; i++)
        {
            float bossX = _witch.gameObject.transform.position.x;
            float bossY = _witch.gameObject.transform.position.y;

            GameObject playerObject = GameObject.FindWithTag("Player");
            Vector3 playerPosition = playerObject.transform.position;

            float angleToPlayer = Mathf.Atan2(playerPosition.y - bossY, playerPosition.x - bossX) * 180 / Mathf.PI;
            float angle = angleToPlayer - _witch.FireWavesAngleCoverage / 2;

            for (int j = 0; j < _witch.FireWavesEmbersPerWave; j++)
            {
                GameObject emberObject = GameObject.Instantiate(_witch.EmberObject);

                float xMultiplier = Mathf.Cos(angle * Mathf.PI / 180);
                float yMultiplier = Mathf.Sin(angle * Mathf.PI / 180);
                float speed = _witch.FireWavesEmberSpeed;

                angle += 1.0f * _witch.FireWavesAngleCoverage / _witch.FireWavesEmbersPerWave;

                emberObject.transform.position = _witch.gameObject.transform.position;
                emberObject.GetComponent<Rigidbody2D>().velocity = new Vector2(xMultiplier, yMultiplier) * speed * Time.fixedDeltaTime;
            }
            yield return new WaitForSeconds(_witch.FireWavesTimeBetweenWave);
        }
        _ready = true;
    }

    public override void FixedUpdateState(EnemyStateManager enemyStateManager)
    {
        if (_ready) enemyStateManager.TransitionToState(new HeresyBossRedWitchPassiveState());
    }

    public override void ExitState(EnemyStateManager enemyStateManager)
    {

    }
}

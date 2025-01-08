using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeresyBossSpikePatternAttackState : BaseEnemyState
{
    private HeresyBossBlueWitch _witch;
    private bool _ready;

    public override void EnterState(EnemyStateManager enemyStateManager)
    {
        _witch = ((HeresyBossBlueWitch)enemyStateManager.GetEnemy());
        _witch.gameObject.GetComponent<Animator>().SetInteger("AnimationIndex", 2);
        _ready = false;

        System.Random rng = new System.Random();
        int attack = rng.Next(3);

        if (_witch.spikePatternSound != null)
        {
            AudioSource audioSource = _witch.gameObject.GetComponent<AudioSource>();
            if (audioSource)
            {
                audioSource.PlayOneShot(_witch.spikePatternSound);
            }
        }

        switch(attack)
        {
            case 0:
                _witch.StartCoroutine(SpikeLines());
                break;
            case 1:
                _witch.StartCoroutine(SpikeCircles());
                break;
            case 2:
                _witch.StartCoroutine(SpikeTrap());
                break;
            default:
                _ready = true;
                break;
        }


    }

    private IEnumerator SpikeLines()
    {
        int lines = _witch.SpikeLines;

        float bossX = _witch.gameObject.transform.position.x;
        float bossY = _witch.gameObject.transform.position.y;

        GameObject playerObject = GameObject.FindWithTag("Player");
        Vector3 playerPosition = playerObject.transform.position;

        float angle = Mathf.Atan2(playerPosition.y - bossY, playerPosition.x - bossX) * 180 / Mathf.PI;
        float dist = _witch.SpikeLineDistance;
        int angleOffset = _witch.SpikeLineAngleOffset;
        int spikeCount = _witch.SpikeLineSpikeCount;

        for (int i = 0; i < spikeCount; i++)
        {
            for(int j = 0; j < _witch.SpikeLines; j++)
            {
                // theres definitely a better way to do this but i dont care
                if(j == 0)
                {
                    GameObject spikeObject = GameObject.Instantiate(_witch.SpikeObject);

                    float xOffset = Mathf.Cos(angle * Mathf.PI / 180) * (dist / spikeCount) * (i + 1);
                    float yOffset = Mathf.Sin(angle * Mathf.PI / 180) * (dist / spikeCount) * (i + 1);

                    spikeObject.transform.position = new Vector3(bossX + xOffset, bossY + yOffset, 0);
                }
                else
                {
                    GameObject spikeObject = GameObject.Instantiate(_witch.SpikeObject);

                    float xOffset = Mathf.Cos((angle + angleOffset * j) * Mathf.PI / 180) * (dist / spikeCount) * (i + 1);
                    float yOffset = Mathf.Sin((angle + angleOffset * j) * Mathf.PI / 180) * (dist / spikeCount) * (i + 1);

                    spikeObject.transform.position = new Vector3(bossX + xOffset, bossY + yOffset, 0);

                    spikeObject = GameObject.Instantiate(_witch.SpikeObject);

                    xOffset = Mathf.Cos((angle - angleOffset * j) * Mathf.PI / 180) * (dist / spikeCount) * (i + 1);
                    yOffset = Mathf.Sin((angle - angleOffset * j) * Mathf.PI / 180) * (dist / spikeCount) * (i + 1);

                    spikeObject.transform.position = new Vector3(bossX + xOffset, bossY + yOffset, 0);
                }
            }

            yield return new WaitForSeconds(_witch.SpikeLineDuration / spikeCount);
        }

        _ready = true;
    }

    private IEnumerator SpikeCircles()
    {
        int circles = _witch.SpikeCircles;
        float bossX = _witch.gameObject.transform.position.x;
        float bossY = _witch.gameObject.transform.position.y;
        float angle;

        for(int i = 0; i < circles; i++)
        {
            angle = UnityEngine.Random.Range(0f, 360f);

            for(int j = 0; j < _witch.SpikeCircleSpikeCount; j++)
            {
                GameObject spikeObject = GameObject.Instantiate(_witch.SpikeObject);

                float xOffset = Mathf.Cos(angle * Mathf.PI / 180) * (i + 1) * _witch.SpikeCircleSpacing;
                float yOffset = Mathf.Sin(angle * Mathf.PI / 180) * (i + 1) * _witch.SpikeCircleSpacing;

                angle = (angle + (360f / _witch.SpikeCircleSpikeCount)) % 360f;

                spikeObject.transform.position = new Vector3(bossX + xOffset, bossY + yOffset, 0);

                yield return new WaitForSeconds(_witch.SpikeCircleTimeBetweenSpikes);
            }

            yield return new WaitForSeconds(_witch.SpikeCircleTimeBetweenCircles);
        }

        yield return new WaitForSeconds(_witch.SpikeCircleCooldown);

        _ready = true;
    }

    private IEnumerator SpikeTrap()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");

        float bossX = _witch.gameObject.transform.position.x;
        float bossY = _witch.gameObject.transform.position.y;

        float playerX = playerObject.transform.position.x;
        float playerY = playerObject.transform.position.y;

        float trapAngle = UnityEngine.Random.Range(0f, 360f);

        for (int i = 0; i < _witch.SpikeCircleSpikeCount; i++)
        {
            GameObject spikeObject = GameObject.Instantiate(_witch.SpikeObject);

            float xOffset = Mathf.Cos(trapAngle * Mathf.PI / 180) * _witch.SpikeTrapRadius;
            float yOffset = Mathf.Sin(trapAngle * Mathf.PI / 180) * _witch.SpikeTrapRadius;

            trapAngle = (trapAngle + (360f / _witch.SpikeTrapSpikeCount)) % 360f;

            spikeObject.transform.position = new Vector3(playerX + xOffset, playerY + yOffset, 0);

            yield return new WaitForSeconds(_witch.SpikeTrapTimeBetweenSpikes);
        }

        yield return new WaitForSeconds(_witch.SpikeTrapLineDelay);

        int spikeCount = _witch.SpikeLineSpikeCount;
        float dist = _witch.SpikeLineDistance;
        float angleToPlayer = Mathf.Atan2(playerY - bossY, playerX - bossX) * 180 / Mathf.PI;

        for (int i = 0; i < spikeCount; i++)
        {
            GameObject spikeObject = GameObject.Instantiate(_witch.SpikeObject);

            float xOffset = Mathf.Cos(angleToPlayer * Mathf.PI / 180) * (dist / spikeCount) * (i + 1);
            float yOffset = Mathf.Sin(angleToPlayer * Mathf.PI / 180) * (dist / spikeCount) * (i + 1);

            spikeObject.transform.position = new Vector3(bossX + xOffset, bossY + yOffset, 0);

            yield return new WaitForSeconds(_witch.SpikeLineDuration / spikeCount);
        }

        _ready = true;
    }

    public override void FixedUpdateState(EnemyStateManager enemyStateManager)
    {
        if (_ready) enemyStateManager.TransitionToState(new HeresyBossBlueWitchPassiveState());
    }

    public override void ExitState(EnemyStateManager enemyStateManager)
    {

    }
}

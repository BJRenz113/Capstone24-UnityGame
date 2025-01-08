using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViolenceBossDarkStormState : BaseEnemyState
{
    private ViolenceBoss _boss;
    private bool _ready;

    public override void EnterState(EnemyStateManager enemyStateManager)
    {
        _boss = ((ViolenceBoss)enemyStateManager.GetEnemy());
        _ready = false;
        _boss.StartCoroutine(PerformState());
    }

    private IEnumerator PerformState()
    {
        _boss.gameObject.GetComponent<Animator>().SetInteger("AnimationIndex", 1);
        float waveTime = _boss.DarkStormWaveTime;

        for (int i = 0; i < _boss.DarkStormWaveCounts.Count; i++)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            Vector3 bossPos = _boss.gameObject.transform.position;
            Vector3 playerPos = playerObject.transform.position;

            float angle = Mathf.Atan2(playerPos.y - bossPos.y, playerPos.x - bossPos.y) * 180 / Mathf.PI;
            int counts = _boss.DarkStormWaveCounts[i];

            for (int j = 0; j < counts; j++)
            {
                GameObject wispObject = GameObject.Instantiate(_boss.DarkStormWispObject);
                float speed = wispObject.GetComponent<ViolenceBossDarkStormWisp>().MoveSpeed * Time.fixedDeltaTime;

                float xMult = Mathf.Cos(angle * Mathf.PI / 180);
                float yMult = Mathf.Sin(angle * Mathf.PI / 180);

                angle = (angle + (360f / counts)) % 360f;

                wispObject.transform.position = new Vector3(bossPos.x, bossPos.y, 0);
                wispObject.GetComponent<Rigidbody2D>().velocity = new Vector2(speed * xMult, speed * yMult);
            }

            yield return new WaitForSeconds(waveTime);
            waveTime *= _boss.DarkStormSpeedupFactor;
        }

        yield return new WaitForSeconds(_boss.DarkStormEndPause);

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

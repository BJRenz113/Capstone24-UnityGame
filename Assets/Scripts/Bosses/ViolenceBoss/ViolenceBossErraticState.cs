using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ViolenceBossErraticState : BaseEnemyState
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
        _boss.gameObject.GetComponent<Animator>().SetInteger("AnimationIndex", 0);
        float teleportDuration = _boss.ErraticTeleportTime;
        GameObject playerObject = GameObject.FindWithTag("Player");
        float teleportRadius = _boss.ErraticTeleportRadiusMax;
        Vector3 playerPos = playerObject.transform.position;
        float angle = UnityEngine.Random.Range(0f, 360f);

        for (int i = 0; i < _boss.ErraticTeleportCount; i++)
        {
            angle = (angle + UnityEngine.Random.Range(_boss.ErraticTeleportMinAngleChange, 360f - _boss.ErraticTeleportMinAngleChange)) % 360;
            playerPos = playerObject.transform.position;

            float xOffset = Mathf.Cos(angle * Mathf.PI / 180) * teleportRadius;
            float yOffset = Mathf.Sin(angle * Mathf.PI / 180) * teleportRadius;
            _boss.gameObject.transform.position = new Vector3(playerPos.x + xOffset, playerPos.y + yOffset, 0);

            teleportRadius -= (_boss.ErraticTeleportRadiusMax - _boss.ErraticTeleportRadiusMin) / (_boss.ErraticTeleportCount - 1);

            yield return new WaitForSeconds(teleportDuration);
            teleportDuration *= _boss.ErraticTeleportSpeedupFactor;
        }

        yield return new WaitForSeconds(_boss.ErraticTeleportEndPause);

        _ready = true;
    }

    public override void FixedUpdateState(EnemyStateManager enemyStateManager)
    {
        if (_ready)
        {
            if (_boss.AttackQueue.Count == 0)
            {
                int[] attackArray = Shuffle(new int[] { 0, 1, 2, 3 });
                _boss.AttackQueue = attackArray.ToList();
                _boss.AttackQueue.Add(4);
            }

            int attackIndex = _boss.AttackQueue[0];
            _boss.AttackQueue.RemoveAt(0);

            switch (attackIndex)
            {
                case 0:
                    enemyStateManager.TransitionToState(new ViolenceBossDarkStormState());
                    break;
                case 1:
                    enemyStateManager.TransitionToState(new ViolenceBossFlameRingsState());
                    break;
                case 2:
                    enemyStateManager.TransitionToState(new ViolenceBossScytheThrowState());
                    break;
                case 3:
                    enemyStateManager.TransitionToState(new ViolenceBossSpiralFlameState());
                    break;
                case 4:
                    enemyStateManager.TransitionToState(new ViolenceBossOHKOAttackState());
                    break;
                default:
                    enemyStateManager.TransitionToState(new ViolenceBossErraticState());
                    break;
            }
        }
    }

    private void RegenerateAttackQueue()
    {

    }

    private static int[] Shuffle(int[] array)
    {
        System.Random rng = new System.Random();

        int[] shuffledArray = new int[array.Length];
        Array.Copy(array, shuffledArray, array.Length);

        int n = shuffledArray.Length;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            int value = shuffledArray[k];
            shuffledArray[k] = shuffledArray[n];
            shuffledArray[n] = value;
        }

        return shuffledArray;
    }

    public override void ExitState(EnemyStateManager enemyStateManager)
    {

    }
}

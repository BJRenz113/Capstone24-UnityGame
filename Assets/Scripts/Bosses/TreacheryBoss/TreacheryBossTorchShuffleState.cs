using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreacheryBossTorchShuffleState : BaseEnemyState
{
    private bool _ready;
    private float _stateDuration;

    public override void EnterState(EnemyStateManager enemyStateManager)
    {
        _stateDuration = ((TreacheryBoss)enemyStateManager.GetEnemy()).TorchShuffleCooldown;
        ((TreacheryBoss)enemyStateManager.GetEnemy()).gameObject.GetComponent<Animator>().SetInteger("AnimationIndex", 2);

        GameObject[] torchPositions = GameObject.FindGameObjectsWithTag("TreacheryBossTorchPosition");
        GameObject[] torches = GameObject.FindGameObjectsWithTag("TreacheryBossTorch");

        torchPositions = Shuffle(torchPositions);

        for (int i = 0; i < torches.Length; i++)
        {
            torches[i].transform.position = torchPositions[i].transform.position;
        }

        ((TreacheryBoss)enemyStateManager.GetEnemy()).StartCoroutine(WaitForStateDuration());
    }

    private static GameObject[] Shuffle(GameObject[] array)
    {
        System.Random rng = new System.Random();

        GameObject[] shuffledArray = new GameObject[array.Length];
        Array.Copy(array, shuffledArray, array.Length);

        int n = shuffledArray.Length;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            GameObject value = shuffledArray[k];
            shuffledArray[k] = shuffledArray[n];
            shuffledArray[n] = value;
        }

        return shuffledArray;
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

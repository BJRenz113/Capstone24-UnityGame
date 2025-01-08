using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GluttonousBlobDeathState : GenericEnemyDeathState
{
    public AudioClip splitSound;

    public override void EnterState(EnemyStateManager enemyStateManager)
    {
        // spawn mini clones unless size 1
        GluttonousBlob baseBlob = ((GluttonousBlob)enemyStateManager.GetEnemy());

        if (baseBlob.Size > 1)
        {
            System.Random rng = new System.Random();
            int angle = rng.Next(360);

            Transform transform = baseBlob.gameObject.transform;

            float baseBlobPosX = transform.position.x;
            float baseBlobPosY = transform.position.y;
            float baseBlobScaleX = transform.localScale.x;
            float baseBlobScaleY = transform.localScale.y;

            for (int i = 0; i < baseBlob.Splits; i++)
            {
                GameObject newBlobObject = GameObject.Instantiate(baseBlob.gameObject);
                GluttonousBlob newBlob = newBlobObject.GetComponent<GluttonousBlob>();
                newBlob.ExecuteOnSplit();

                bool foundSpot = false;

                while (foundSpot == false)
                {
                    angle = rng.Next(360);

                    float radius = newBlob.InitialSplitRadius * Mathf.Pow(newBlob.SplitScaleMultiplier, newBlob.OriginalSize - newBlob.Size);

                    float xOffset = Mathf.Cos(angle * Mathf.PI / 180) * radius;
                    float yOffset = Mathf.Sin(angle * Mathf.PI / 180) * radius;

                    Vector3 potentialPosition = new Vector3(baseBlobPosX + xOffset, baseBlobPosY + yOffset, 0);

                    UnityEngine.AI.NavMeshHit hit;
                    foundSpot = UnityEngine.AI.NavMesh.SamplePosition(potentialPosition, out hit, 0.1f, UnityEngine.AI.NavMesh.AllAreas);
                    newBlobObject.GetComponent<UnityEngine.AI.NavMeshAgent>().Warp(hit.position);
                }

                // Play the split sound effect
                AudioSource audioSource = newBlobObject.GetComponent<AudioSource>();
                if (audioSource && splitSound != null)
                {
                    audioSource.PlayOneShot(splitSound);
                }
            }
        }
        else
        {
            Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
        }

        base.EnterState(enemyStateManager);
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

    public override void FixedUpdateState(EnemyStateManager enemyStateManager)
    {

    }

    public override void ExitState(EnemyStateManager enemyStateManager)
    {

    }
}

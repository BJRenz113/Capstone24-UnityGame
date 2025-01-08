using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeresyBossSkeletonSummonState : BaseEnemyState
{
    private HeresyBossWhiteWitch _witch;
    private bool _ready;
    public AudioClip summonSound;

    public override void EnterState(EnemyStateManager enemyStateManager)
    {
        _witch = ((HeresyBossWhiteWitch)enemyStateManager.GetEnemy());
        _ready = false;
        _witch.StartCoroutine(BeginSummoning());

        if (_witch.summonSound != null)
        {
            AudioSource audioSource = _witch.gameObject.GetComponent<AudioSource>();
            if (audioSource)
            {
                audioSource.PlayOneShot(_witch.summonSound);
            }
        }


    }

    private IEnumerator BeginSummoning()
    {
        int numSkeletons = GameObject.FindGameObjectsWithTag("HeresyBossSkeleton").Length;

        if (numSkeletons < _witch.MaxSkeletons) _witch.gameObject.GetComponent<Animator>().SetInteger("AnimationIndex", 2);

        float bossX = _witch.transform.position.x;
        float bossY = _witch.transform.position.y;

        for(int i = 0; i < _witch.SkeletonsToSummon && numSkeletons + i < _witch.MaxSkeletons; i++)
        {
            GameObject skeleton = GameObject.Instantiate(_witch.SkeletonObject);

            float angle = UnityEngine.Random.Range(0f, 360f);
            float dist = UnityEngine.Random.Range(_witch.SkeletonSummonMinRadius, _witch.SkeletonSummonMaxRadius);

            float xOffset = Mathf.Cos(angle * Mathf.PI / 180) * dist;
            float yOffset = Mathf.Sin(angle * Mathf.PI / 180) * dist;

            skeleton.transform.position = new Vector3(bossX + xOffset, bossY + yOffset, 0);

            skeleton.GetComponent<HeresyBossSkeleton>().EmitEmbers = _witch.SkeletonsEmitEmbers;
            skeleton.GetComponent<HeresyBossSkeleton>().DealSanity = _witch.SkeletonsDealSanity;

            yield return new WaitForSeconds(_witch.SummonRate);
        }

        _ready = true;
    }

    public override void FixedUpdateState(EnemyStateManager enemyStateManager)
    {
        if (_ready) enemyStateManager.TransitionToState(new HeresyBossWhiteWitchPassiveState());
    }

    public override void ExitState(EnemyStateManager enemyStateManager)
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodMosquitoBitingState : BaseEnemyState
{
    private BloodMosquito _mosquito;
    private bool _ready;

    public override void EnterState(EnemyStateManager enemyStateManager)
    {
        _mosquito = ((BloodMosquito)enemyStateManager.GetEnemy());
        _mosquito.StartCoroutine(MainState());
        _mosquito.gameObject.GetComponent<CircleCollider2D>().radius *= 3;

        AudioClip attackSound = ((BloodMosquito)enemyStateManager.GetEnemy()).attackSound;
        if (attackSound != null) {
            AudioSource audioSource = ((BloodMosquito)enemyStateManager.GetEnemy()).gameObject.GetComponent<AudioSource>();
            if (audioSource)
            {
                audioSource.PlayOneShot(attackSound);
            }
        }   
    }

    private IEnumerator MainState()
    {
        _mosquito.gameObject.GetComponent<Animator>().SetInteger("AnimationIndex", 4);
        AudioSource audioSource = _mosquito.gameObject.GetComponent<AudioSource>();

        yield return new WaitForSeconds(_mosquito.BiteDelay);

        for (int i = 0; i < _mosquito.BiteCount; i++)
        {
            Player player = GameObject.FindWithTag("Player").GetComponent<Player>();

            int actualBiteDamage = player.CurrentHealth;
            player.TakeHealthDamage(_mosquito.BiteDamage);

            actualBiteDamage -= player.CurrentHealth;
            _mosquito.TotalDamageDealt += actualBiteDamage;

            yield return new WaitForSeconds(_mosquito.TimeBetweenBites);
        }
        _ready = true;
    }

    public override void FixedUpdateState(EnemyStateManager enemyStateManager)
    {
        if(_ready)
        {
            enemyStateManager.TransitionToState(new BloodMosquitoMoveState());
        }
        else
        {
            _mosquito.gameObject.transform.position = GameObject.FindWithTag("Player").transform.position;
        }
    }

    public override void ExitState(EnemyStateManager enemyStateManager)
    {
        _mosquito.gameObject.GetComponent<CircleCollider2D>().radius /= 3;
    }
}

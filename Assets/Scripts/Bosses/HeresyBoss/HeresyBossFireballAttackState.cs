using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeresyBossFireballAttackState : BaseEnemyState
{
    private HeresyBossRedWitch _witch;
    private bool _ready;

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
                audioSource.PlayOneShot(_witch.fireballSound);
            }
        }
    }

    private IEnumerator DoAttack()
    {
        GameObject fireballObject = GameObject.Instantiate(_witch.FireballObject);
        GameObject playerObject = GameObject.FindWithTag("Player");

        fireballObject.transform.position = _witch.gameObject.transform.position;
        Vector2 direction = (playerObject.transform.position - fireballObject.transform.position).normalized;

        fireballObject.GetComponent<Rigidbody2D>().velocity = _witch.FireballSpeed * direction * Time.fixedDeltaTime;

        yield return new WaitForSeconds(_witch.FireballExitDelay);

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

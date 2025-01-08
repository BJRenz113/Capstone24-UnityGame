using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViolenceBossScytheThrowState : BaseEnemyState
{
    private ViolenceBoss _boss;
    private bool _ready;

    public override void EnterState(EnemyStateManager enemyStateManager)
    {
        _boss = ((ViolenceBoss)enemyStateManager.GetEnemy());
        _ready = false;
        _boss.StartCoroutine(PerformState());

        if (_boss.scytheThrowSound != null)
        {
            AudioSource audioSource = _boss.gameObject.GetComponent<AudioSource>();
            if (audioSource)
            {
                audioSource.PlayOneShot(_boss.scytheThrowSound);
            }
        }
    }

    private IEnumerator PerformState()
    {
        _boss.gameObject.GetComponent<Animator>().SetInteger("AnimationIndex", 3);
        GameObject playerObject = GameObject.FindWithTag("Player");
        Vector3 bossPos = _boss.gameObject.transform.position;
        Vector3 playerPos = playerObject.transform.position;

        Vector2 throwDirection = (playerPos - bossPos).normalized;

        GameObject scytheObject = GameObject.Instantiate(_boss.ScytheThrowObject);
        scytheObject.transform.position = bossPos;

        scytheObject.GetComponent<Rigidbody2D>().velocity = throwDirection * _boss.ScytheThrowDistance / (_boss.ScytheThrowDuration / 2);
        yield return new WaitForSeconds(_boss.ScytheThrowDuration / 2);

        scytheObject.GetComponent<Rigidbody2D>().velocity = -throwDirection * _boss.ScytheThrowDistance / (_boss.ScytheThrowDuration / 2);
        yield return new WaitForSeconds(_boss.ScytheThrowDuration / 2);

        GameObject.Destroy(scytheObject);

        yield return new WaitForSeconds(_boss.ScytheThrowCooldown);
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

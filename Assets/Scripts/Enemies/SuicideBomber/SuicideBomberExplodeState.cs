using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideBomberExplodeState : BaseEnemyState
{
    private SuicideBomber _bomber;
    public AudioClip explodeSound;

    public override void EnterState(EnemyStateManager enemyStateManager)
    {
        _bomber = ((SuicideBomber)enemyStateManager.GetEnemy());
        _bomber.gameObject.GetComponent<Animator>().SetInteger("AnimationIndex", 2);
        _bomber.StartCoroutine(Explode());

        if (_bomber.explodeSound != null)
        {
            AudioSource audioSource = _bomber.gameObject.GetComponent<AudioSource>();
            if (audioSource)
            {
                audioSource.PlayOneShot(_bomber.explodeSound);
            }
        }
        }
    

    private IEnumerator Explode()
    {
        yield return new WaitForSeconds(_bomber.ExplosionDelay);

        GameObject explosion = GameObject.Instantiate(_bomber.ExplosionObject);
        explosion.GetComponent<SuicideBomberExplosion>().Damage = _bomber.ExplosionDamage;
        CircleCollider2D offsetCollider = _bomber.GetComponent<CircleCollider2D>();
        Vector3 offset = new Vector3(offsetCollider.offset.x, offsetCollider.offset.y, 0);
        explosion.transform.position = _bomber.gameObject.transform.position + offset;
        GameObject.Destroy(_bomber.gameObject);
    }

    public override void FixedUpdateState(EnemyStateManager enemyStateManager)
    {

    }

    public override void ExitState(EnemyStateManager enemyStateManager)
    {

    }
}

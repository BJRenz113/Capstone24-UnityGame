using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class SoulboundWraithAttackState : BaseEnemyState
{
    private int _attackDuration;
    private int _cooldownDuration;
    private Vector2 _attackDirection;
    private float _attackSpeed;
    private Rigidbody2D _rb;

    public SoulboundWraithAttackState(Vector2 direction, Rigidbody2D rb)
    {
        _attackDirection = direction;
        _rb = rb;
    }

    public override void EnterState(EnemyStateManager enemyStateManager)
    {
        _attackDuration = ((SoulboundWraith) enemyStateManager.GetEnemy()).AttackDuration;
        _attackSpeed = ((SoulboundWraith) enemyStateManager.GetEnemy()).AttackSpeed;
        _cooldownDuration = ((SoulboundWraith) enemyStateManager.GetEnemy()).AttackCooldown;

        
        AudioClip attackSound = ((SoulboundWraith)enemyStateManager.GetEnemy()).attackSound;
        if (attackSound != null)
        {
            AudioSource audioSource = ((SoulboundWraith)enemyStateManager.GetEnemy()).gameObject.GetComponent<AudioSource>();
            if (audioSource)
            {
                audioSource.PlayOneShot(attackSound);
            }
        }
    }
    

    public override void FixedUpdateState(EnemyStateManager enemyStateManager)
    {
        if(_attackDuration <= 0)
        {
            _rb.velocity = Vector2.zero;

            _cooldownDuration--;
            if( _cooldownDuration <= 0 )
            {
                enemyStateManager.TransitionToState(new SoulboundWraithPatrolState());
            }
        }
        else
        {
            _attackDuration--;
            _rb.velocity = _attackDirection * _attackSpeed * Time.fixedDeltaTime;
        }
    }

    public override void ExitState(EnemyStateManager enemyStateManager)
    {

    }

    public override void OnColliderEnter2D(EnemyStateManager enemyStateManager, Collider2D other)
    {
        Player player = other.GetComponent<Player>();

        if( player != null )
        {
            int damage = ((SoulboundWraith)enemyStateManager.GetEnemy()).AttackDamage;
            player.TakeHealthDamage(damage);
        }
    }
}

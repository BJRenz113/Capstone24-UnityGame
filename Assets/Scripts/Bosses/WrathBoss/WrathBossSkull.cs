using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrathBossSkull : Enemy
{
    // Define an AudioSource variable to play sound effects
    public AudioSource hitSound;

    public override void FixedUpdate()
    {
        
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();

        if (player != null)
        {
            player.TakeHealthDamage(Damage, true);
            player.TakeSanityDamage(Damage, true);

            // Play the hit sound effect
            if (hitSound != null)
            {
                hitSound.Play();
            }
        }
    }

    public override void TakeDamage(int baseDamage, bool skipArmor = false)
    {
        CurrentHealth -= 1;
        if (CurrentHealth <= 0) enemyStateManager.TransitionToState(deathStateToTrigger);
    }

    [SerializeField] private int _damage = 1;
    public int Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }
}

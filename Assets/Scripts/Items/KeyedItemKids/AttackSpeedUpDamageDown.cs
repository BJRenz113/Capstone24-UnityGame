using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeedUpDamageDown : KeyedItem
{
    public Player player;
    public float attackSpeedIncreasePercentage = 20f;
    public float meleeDamageReductionPercentage = 15f;
    
    void OnTriggerStay2D(Collider2D other) {
        if (Input.GetButton("Submit")) {
            if (other.gameObject.GetComponent<Player>() != null) {
                player = other.gameObject.GetComponent<Player>();
                ApplyItem();
            }
        }
    }

    protected void ApplyItem()
    {
        player.MeleeAttackCooldown -= attackSpeedIncreasePercentage;
        player.MeleeDamage -= Mathf.RoundToInt(player.MeleeDamage * (meleeDamageReductionPercentage / 100));

        AudioSource source = GetComponent<AudioSource>();
        if (source != null)
        {
            source.Play();
        }
        player.getText = "Attack Speed Up, Damage Down";
        Destroy(gameObject);
    }
}


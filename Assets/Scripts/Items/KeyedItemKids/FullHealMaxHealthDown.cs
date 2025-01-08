using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullHealMaxHealthDown : KeyedItem
{
        public Player player;
    public int healthRestoreAmount = 150;
    public int healthReduction = 10;

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
        player.HealHealth(healthRestoreAmount);
        player.MaxHealth -= healthReduction;

        if (player.CurrentHealth > player.MaxHealth)
        {
            player.CurrentHealth = player.MaxHealth;
        }

        AudioSource source = GetComponent<AudioSource>();
        if (source != null)
        {
            source.Play();
        }
        player.getText = "Deal with the Devil";
        Destroy(gameObject);
    }
}

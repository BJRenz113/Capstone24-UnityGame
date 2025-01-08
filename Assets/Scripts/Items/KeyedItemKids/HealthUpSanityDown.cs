using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUpSanityDown : KeyedItem
{

        public Player player;

    public int healthIncrease = 10;
    public int sanityDecrease = 10;

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
        player.MaxHealth += healthIncrease;
        player.MaxSanity -= sanityDecrease;
        if (player.CurrentHealth < player.MaxHealth)
            player.CurrentHealth = player.MaxHealth;

        AudioSource source = GetComponent<AudioSource>();
        if (source != null)
        {
            source.Play();
        }
        player.getText = "Sketchy Pills";
        Destroy(gameObject);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanityImmuneHealthDown : KeyedItem
{
        public Player player;

    public int healthReduction = 10;
    public int sanityResist = 40;

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


          player.ResistSanityPercentage += (int) sanityResist;


        player.MaxHealth -= healthReduction;
        if (player.CurrentHealth > player.MaxHealth)
            player.CurrentHealth = player.MaxHealth;

        AudioSource source = GetComponent<AudioSource>();
        if (source != null)
        {
            source.Play();
        }
        player.getText = "PHD";
        Destroy(gameObject);
    }
}

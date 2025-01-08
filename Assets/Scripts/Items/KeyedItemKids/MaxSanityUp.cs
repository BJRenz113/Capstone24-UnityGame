using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxSanityUp : KeyedItem
{
        public Player player;
    public int sanityBoost = 25; 

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
        player.BuffMaxSanity(sanityBoost);
        Debug.Log($"Mind Elixir obtained! Max Sanity increased by {sanityBoost} points.");

        AudioSource source = GetComponent<AudioSource>();
        if (source != null)
        {
            source.Play();
        }
        player.getText = "Max Sanity Up";
        Destroy(gameObject);
    }
}

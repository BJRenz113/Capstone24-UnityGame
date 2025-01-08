using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDownSanityUp: KeyedItem
{
        public Player player;
    public int sanityIncrease = 25;
    public int healthDrain = 25;




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
        player.CurrentSanity += sanityIncrease;
        player.CurrentHealth -= healthDrain;

        AudioSource source = GetComponent<AudioSource>();
        if (source != null)
        {
            source.Play();
        }
        player.getText = "Sanity > Health";
        Destroy(gameObject);
    }
}

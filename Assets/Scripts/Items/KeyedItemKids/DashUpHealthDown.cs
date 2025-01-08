using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashUpHealthDown : KeyedItem
{
        public Player player;
    public int dashAmount = 2;
    public int healthDrainAmount = 3;

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
        player.MaxDashes += dashAmount;
        player.MaxHealth -= healthDrainAmount;

        AudioSource source = GetComponent<AudioSource>();
        if (source != null)
        {
            source.Play();
        }
        player.getText = "Devils Dash";
        Destroy(gameObject);
    }
}

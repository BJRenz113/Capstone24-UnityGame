using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashUpDurationDown : KeyedItem
{

    public Player player;
    public int dashAmount = 1;
    public int durationDrainAmount = 3;

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
        player.DashDuration -= durationDrainAmount;
        
        AudioSource source = GetComponent<AudioSource>();
        if (source != null)
        {
            source.Play();
        }
        player.getText = "Long Way";
        Destroy(gameObject);
    }
}


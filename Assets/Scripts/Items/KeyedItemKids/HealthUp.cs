using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealthUp : KeyedItem {    
    public Player player;

    public int health = 100;

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
        player.MaxHealth += health;
        AudioSource source = GetComponent<AudioSource>();
        if (source != null)
        {
            source.Play();
        }
        player.getText = "Health Up";
        Destroy(gameObject);
    }
}


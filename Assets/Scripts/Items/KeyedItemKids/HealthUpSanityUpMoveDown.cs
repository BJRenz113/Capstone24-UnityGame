using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUpSanityUpMoveDown : KeyedItem

{

        public Player player;
    public int healthIncrease = 10;
    public int sanityIncrease = 10;
    public float movementSpeedReductionPercentage = 5f;
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
        player.MaxSanity += sanityIncrease;
        player.MoveSpeed -= movementSpeedReductionPercentage;
        AudioSource source = GetComponent<AudioSource>();
        if (source != null)
        {
            source.Play();
        }
        player.getText = "Ultra Juggernaut";
        Destroy(gameObject);
    }
}

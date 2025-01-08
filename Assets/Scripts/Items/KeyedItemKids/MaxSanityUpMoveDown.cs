using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxSanityUpMoveDown : KeyedItem
{
        public Player player;
    public int sanityIncrease = 30;
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
        player.MaxSanity += sanityIncrease;
        player.MoveSpeed += player.MoveSpeed * (movementSpeedReductionPercentage / 100);

        AudioSource source = GetComponent<AudioSource>();
        if (source != null)
        {
            source.Play();
        }
        player.getText = "False PHD";
        Destroy(gameObject);
    }
}

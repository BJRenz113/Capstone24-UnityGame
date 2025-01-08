using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeUpMovementDown : KeyedItem
{
        public Player player;
    public float meleeDamageIncreasePercentage = 20f;
    public float movementSpeedReductionPercentage = 0.05f;

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
        player.MeleeDamage += Mathf.RoundToInt(player.MeleeDamage * (meleeDamageIncreasePercentage / 100));
        player.MoveSpeed -= movementSpeedReductionPercentage;

        AudioSource source = GetComponent<AudioSource>();
        if (source != null)
        {
            source.Play();
        }
        player.getText = "Juggernaut";
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUpDamageReduceMovementReduce : KeyedItem
{
        public Player player;
    public int healthIncrease = 5;
    public float damageReductionPercentage = 5f;
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
        player.ResistHealthPercentage += (int) damageReductionPercentage;
        player.MoveSpeed -= player.MoveSpeed * (movementSpeedReductionPercentage / 100);

        AudioSource source = GetComponent<AudioSource>();
        if (source != null)
        {
            source.Play();
        }
        player.getText = "Snail Juice";
        Destroy(gameObject);
    }
}

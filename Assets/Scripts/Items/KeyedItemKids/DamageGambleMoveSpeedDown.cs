using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DamageGambleMoveSpeedDown : KeyedItem
{
    public Player player;
    public int doubleDamageChance;
    public float movementSpeedReductionPercentage = 15f;

    void OnTriggerStay2D(Collider2D other) {
        if (Input.GetButtonDown("Submit")) {
            if (other.gameObject.GetComponent<Player>() != null) {
                player = other.gameObject.GetComponent<Player>();
                ApplyItem();
            }
        }
    }

    protected void ApplyItem()
    {
        int n = UnityEngine.Random.Range(0, 3);

        player.DashDamage += n;
        player.MoveSpeed -= player.MoveSpeed * (movementSpeedReductionPercentage / 100);

        AudioSource source = GetComponent<AudioSource>();
        if (source != null)
        {
            source.Play();
        }
        player.getText = "Gambling Fever " + n;
        Destroy(gameObject);
    }
}

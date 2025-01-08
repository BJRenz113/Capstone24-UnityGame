using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHealhKeyItem : MonoBehaviour
{
    private Player player; // Reference to the Player object
    public int healthRestoreAmount = 150;
    public int healthReduction = 10;


    void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetButtonDown("Submit"))
        {
            if (other.gameObject.GetComponent<Player>() != null)
            {
                player = other.gameObject.GetComponent<Player>();
                player.HealHealth(healthRestoreAmount);
                player.MaxHealth -= healthReduction;

                if (player.CurrentHealth > player.MaxHealth)
                {
                    player.CurrentHealth = player.MaxHealth;
                }

                Destroy(gameObject);
            }
        }
    }
}

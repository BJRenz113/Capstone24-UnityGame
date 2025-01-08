using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour
{
    public int HealthAmount = 5;
    private Player player; // Reference to the Player object

    void OnTriggerEnter2D(Collider2D other)
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        player.HealHealth(HealthAmount);
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanityItem : MonoBehaviour
{
    public int sanityAmount = 4;
    private Player player; // Reference to the Player object

    void OnTriggerEnter2D(Collider2D other)
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        player.HealSanity(sanityAmount);
        Destroy(gameObject);
    }
}

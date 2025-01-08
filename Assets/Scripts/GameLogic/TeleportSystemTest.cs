using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSystemTest : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collided object has a Box Collider and is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            // Log a message when player collides with the box
            Debug.Log("Player collided with the box!");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeperLooking : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float maxAngle = 90f; // Maximum angle within which NPC can look at the player

    private SpriteRenderer spriteRenderer; // Reference to the NPC's sprite renderer

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (player != null)
        {
            // Calculate direction to player
            Vector3 directionToPlayer = player.position - transform.position;

            // Calculate angle between NPC's forward direction and direction to player
            float angle = Vector3.Angle(transform.up, directionToPlayer);

            // Check if the angle is within range
            if (angle <= maxAngle)
            {
                // Player is within range, determine if player is on the left or right side of NPC
                bool playerIsOnRight = Vector3.Cross(transform.up, directionToPlayer).z < 0;

                // Flip NPC sprite if needed
                spriteRenderer.flipX = playerIsOnRight;
            }
        }
    }
}
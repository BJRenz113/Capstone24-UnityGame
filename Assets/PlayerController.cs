using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 5f;

    private Teleporter currentTeleporter;

    void Update()
    {
        // Player movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontal, vertical, 0f) * movementSpeed * Time.deltaTime;
        transform.Translate(movement);

        // Interact with teleporter
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryTeleport();
        }
    }

    void TryTeleport()
    {
        // Check if the player is near a teleporter
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Teleporter"))
            {
                Teleport(collider.GetComponent<Teleporter>());
                break;
            }
        }
    }

    void Teleport(Teleporter teleporter)
    {
        if (currentTeleporter == null)
        {
            // First teleporter activated
            currentTeleporter = teleporter;
        }
        else
        {
            // Second teleporter activated, perform teleportation
            transform.position = teleporter.GetTeleportPosition();
            currentTeleporter = null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportClick : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        // Check for mouse click
        if (Input.GetMouseButtonDown(0))
        {
            // Get the mouse position in world coordinates
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f; // Ensure the z-coordinate is 0 in a 2D game

            // Teleport the player to the clicked position
            transform.position = mousePosition;
        }
    }
}

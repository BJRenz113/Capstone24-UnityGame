using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTeleBack : MonoBehaviour
{
    private Player player; // Reference to the Player object


    void OnTriggerStay2D(Collider2D other)
    {
        player = other.gameObject.GetComponent<Player>();

        if (Input.GetButton("Submit"))
        {
            if (other.gameObject.GetComponent<Player>() != null)
            {
                player.transform.position = new Vector2(0, -2);

            }
        }
    }
}

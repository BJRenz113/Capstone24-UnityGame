using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PickupItem : MonoBehaviour
{
    public enum ItemType
    {
        HealthPack,
        Coin,
    }

    public ItemType type;
    public int healthIncreaseAmount = 10; // Amount to increase max health by
    public int moneyIncrement = 1;
    public float pickupRange = 3f; // Range within which the player can pick up the item

    public Sprite sprite;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                // Calculate the distance between the item and the player
                float distance = Vector3.Distance(transform.position, other.transform.position);

                // Check if the player is within the pickup range for the item type
                switch (type)
                {
                    case ItemType.HealthPack:
                        if (distance <= pickupRange)
                        {
                            // Increase player's max health
                            player.HealHealth(healthIncreaseAmount);
                            //00player.MaxHealth += healthIncreaseAmount + 20; // Likely needs to be removed, testing purposes only
                            Debug.Log("HealthPack collected! Health increased by " + healthIncreaseAmount + ".");
                            AudioSource source = GetComponent<AudioSource> ();
                            
                            // Play the health pack sound using AudioSource
                            if (source != null)
                            {

                                source.Play();
                            }
                            
                             Destroy(gameObject, 0.5f); //delay to ensure game item plays sound to completetion
                        }
                        break;
                    case ItemType.Coin:
                        if (distance <= pickupRange)
                        {
                            // Increase player's money
                            player.AddMoney(moneyIncrement);
                            Debug.Log("Coin collected!");

                            AudioSource source = GetComponent<AudioSource> ();
                            if (source != null) {
                                source.Play();
                            }
                            Destroy(gameObject, 0.5f);
                        }
                        break;
                }
            }
        }
    }
}

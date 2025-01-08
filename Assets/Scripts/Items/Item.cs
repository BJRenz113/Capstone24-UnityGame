using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        HealthPack,
        Coin,
        PowerUp
    }

    public ItemType type;
    public int healthIncreaseAmount = 10; // Amount to increase max health by
    public int moneyIncrementAmount = 1;
    public float pickupRange = 3f; // Range within which the player can pick up the item

    private Player player; // Reference to the Player object
    public Sprite sprite;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            float distance = Vector2.Distance(transform.position, other.transform.position);
            switch (type)
            {
                case ItemType.HealthPack:
                    if (distance <= pickupRange)
                    {
                        player.HealHealth(healthIncreaseAmount);
                        Debug.Log("HealthPack collected! Max health increased by " + healthIncreaseAmount + ".");
                        Destroy(gameObject);
                    }
                    break;
                case ItemType.Coin:
                    if (distance <= pickupRange)
                    {
                        player.AddMoney(moneyIncrementAmount);
                        Debug.Log("Coin collected!");
                        Destroy(gameObject);
                    }
                    break;
                case ItemType.PowerUp:
                    break;
            }
        }
    }

    void Update()
    {
    }
}

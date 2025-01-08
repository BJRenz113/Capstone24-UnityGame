using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShop : MonoBehaviour
{
    public RandomLoot randomLoot;
    public int chargeAmount = 20;
    public Sprite itemSprite;
    public float spawnOffsetY = -0.7f; // Adjust this value to set the spawn position lower
    public float spawnRange = 1.0f; // Adjust this value to set the horizontal spawn range

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ChargePlayer();
        }
    }

    private void ChargePlayer()
    {
        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();

        if (player.CurrentMoney >= chargeAmount)
        {
            player.LoseMoney(chargeAmount);
            SpawnRandomBasicItem();
        }
        else
        {
            Debug.Log("Not enough gold to use the charging station: " + player.CurrentMoney);
        }
    }

    private void SpawnRandomBasicItem()
    {
        if (randomLoot != null)
        {
            float randomX = Random.Range(transform.position.x - spawnRange, transform.position.x + spawnRange);
            float randomY = transform.position.y + spawnOffsetY;
            Vector2 spawnPosition = new Vector2(randomX, randomY);
            
            GameObject spawnedItem = randomLoot.GenerateRandomBasicItem(spawnPosition);
            if (spawnedItem != null)
            {
                Debug.Log("Basic item spawned at charging station.");
            }
            else
            {
                Debug.Log("Failed to spawn basic item at charging station.");
            }
        }
        else
        {
            Debug.Log("RandomLoot script reference missing.");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : MonoBehaviour
{
    public int coinAmount = 5;
    private Player player; // Reference to the Player object

    void OnTriggerEnter2D(Collider2D other)
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        player.AddMoney (coinAmount);
        Destroy(gameObject);
    }
}

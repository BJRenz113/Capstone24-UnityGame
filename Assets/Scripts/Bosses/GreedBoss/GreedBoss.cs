using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreedBoss : MonoBehaviour
{
    public Animator animator;
    public int hpDrainPerTick = 5;
    public int coinsPerTick = 1;
    public int coinThreshold = 50;
    public Player player; // Reference to the player script
    public AudioSource audioSource; // Reference to the AudioSource component
    public AudioClip endSound; // Sound for end of Greed sequence

    private bool playerInteracted = false;
    private RandomLoot randomLoot; // Reference to the RandomLoot script

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        // Play idle animation
        gameObject.GetComponent<Animator>().SetInteger("AnimationIndex", 0);
        //animator.SetInteger("AnimationIndex", 0);
        Debug.Log("Idle animation started.");

        // Find and store reference to RandomLoot script
        randomLoot = FindObjectOfType<RandomLoot>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !playerInteracted)
        {
            StartGreedSequence();
            playerInteracted = true;
        }
    }

    private void StartGreedSequence()
    {
        Debug.Log("Greed Sequence Entered");
        // Play second animation
        //animator.SetInteger("AnimationIndex", 1);
        gameObject.GetComponent<Animator>().SetInteger("AnimationIndex", 1);

        Debug.Log("DrainCoins animation started.");

        // Start draining coins
        StartCoroutine(DrainPlayerCoins());
    }

private IEnumerator DrainPlayerCoins()
{
    int totalTicks = 0;
    int totalCoinsDrained = 0;

    // Drain coins and/or health until threshold is reached
    while (totalTicks < coinThreshold)
    {
        // Check if player has enough coins
        if (player.CurrentMoney > 0 && totalCoinsDrained < coinThreshold)
        {
            // Drain coins
            player.LoseMoney(coinsPerTick);
            totalCoinsDrained += coinsPerTick;
        }
        else
        {
            // If player has less than 50 coins, drain HP instead
            player.TakeHealthDamage(hpDrainPerTick);
        }

        totalTicks++; // Increment total ticks

        yield return new WaitForSeconds(0.25f); // Adjust the time interval between each coin/health draining tick
    }

    EndGreedSequence();
}



    private void EndGreedSequence()
    {
        gameObject.GetComponent<Animator>().SetInteger("AnimationIndex", 2);
        Debug.Log("EndGreed animation started.");

        //audioSource.PlayOneShot(endSound);

        Destroy(gameObject);



    }
}

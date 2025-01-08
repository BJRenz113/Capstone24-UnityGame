using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackPoint; // The point from where the attack originates
    public LayerMask enemyLayers; // The layers considered as enemies
    public float attackRange = 1.0f; // The range of the attack
    public int damage = 10; // The amount of damage dealt to enemies
    public AudioClip attackSound; // Sound effect to play when attacking
    private AudioSource audioSource; // Reference to the AudioSource component

    void Start()
    {
        // Get the AudioSource component attached to the player
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Check for left mouse button click
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemyCollider in hitEnemies)
        {
            Debug.Log("WE HIT ENEMY");
            
            // Check if the hit object has an Enemy script (replace "Enemy" with the actual script name)
            Enemy enemy = enemyCollider.GetComponent<Enemy>();

            // If the hit object has an Enemy script, deal damage to it
            if (enemy != null)
            {
                Destroy(enemy.gameObject); // Assuming the enemy script is attached to a GameObject
            }
        }

        // Play the attack sound effect
        if (attackSound != null)
        {
            audioSource.PlayOneShot(attackSound);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// using UnityEngine;

// public class WalkingAOE : Enemy
// {
//     [SerializeField] private float constantDamageRadius = 1.0f; // The constant damage radius
//     [SerializeField] private float moveSpeed = 1.0f; // The speed at which the enemy moves
//     [SerializeField] private float radiusOffDuration = 0.75f; // The duration for which the damage radius is turned off
//     [SerializeField] private float radiusOnDuration = 2.0f; // The duration after which the damage radius is turned on again

//     private bool radiusActive = true; // Flag to keep track of whether the damage radius is active or not
//     private float timer = 0.0f; // Timer to keep track of time intervals

//     public override void Start()
//     {
//         base.Start();
//         enemyStateManager.TransitionToState(new TemplateEnemyState()); // Replace with your desired initial state
//     }

//     public override void FixedUpdate()
//     {
//         base.FixedUpdate();

//         MoveTowardsPlayer();
//         UpdateRadiusActivation();
//     }

//     private void MoveTowardsPlayer()
//     {
//         // Example code to move towards the player (Replace with your own logic)
//         Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player");
//         Vector3 direction = (playerPosition - transform.position).normalized;
//         transform.position += direction * moveSpeed * Time.deltaTime;
//     }

//     private void UpdateRadiusActivation()
//     {
//         // Update timer
//         timer += Time.deltaTime;

//         // Toggle radius activation based on timer and duration settings
//         if (timer >= radiusOnDuration)
//         {
//             radiusActive = false;
//             if (timer >= radiusOnDuration + radiusOffDuration)
//             {
//                 timer = 0.0f;
//                 radiusActive = true;
//             }
//         }
//     }

//     public override void TakeDamage(int baseDamage, bool skipArmor = false)
//     {
//         // Apply damage to the main target
//         base.TakeDamage(baseDamage, skipArmor);

//         // Apply constant damage to nearby targets within the constant damage radius if it's active
//         if (radiusActive)
//         {
//             Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, constantDamageRadius);
//             foreach (Collider2D collider in colliders)
//             {
//                 if (collider.CompareTag("Player"))
//                 {
//                     Player player = collider.GetComponent<Player>();
//                     player.TakeHealthDamage(baseDamage, skipArmor);
//                 }
//             }
//         }
//     }

//     // For visualization in the Unity Editor
//     private void OnDrawGizmosSelected()
//     {
//         // Draw constant damage radius
//         Gizmos.color = Color.red;
//         Gizmos.DrawWireSphere(transform.position, constantDamageRadius);
//     }
// }


using UnityEngine;

public class CollisionLogger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Assuming the player has a tag "Player"
        {
            Debug.Log("Player collided with BoxCollider2D!");
        }
    }
}

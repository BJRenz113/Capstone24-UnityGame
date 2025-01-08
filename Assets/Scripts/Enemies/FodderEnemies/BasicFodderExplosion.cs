using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicFodderExplosion: Enemy
{
    [SerializeField] private float explosionRadius = 2.0f; // Radius of the explosion
    [SerializeField] private int explosionDamage = 50; // Damage dealt by the explosion
    [SerializeField] private float explosionDelay = 0.5f; // Delay before the explosion occurs after death

    public override void Start()
    {
        base.Start();
        enemyStateManager.TransitionToState(new TemplateEnemyState());
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    // This is just an example of how you would add new fields to a class that extends Enemy
    [SerializeField] private int _uniqueField = 0;
    public int UniqueField
    {
        get { return _uniqueField; }
        set { _uniqueField = value; }
    }

    public override void TakeDamage(int baseDamage, bool skipArmor = false)
    {
        base.TakeDamage(baseDamage, skipArmor);

        if (CurrentHealth <= 0)
        {
            StartCoroutine(ExplodeAfterDelay());
        }
    }

    private IEnumerator ExplodeAfterDelay()
    {
        yield return new WaitForSeconds(explosionDelay);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                Player player = collider.GetComponent<Player>();
                player.TakeHealthDamage(explosionDamage);
            }
        }

        // Destroy the enemy after the explosion
        Destroy(gameObject);
    }

    // For visualization in the Unity Editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicFodderLongReach: Enemy
{
    [SerializeField] private float damageRadius = 1.5f; // The radius within which the enemy deals damage

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
        // Apply damage to the main target
        base.TakeDamage(baseDamage, skipArmor);

        // Apply damage to nearby targets within the damage radius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, damageRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                Player player = collider.GetComponent<Player>();
                player.TakeHealthDamage(baseDamage, skipArmor);
            }
        }
    }

    // For visualization in the Unity Editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }
}

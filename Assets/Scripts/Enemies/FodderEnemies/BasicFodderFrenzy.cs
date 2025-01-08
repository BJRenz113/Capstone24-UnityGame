using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicFodderFrenzy: Enemy
{
    private float damageMultiplier = 1f;
    private float speedMultiplier = 1f;

    [SerializeField] private float moveSpeed = 5f;

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
        IncreaseDamageAndSpeed();
    }

    private void IncreaseDamageAndSpeed()
    {
        // Increase damage and move speed by a factor of 5
        damageMultiplier *= 5f;
        speedMultiplier *= 5f;

        // Apply the increased damage and move speed
        ApplyMultipliers();
    }

    private void ApplyMultipliers()
    {
        // Multiply the enemy's damage and move speed by the multipliers
        MaxHealth = Mathf.RoundToInt(MaxHealth * damageMultiplier);
        CurrentHealth = Mathf.RoundToInt(CurrentHealth * damageMultiplier);
        // You may need to adjust the calculations based on how your speed is implemented.
        // Assuming speed is a float value, you would multiply it by the speedMultiplier
        // For example:
        moveSpeed *= speedMultiplier;
    }
}

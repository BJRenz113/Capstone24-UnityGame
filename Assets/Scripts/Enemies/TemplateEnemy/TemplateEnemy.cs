using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemplateEnemy : Enemy
{
    public override void Start()
    {
        base.Start();
        enemyStateManager.TransitionToState(new TemplateEnemyState());
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    // this is just an example of how you would add new fields to a class that extends enemy
    [SerializeField] private int _uniqueField = 0;
    public int UniqueField
    {
        get { return _uniqueField; }
        set { _uniqueField = value; }
    }
}

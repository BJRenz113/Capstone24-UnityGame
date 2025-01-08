using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner : Enemy
{
    public override void Start()
    {
        base.Start();
        enemyStateManager.TransitionToState(new SummonerChaseState());
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
    [SerializeField] private int _maxSpawn = 3;
    public int getMaxSpawn
    {
        get { return _maxSpawn; }
        set { _maxSpawn = value; }
    }

    [SerializeField] private int _keepDistanceRadius = 1;
    public int DistanceRadius
    {
        get { return _keepDistanceRadius; }
        set { _keepDistanceRadius = value; }
    }

}

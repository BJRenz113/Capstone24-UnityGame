using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulboundWraith : Enemy
{
    public AudioClip attackSound;
    
    public override void Start()
    {
        base.Start();
        deathStateToTrigger = new SoulboundWraithDeathState();
        HomePosition = gameObject.transform.position;

        int areas = GetAreasCleared();
        AttackDamage = AttackDamageList[areas];

        enemyStateManager.TransitionToState(new SoulboundWraithPatrolState());
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    // initial min/max properties
    [SerializeField] private float _patrolSpeed = 10f;
    public float PatrolSpeed
    {
        get { return _patrolSpeed; }
        set { _patrolSpeed = value; }
    }

    [SerializeField] private float _patrolProximity = 2f;
    public float PatrolProximity
    {
        get { return _patrolProximity; }
        set { _patrolProximity = value; }
    }

    [SerializeField] private int _stalkHesitation = 50;
    public int StalkHesitation
    {
        get { return _stalkHesitation; }
        set { _stalkHesitation = value; }
    }

    [SerializeField] private float _stalkProximity = 3f;
    public float StalkProximity
    {
        get { return _stalkProximity; }
        set { _stalkProximity = value; }
    }

    [SerializeField] private float _stalkSpeed = 30f;
    public float StalkSpeed
    {
        get { return _stalkSpeed; }
        set { _stalkSpeed = value; }
    }

    [SerializeField] private int _attackHesitation = 40;
    public int AttackHesitation
    {
        get { return _attackHesitation; }
        set { _attackHesitation = value; }
    }

    [SerializeField] private int _attackCooldown = 70;
    public int AttackCooldown
    {
        get { return _attackCooldown; }
        set { _attackCooldown = value; }
    }

    [SerializeField] private int _attackDuration = 10;
    public int AttackDuration
    {
        get { return _attackDuration; }
        set { _attackDuration = value; }
    }

    [SerializeField] private float _attackSpeed = 340f;
    public float AttackSpeed
    {
        get { return _attackSpeed; }
        set { _attackSpeed = value; }
    }

    private int _attackDamage;
    public int AttackDamage
    {
        get { return _attackDamage; }
        set { _attackDamage = value; }
    }

    [SerializeField] private List<int> _attackDamageList = new List<int>() { 7, 7, 7, 7, 7, 7 };
    public List<int> AttackDamageList
    {
        get { return _attackDamageList; }
        set { _attackDamageList = value; }
    }

    private Vector2 _homePosition;
    public Vector2 HomePosition
    {
        get { return _homePosition; }
        set { _homePosition = value; }
    }
}

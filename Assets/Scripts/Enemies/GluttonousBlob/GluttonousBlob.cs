using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GluttonousBlob : Enemy
{
    public AudioClip attackSound;
    public override void Start()
    {
        base.Start();
        deathStateToTrigger = new GluttonousBlobDeathState();
        _startTime = Time.time;
        _homePosition = gameObject.transform.position;
        _originalSize = _size;
        int areas = GetAreasCleared();
        BaseDamage = BaseDamageList[areas];

        UnityEngine.AI.NavMeshAgent agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();

        agent.speed = _baseSpeed * Time.fixedDeltaTime;
        agent.angularSpeed = _baseAngularSpeed * Time.fixedDeltaTime;
        agent.acceleration = _baseAcceleration * Time.fixedDeltaTime;

        enemyStateManager.TransitionToState(new GluttonousBlobMoveState());
    }

    public override void FixedUpdate()
    {
        Vector3 playerPos = GameObject.FindWithTag("Player").transform.position;
        gameObject.GetComponent<SpriteRenderer>().flipX = gameObject.transform.position.x < playerPos.x;
        base.FixedUpdate();
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player == null) return;
        if (CanHurt) player.TakeHealthDamage(_baseDamage);
    }

    public override void TakeDamage(int baseDamage, bool skipArmor = false)
    {
        if (Time.time - _startTime >= _spawnInvincibility) base.TakeDamage(baseDamage, skipArmor);
    }

    public void ExecuteOnSplit()
    {
        Size -= 1;

        for (int i = 0; i < MaxHealthList.Count; i++)
        {
            MaxHealthList[i] = (int)Math.Floor(MaxHealthList[i] * SplitHealthMultiplier);
        }

        for (int i = 0; i < BaseDamageList.Count; i++)
        {
            BaseDamageList[i] = (int)Math.Floor(BaseDamageList[i] * SplitDamageMultiplier);
        }

        BaseSpeed = BaseSpeed * SplitSpeedMultiplier;
        BaseAngularSpeed = BaseAngularSpeed * SplitSpeedMultiplier;
        BaseAcceleration = BaseAcceleration * SplitSpeedMultiplier;

        UnityEngine.AI.NavMeshAgent agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();

        agent.speed = _baseSpeed * Time.fixedDeltaTime;
        agent.angularSpeed = _baseAngularSpeed * Time.fixedDeltaTime;
        agent.acceleration = _baseAcceleration * Time.fixedDeltaTime;

        Vector3 blobScale = gameObject.transform.localScale;
        gameObject.transform.localScale = new Vector3(blobScale.x * SplitScaleMultiplier, blobScale.y * SplitScaleMultiplier, blobScale.z);

        if (BaseDamage <= 0) BaseDamage = 1;
        if (MaxHealth <= 0) MaxHealth = 1;
    }

    private float _startTime;

    private bool _canHurt;
    public bool CanHurt
    {
        get { return _canHurt; }
        set { _canHurt = value; }
    }

    private Vector3 _homePosition;
    public Vector3 HomePosition
    {
        get { return _homePosition; }
        set { _homePosition = value; }
    }

    private int _originalSize;
    public int OriginalSize
    {
        get { return _originalSize; }
        set { _originalSize = value; }
    }

    [SerializeField] private float _spawnInvincibility = 0.5f;
    public float SpawnInvincibility
    {
        get { return _spawnInvincibility; }
        set { _spawnInvincibility = value; }
    }

    [SerializeField] private int _size = 2;
    public int Size
    {
        get { return _size; }
        set { _size = value; }
    }

    [SerializeField] private int _splits = 2;
    public int Splits
    {
        get { return _splits; }
        set { _splits = value; }
    }

    [SerializeField] private float _initialSplitRadius = 1f;
    public float InitialSplitRadius
    {
        get { return _initialSplitRadius; }
        set { _initialSplitRadius = value; }
    }

    [SerializeField] private int _splitAngleGranularity = 8;
    public int SplitAngleGranularity
    {
        get { return _splitAngleGranularity; }
        set { _splitAngleGranularity = value; }
    }

    [SerializeField] private List<int> _baseDamageList = new List<int>() {10, 10, 10, 10, 10, 10};
    public List<int> BaseDamageList
    {
        get { return _baseDamageList; }
        set { _baseDamageList = value; }
    }

    private int _baseDamage = 10;
    public int BaseDamage
    {
        get { return _baseDamage; }
        set { _baseDamage = value; }
    }

    [SerializeField] private float _attackRadius = 0.2f;
    public float AttackRadius
    {
        get { return _attackRadius; }
        set { _attackRadius = value; }
    }

    [SerializeField] private float _attackWait = 0.25f;
    public float AttackWait
    {
        get { return _attackWait; }
        set { _attackWait = value; }
    }

    [SerializeField] private float _attackDuration = 0.5f;
    public float AttackDuration
    {
        get { return _attackDuration; }
        set { _attackDuration = value; }
    }

    [SerializeField] private float _attackCooldown = 1f;
    public float AttackCooldown
    {
        get { return _attackCooldown; }
        set { _attackCooldown = value; }
    }

    [SerializeField] private float _baseSpeed = 10f;
    public float BaseSpeed
    {
        get { return _baseSpeed; }
        set { _baseSpeed = value; }
    }

    [SerializeField] private float _baseAngularSpeed = 45f;
    public float BaseAngularSpeed
    {
        get { return _baseAngularSpeed; }
        set { _baseAngularSpeed = value; }
    }

    [SerializeField] private float _baseAcceleration = 20f;
    public float BaseAcceleration
    {
        get { return _baseAcceleration; }
        set { _baseAcceleration = value; }
    }

    [SerializeField] private float _splitHealthMultiplier = 0.5f;
    public float SplitHealthMultiplier
    {
        get { return _splitHealthMultiplier; }
        set { _splitHealthMultiplier = value; }
    }

    [SerializeField] private float _splitDamageMultiplier = 0.5f;
    public float SplitDamageMultiplier
    {
        get { return _splitDamageMultiplier; }
        set { _splitDamageMultiplier = value; }
    }

    [SerializeField] private float _splitSpeedMultiplier = 2f;
    public float SplitSpeedMultiplier
    {
        get { return _splitSpeedMultiplier; }
        set { _splitSpeedMultiplier = value; }
    }

    [SerializeField] private float _splitScaleMultiplier = 0.5f;
    public float SplitScaleMultiplier
    {
        get { return _splitScaleMultiplier; }
        set { _splitScaleMultiplier = value; }
    }
}

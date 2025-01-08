using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodMosquito : Enemy
{
    public AudioClip attackSound;
    public override void Start()
    {
        base.Start();
        deathStateToTrigger = new BloodMosquitoDeathState();
        _homePosition = gameObject.transform.position;

        int areas = GetAreasCleared();
        BiteDamage = BiteDamageList[areas];

        enemyStateManager.TransitionToState(new BloodMosquitoMoveState());
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player == null) return;
        _hasTouched = true;
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player == null) return;
        _hasTouched = false;
    }

    [SerializeField] private float _moveSpeed = 200f;
    public float MoveSpeed
    {
        get { return _moveSpeed; }
        set { _moveSpeed = value; }
    }

    [SerializeField] private float _homeRadius = 2f;
    public float HomeRadius
    {
        get { return _homeRadius; }
        set { _homeRadius = value; }
    }

    [SerializeField] private float _moveWaitMin = 0.4f;
    public float MoveWaitMin
    {
        get { return _moveWaitMin; }
        set { _moveWaitMin = value; }
    }

    [SerializeField] private float _moveWaitMax = 0.9f;
    public float MoveWaitMax
    {
        get { return _moveWaitMax; }
        set { _moveWaitMax = value; }
    }

    private Vector3 _homePosition;
    public Vector3 HomePosition
    {
        get { return _homePosition; }
        set { _homePosition = value; }
    }

    private bool _hasTouched = false;
    public bool HasTouched
    {
        get { return _hasTouched; }
        set { _hasTouched = value; }
    }

    private int _totalDamageDealt = 0;
    public int TotalDamageDealt
    {
        get { return _totalDamageDealt; }
        set { _totalDamageDealt = value; }
    }

    [SerializeField] private float _bloodRecoveryFactor = 0.5f;
    public float BloodRecoveryFactor
    {
        get { return _bloodRecoveryFactor; }
        set { _bloodRecoveryFactor = value; }
    }

    [SerializeField] private int _biteCount = 3;
    public int BiteCount
    {
        get { return _biteCount; }
        set { _biteCount = value; }
    }

    [SerializeField] private float _biteDelay = 1f;
    public float BiteDelay
    {
        get { return _biteDelay; }
        set { _biteDelay = value; }
    }

    [SerializeField] private float _timeBetweenBites = 1f;
    public float TimeBetweenBites
    {
        get { return _timeBetweenBites; }
        set { _timeBetweenBites = value; }
    }

    [SerializeField] private List<int> _biteDamageList = new List<int>() { 6, 6, 6, 6, 6, 6};
    public List<int> BiteDamageList
    {
        get { return _biteDamageList; }
        set { _biteDamageList = value; }
    }

    private int _biteDamage = 6;
    public int BiteDamage
    {
        get { return _biteDamage; }
        set { _biteDamage = value; }
    }
}

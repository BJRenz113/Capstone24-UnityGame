using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeresyBossWhiteWitch : Enemy
{
    public AudioClip summonSound;
        public override void Start()
    {
        base.Start();
        _homePosition = gameObject.transform.position;
        enemyStateManager.TransitionToState(new HeresyBossWhiteWitchPassiveState());
    }

    public override void FixedUpdate()
    {
        Vector3 playerPos = GameObject.FindWithTag("Player").transform.position;
        gameObject.GetComponent<SpriteRenderer>().flipX = gameObject.transform.position.x > playerPos.x;
        base.FixedUpdate();
    }

    private Vector3 _homePosition;
    public Vector3 HomePosition
    {
        get { return _homePosition; }
        set { _homePosition = value; }
    }

    [SerializeField] private GameObject _skeletonObject;
    public GameObject SkeletonObject
    {
        get { return _skeletonObject; }
        set { _skeletonObject = value; }
    }

    [SerializeField] private bool _skeletonsEmitEmbers;
    public bool SkeletonsEmitEmbers
    {
        get { return _skeletonsEmitEmbers; }
        set { _skeletonsEmitEmbers = value; }
    }

    [SerializeField] private bool _skeletonsDealSanity;
    public bool SkeletonsDealSanity
    {
        get { return _skeletonsDealSanity; }
        set { _skeletonsDealSanity = value; }
    }

    [SerializeField] private int _skeletonsToSummon = 2;
    public int SkeletonsToSummon
    {
        get { return _skeletonsToSummon; }
        set { _skeletonsToSummon = value; }
    }

    [SerializeField] private int _maxSkeletons = 7;
    public int MaxSkeletons
    {
        get { return _maxSkeletons; }
        set { _maxSkeletons = value; }
    }

    [SerializeField] private float _summonRate = 1f;
    public float SummonRate
    {
        get { return _summonRate; }
        set { _summonRate = value; }
    }

    [SerializeField] private float _skeletonSummonMinRadius = 0.25f;
    public float SkeletonSummonMinRadius
    {
        get { return _skeletonSummonMinRadius; }
        set { _skeletonSummonMinRadius = value; }
    }

    [SerializeField] private float _skeletonSummonMaxRadius = 0.75f;
    public float SkeletonSummonMaxRadius
    {
        get { return _skeletonSummonMaxRadius; }
        set { _skeletonSummonMaxRadius = value; }
    }

    [SerializeField] private float _teleportTransitionDuration = 0.5f;
    public float TeleportTransitionDuration
    {
        get { return _teleportTransitionDuration; }
        set { _teleportTransitionDuration = value; }
    }

    [SerializeField] private float _teleportInvisibleDuration = 1f;
    public float TeleportInvisibleDuration
    {
        get { return _teleportInvisibleDuration; }
        set { _teleportInvisibleDuration = value; }
    }

    [SerializeField] private int _teleportInvisibleGranularity = 10;
    public int TeleportInvisibleGranularity
    {
        get { return _teleportInvisibleGranularity; }
        set { _teleportInvisibleGranularity = value; }
    }

    [SerializeField] private float _teleportRadius = 1.5f;
    public float TeleportRadius
    {
        get { return _teleportRadius; }
        set { _teleportRadius = value; }
    }

    [SerializeField] private float _forceTeleportRadius = 2.5f;
    public float ForceTeleportRadius
    {
        get { return _forceTeleportRadius; }
        set { _forceTeleportRadius = value; }
    }

    [SerializeField] private float _walkSpeed = 20f;
    public float WalkSpeed
    {
        get { return _walkSpeed; }
        set { _walkSpeed = value; }
    }

    [SerializeField] private float _passiveDuration = 2f;
    public float PassiveDuration
    {
        get { return _passiveDuration; }
        set { _passiveDuration = value; }
    }

    [SerializeField] private float _playerAvoidDistance = 2.5f;
    public float PlayerAvoidDistance
    {
        get { return _playerAvoidDistance; }
        set { _playerAvoidDistance = value; }
    }
}

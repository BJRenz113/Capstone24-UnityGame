using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeresyBossBlueWitch : Enemy
{
    public AudioClip spikePatternSound;
    public override void Start()
    {
        base.Start();
        enemyStateManager.TransitionToState(new HeresyBossBlueWitchPassiveState());
    }

    public override void FixedUpdate()
    {
        Vector3 playerPos = GameObject.FindWithTag("Player").transform.position;
        gameObject.GetComponent<SpriteRenderer>().flipX = gameObject.transform.position.x > playerPos.x;
        base.FixedUpdate();
    }

    [SerializeField] private GameObject _spikeObject;
    public GameObject SpikeObject
    {
        get { return _spikeObject; }
        set { _spikeObject = value; }
    }

    [SerializeField] private int _spikeLines = 2;
    public int SpikeLines
    {
        get { return _spikeLines; }
        set { _spikeLines = value; }
    }

    [SerializeField] private float _spikeLineDistance = 3f;
    public float SpikeLineDistance
    {
        get { return _spikeLineDistance; }
        set { _spikeLineDistance = value; }
    }

    [SerializeField] private int _spikeLineAngleOffset = 15;
    public int SpikeLineAngleOffset
    {
        get { return _spikeLineAngleOffset; }
        set { _spikeLineAngleOffset = value; }
    }

    [SerializeField] private int _spikeLineSpikeCount = 20;
    public int SpikeLineSpikeCount
    {
        get { return _spikeLineSpikeCount; }
        set { _spikeLineSpikeCount = value; }
    }

    [SerializeField] private float _spikeLineDuration = 0.75f;
    public float SpikeLineDuration
    {
        get { return _spikeLineDuration; }
        set { _spikeLineDuration = value; }
    }

    [SerializeField] private int _spikeCircles = 4;
    public int SpikeCircles
    {
        get { return _spikeCircles; }
        set { _spikeCircles = value; }
    }

    [SerializeField] private int _spikeCircleSpikeCount = 32;
    public int SpikeCircleSpikeCount
    {
        get { return _spikeCircleSpikeCount; }
        set { _spikeCircleSpikeCount = value; }
    }

    [SerializeField] private float _spikeCircleSpacing = 0.3f;
    public float SpikeCircleSpacing
    {
        get { return _spikeCircleSpacing; }
        set { _spikeCircleSpacing = value; }
    }

    [SerializeField] private float _spikeCircleTimeBetweenSpikes = 0.01f;
    public float SpikeCircleTimeBetweenSpikes
    {
        get { return _spikeCircleTimeBetweenSpikes; }
        set { _spikeCircleTimeBetweenSpikes = value; }
    }

    [SerializeField] private float _spikeCircleTimeBetweenCircles = 0.1f;
    public float SpikeCircleTimeBetweenCircles
    {
        get { return _spikeCircleTimeBetweenCircles; }
        set { _spikeCircleTimeBetweenCircles = value; }
    }

    [SerializeField] private float _spikeCircleCooldown = 0.75f;
    public float SpikeCircleCooldown
    {
        get { return _spikeCircleCooldown; }
        set { _spikeCircleCooldown = value; }
    }

    [SerializeField] private float _spikeTrapRadius = 1.2f;
    public float SpikeTrapRadius
    {
        get { return _spikeTrapRadius; }
        set { _spikeTrapRadius = value; }
    }

    [SerializeField] private int _spikeTrapSpikeCount = 32;
    public int SpikeTrapSpikeCount
    {
        get { return _spikeTrapSpikeCount; }
        set { _spikeTrapSpikeCount = value; }
    }

    [SerializeField] private float _spikeTrapTimeBetweenSpikes = 0.02f;
    public float SpikeTrapTimeBetweenSpikes
    {
        get { return _spikeTrapTimeBetweenSpikes; }
        set { _spikeTrapTimeBetweenSpikes = value; }
    }

    [SerializeField] private float _spikeTrapLineDelay = 0.3f;
    public float SpikeTrapLineDelay
    {
        get { return _spikeTrapLineDelay; }
        set { _spikeTrapLineDelay = value; }
    }

    [SerializeField] private float _walkSpeed = 30f;
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
}

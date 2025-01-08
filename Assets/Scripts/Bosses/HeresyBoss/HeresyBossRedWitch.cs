using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeresyBossRedWitch : Enemy
{

    public AudioClip fireballSound;
    public AudioClip fireWaveSound;
    public override void Start()
    {
        base.Start();
        enemyStateManager.TransitionToState(new HeresyBossRedWitchPassiveState());
    }

    public override void FixedUpdate()
    {
        Vector3 playerPos = GameObject.FindWithTag("Player").transform.position;
        gameObject.GetComponent<SpriteRenderer>().flipX = gameObject.transform.position.x > playerPos.x;
        base.FixedUpdate();
    }

    [SerializeField] private GameObject _emberObject;
    public GameObject EmberObject
    {
        get { return _emberObject; }
        set { _emberObject = value; }
    }

    [SerializeField] private GameObject _fireballObject;
    public GameObject FireballObject
    {
        get { return _fireballObject; }
        set { _fireballObject = value; }
    }

    [SerializeField] private int _fireWaves = 3;
    public int FireWaves
    {
        get { return _fireWaves; }
        set { _fireWaves = value; }
    }

    [SerializeField] private int _fireWavesAngleCoverage = 60;
    public int FireWavesAngleCoverage
    {
        get { return _fireWavesAngleCoverage; }
        set { _fireWavesAngleCoverage = value; }
    }

    [SerializeField] private int _fireWavesEmbersPerWave = 20;
    public int FireWavesEmbersPerWave
    {
        get { return _fireWavesEmbersPerWave; }
        set { _fireWavesEmbersPerWave = value; }
    }

    [SerializeField] private float _fireWavesEmberSpeed = 110f;
    public float FireWavesEmberSpeed
    {
        get { return _fireWavesEmberSpeed; }
        set { _fireWavesEmberSpeed = value; }
    }

    [SerializeField] private float _fireWavesTimeBetweenWave = 0.6f;
    public float FireWavesTimeBetweenWave
    {
        get { return _fireWavesTimeBetweenWave; }
        set { _fireWavesTimeBetweenWave = value; }
    }

    [SerializeField] private float _fireballSpeed = 240f;
    public float FireballSpeed
    {
        get { return _fireballSpeed; }
        set { _fireballSpeed = value; }
    }

    [SerializeField] private float _fireballExitDelay = 1f;
    public float FireballExitDelay
    {
        get { return _fireballExitDelay; }
        set { _fireballExitDelay = value; }
    }

    [SerializeField] private float _walkSpeed = 60f;
    public float WalkSpeed
    {
        get { return _walkSpeed; }
        set { _walkSpeed = value; }
    }

    [SerializeField] private float _passiveDurationMin = 2f;
    public float PassiveDurationMin
    {
        get { return _passiveDurationMin; }
        set { _passiveDurationMin = value; }
    }

    [SerializeField] private float _passiveDurationMax = 4f;
    public float PassiveDurationMax
    {
        get { return _passiveDurationMax; }
        set { _passiveDurationMax = value; }
    }

    [SerializeField] private float _forceAttackRadius = 0.6f;
    public float ForceAttackRadius
    {
        get { return _forceAttackRadius; }
        set { _forceAttackRadius = value; }
    }

    [SerializeField] private float _fireballRange = 2f;
    public float FireballRange
    {
        get { return _fireballRange; }
        set { _fireballRange = value; }
    }
}

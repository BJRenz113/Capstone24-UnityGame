using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViolenceBoss : Enemy
{

    public AudioClip spiralFlameSound;
    public AudioClip flameRingsSound;
    public AudioClip scytheThrowSound;

    public override void Start()
    {
        base.Start();
        enemyStateManager.TransitionToState(new ViolenceBossErraticState());
    }

    public override void FixedUpdate()
    {
        Vector3 playerPos = GameObject.FindWithTag("Player").transform.position;
        gameObject.GetComponent<SpriteRenderer>().flipX = gameObject.transform.position.x > playerPos.x;
        base.FixedUpdate();
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player == null) return;
        if(_OHKOAttackIsActive) player.TakeHealthDamage(player.MaxHealth, true);
    }

    private List<int> _attackQueue = new List<int>();
    public List<int> AttackQueue
    {
        get { return _attackQueue; }
        set { _attackQueue = value; }
    }

    [SerializeField] private float _erraticTeleportRadiusMax = 2f;
    public float ErraticTeleportRadiusMax
    {
        get { return _erraticTeleportRadiusMax; }
        set { _erraticTeleportRadiusMax = value; }
    }

    [SerializeField] private float _erraticTeleportRadiusMin = 0.5f;
    public float ErraticTeleportRadiusMin
    {
        get { return _erraticTeleportRadiusMin; }
        set { _erraticTeleportRadiusMin = value; }
    }

    [SerializeField] private int _erraticTeleportCount = 9;
    public int ErraticTeleportCount
    {
        get { return _erraticTeleportCount; }
        set { _erraticTeleportCount = value; }
    }

    [SerializeField] private int _erraticTeleportMinAngleChange = 45;
    public int ErraticTeleportMinAngleChange
    {
        get { return _erraticTeleportMinAngleChange; }
        set { _erraticTeleportMinAngleChange = value; }
    }

    [SerializeField] private float _erraticTeleportTime = 1f;
    public float ErraticTeleportTime
    {
        get { return _erraticTeleportTime; }
        set { _erraticTeleportTime = value; }
    }

    [SerializeField] private float _erraticTeleportSpeedupFactor = 0.75f;
    public float ErraticTeleportSpeedupFactor
    {
        get { return _erraticTeleportSpeedupFactor; }
        set { _erraticTeleportSpeedupFactor = value; }
    }

    [SerializeField] private float _erraticTeleportEndPause = 1f;
    public float ErraticTeleportEndPause
    {
        get { return _erraticTeleportEndPause; }
        set { _erraticTeleportEndPause = value; }
    }

    [SerializeField] private GameObject _darkStormWispObject;
    public GameObject DarkStormWispObject
    {
        get { return _darkStormWispObject; }
        set { _darkStormWispObject = value; }
    }

    [SerializeField] private List<int> _darkStormWaveCounts = new List<int>();
    public List<int> DarkStormWaveCounts
    {
        get { return _darkStormWaveCounts; }
        set { _darkStormWaveCounts = value; }
    }

    [SerializeField] private float _darkStormWaveTime = 1f;
    public float DarkStormWaveTime
    {
        get { return _darkStormWaveTime; }
        set { _darkStormWaveTime = value; }
    }

    [SerializeField] private float _darkStormSpeedupFactor = 0.75f;
    public float DarkStormSpeedupFactor
    {
        get { return _darkStormSpeedupFactor; }
        set { _darkStormSpeedupFactor = value; }
    }

    [SerializeField] private float _darkStormEndPause = 1f;
    public float DarkStormEndPause
    {
        get { return _darkStormEndPause; }
        set { _darkStormEndPause = value; }
    }

    private bool _OHKOAttackIsActive = false;
    public bool OHKOAttackIsActive
    {
        get { return _OHKOAttackIsActive; }
        set { _OHKOAttackIsActive = value; }
    }

    [SerializeField] private int _OHKOAttackShakes = 14;
    public int OHKOAttackShakes
    {
        get { return _OHKOAttackShakes; }
        set { _OHKOAttackShakes = value; }
    }

    [SerializeField] private float _OHKOAttackShakeTime = 0.1f;
    public float OHKOAttackShakeTime
    {
        get { return _OHKOAttackShakeTime; }
        set { _OHKOAttackShakeTime = value; }
    }

    [SerializeField] private float _OHKOAttackShakeSpeedupFactor = 1.15f;
    public float OHKOAttackShakeSpeedupFactor
    {
        get { return _OHKOAttackShakeSpeedupFactor; }
        set { _OHKOAttackShakeSpeedupFactor = value; }
    }

    [SerializeField] private float _OHKOAttackShakeDist = 0.1f;
    public float OHKOAttackShakeDist
    {
        get { return _OHKOAttackShakeDist; }
        set { _OHKOAttackShakeDist = value; }
    }

    [SerializeField] private float _OHKOAttackCooldown = 2f;
    public float OHKOAttackCooldown
    {
        get { return _OHKOAttackCooldown; }
        set { _OHKOAttackCooldown = value; }
    }

    [SerializeField] private GameObject _scytheThrowObject;
    public GameObject ScytheThrowObject
    {
        get { return _scytheThrowObject; }
        set { _scytheThrowObject = value; }
    }

    [SerializeField] private float _scytheThrowDuration = 4f;
    public float ScytheThrowDuration
    {
        get { return _scytheThrowDuration; }
        set { _scytheThrowDuration = value; }
    }

    [SerializeField] private float _scytheThrowDistance = 3f;
    public float ScytheThrowDistance
    {
        get { return _scytheThrowDistance; }
        set { _scytheThrowDistance = value; }
    }

    [SerializeField] private float _scytheThrowCooldown = 1f;
    public float ScytheThrowCooldown
    {
        get { return _scytheThrowCooldown; }
        set { _scytheThrowCooldown = value; }
    }

    [SerializeField] private GameObject _flameRingsWispObject;
    public GameObject FlameRingsWispObject
    {
        get { return _flameRingsWispObject; }
        set { _flameRingsWispObject = value; }
    }

    [SerializeField] private List<int> _flameRingsCounts = new List<int>();
    public List<int> FlameRingsCounts
    {
        get { return _flameRingsCounts; }
        set { _flameRingsCounts = value; }
    }

    [SerializeField] private float _flameRingsRadiusMax = 4f;
    public float FlameRingsRadiusMax
    {
        get { return _flameRingsRadiusMax; }
        set { _flameRingsRadiusMax = value; }
    }

    [SerializeField] private float _flameRingsRadiusMin = 0.5f;
    public float FlameRingsRadiusMin
    {
        get { return _flameRingsRadiusMin; }
        set { _flameRingsRadiusMin = value; }
    }

    [SerializeField] private float _flameRingsSummonTime = 1f;
    public float FlameRingsSummonTime
    {
        get { return _flameRingsSummonTime; }
        set { _flameRingsSummonTime = value; }
    }

    [SerializeField] private float _flameRingsSpeedupFactor = 0.75f;
    public float FlameRingsSpeedupFactor
    {
        get { return _flameRingsSpeedupFactor; }
        set { _flameRingsSpeedupFactor = value; }
    }

    [SerializeField] private float _flameRingsCooldown = 1f;
    public float FlameRingsCooldown
    {
        get { return _flameRingsCooldown; }
        set { _flameRingsCooldown = value; }
    }

    [SerializeField] private GameObject _spiralFlameObject;
    public GameObject SpiralFlameObject
    {
        get { return _spiralFlameObject; }
        set { _spiralFlameObject = value; }
    }

    [SerializeField] private int _spiralFlameSpirals = 2;
    public int SpiralFlameSpirals
    {
        get { return _spiralFlameSpirals; }
        set { _spiralFlameSpirals = value; }
    }

    [SerializeField] private int _spiralFlameAngleGranularity = 16;
    public int SpiralFlameAngleGranularity
    {
        get { return _spiralFlameAngleGranularity; }
        set { _spiralFlameAngleGranularity = value; }
    }

    [SerializeField] private int _spiralFlameLoops = 3;
    public int SpiralFlameLoops
    {
        get { return _spiralFlameLoops; }
        set { _spiralFlameLoops = value; }
    }

    [SerializeField] private float _spiralFlameLoopSpacing = 1f;
    public float SpiralFlameLoopSpacing
    {
        get { return _spiralFlameLoopSpacing; }
        set { _spiralFlameLoopSpacing = value; }
    }

    [SerializeField] private float _spiralFlameBatchTime = 0.25f;
    public float SpiralFlameBatchTime
    {
        get { return _spiralFlameBatchTime; }
        set { _spiralFlameBatchTime = value; }
    }

    [SerializeField] private float _spiralFlameSpeedupFactor = 0.95f;
    public float SpiralFlameSpeedupFactor
    {
        get { return _spiralFlameSpeedupFactor; }
        set { _spiralFlameSpeedupFactor = value; }
    }

    [SerializeField] private float _spiralFlameCooldown = 1f;
    public float SpiralFlameCooldown
    {
        get { return _spiralFlameCooldown; }
        set { _spiralFlameCooldown = value; }
    }
}

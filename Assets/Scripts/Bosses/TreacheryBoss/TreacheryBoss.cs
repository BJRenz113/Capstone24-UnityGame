using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreacheryBoss : Enemy
{
    public AudioClip playerVoidAttackSound;
    public AudioClip scatterVoidAttackSound;
    public AudioClip selfVoidAttackSound;
    public AudioClip spiralVoidAttack;
    public override void Start()
    {
        base.Start();
        AddAttacks();
        enemyStateManager.TransitionToState(new TreacheryBossIdleState());
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        GameObject playerObject = GameObject.FindWithTag("Player");

        Vector2 direction = (playerObject.transform.position - gameObject.transform.position).normalized;
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();

        rb.velocity = direction * _followSpeed * Time.fixedDeltaTime;

        UpdateVulnerability();
    }

    public override void TakeDamage(int baseDamage, bool skipArmor = false)
    {
        int damageResisted = baseDamage * _resistPercentage / 100;

        if (skipArmor || Armor.Count == 0)
        {
            CurrentHealth -= (baseDamage - damageResisted);

            if (CurrentHealth <= 0)
            {
                enemyStateManager.TransitionToState(deathStateToTrigger);
            }
        }
        else
        {
            Armor[Armor.Count - 1] -= (baseDamage - damageResisted);

            if (Armor[Armor.Count - 1] <= 0)
            {
                Armor.RemoveAt(Armor.Count - 1);
            }
        }
    }

    private void UpdateVulnerability()
    {
        GameObject[] torches = GameObject.FindGameObjectsWithTag("TreacheryBossTorch");
        int totalBurnPercentage = 0;

        for (int i = 0; i < torches.Length; i++)
        {
            TreacheryBossTorch torch = torches[i].GetComponent<TreacheryBossTorch>();

            totalBurnPercentage += torch.CurrentLightPercentage;
        }

        totalBurnPercentage /= 4;

        if(totalBurnPercentage >= _maxBurnPercentageNecessary)
        {
            _resistPercentage = 0;
        }
        else if(totalBurnPercentage == 0)
        {
            _resistPercentage = 100;
        }
        else
        {
            _resistPercentage = (_burnResistLevels - ((totalBurnPercentage - 1) / (_maxBurnPercentageNecessary / _burnResistLevels))) * (100 / (_burnResistLevels + 1));
        }

        gameObject.GetComponent<Renderer>().enabled = totalBurnPercentage > 0;
        gameObject.transform.GetChild(0).GetComponent<Canvas>().enabled = totalBurnPercentage > 0;
    }

    // attack values
    // self void = 10
    // player void = 11
    // X void = 12
    // scatter void = 13
    // spiral void = 14
    // torch shuffle = 20
    // torch attack = 21 (UNUSED)
    // torch extinguish = 22

    private void AddAttacks()
    {
        _attacks.Clear();

        // attack value, weight
        Dictionary<int, int> nearAttacks = new Dictionary<int, int> ();
        Dictionary<int, int> betweenAttacks = new Dictionary<int, int> ();
        Dictionary<int, int> farAttacks = new Dictionary<int, int> ();

        // for right now, i want the weights to be increments of 10%, but we very easily can microadjust post playtesting
        nearAttacks.Add(10, 7);
        nearAttacks.Add(14, 1);
        nearAttacks.Add(22, 2);

        betweenAttacks.Add(10, 1);
        betweenAttacks.Add(11, 2);
        betweenAttacks.Add(12, 3);
        betweenAttacks.Add(14, 3);
        betweenAttacks.Add(20, 2);

        farAttacks.Add(11, 4);
        farAttacks.Add(12, 1);
        farAttacks.Add(13, 3);
        farAttacks.Add(14, 2);

        // closeness value, attack dict
        _attacks.Add(0, nearAttacks);
        _attacks.Add(1, betweenAttacks);
        _attacks.Add(2, farAttacks);
    }

    // 0 if near, 1 if between, 2 if far
    public int GetCloseness()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        float dist = Vector2.Distance(playerObject.transform.position, gameObject.transform.position);

        if (dist < _nearRadius) return 0;
        else if (dist < _farRadius) return 1;
        else return 2;
    }

    // total burn percentages of all torches - one full torch would result in 25% (100/400)
    [SerializeField] private int _maxBurnPercentageNecessary = 40;
    public int MaxBurnPercentageNecessary
    {
        get { return _maxBurnPercentageNecessary; }
        set { _maxBurnPercentageNecessary = value; }
    }

    // 0 = 0 damage no matter what
    // >= MaxBurnPercentageNecessary = full damage dealt
    // otherwise, split into (number) unique resists (so for 4 it would be 80, 60, 40, 20 and the 0/100 for min/max)
    [SerializeField] private int _burnResistLevels = 4;
    public int BurnResistLevels
    {
        get { return _burnResistLevels; }
        set { _burnResistLevels = value; }
    }

    private int _resistPercentage = 0;
    public int ResistPercentage
    {
        get { return _resistPercentage; }
        set { _resistPercentage = value; }
    }

    [SerializeField] private float _nearRadius = 1f;
    public float NearRadius
    {
        get { return _nearRadius; }
        set { _nearRadius = value; }
    }

    [SerializeField] private float _farRadius = 2.5f;
    public float FarRadius
    {
        get { return _farRadius; }
        set { _farRadius = value; }
    }

    [SerializeField] private float _followSpeed = 30f;
    public float FollowSpeed
    {
        get { return _followSpeed; }
        set { _followSpeed = value; }
    }

    [SerializeField] private float _transitionCooldown = 1f;
    public float TransitionCooldown
    {
        get { return _transitionCooldown; }
        set { _transitionCooldown = value; }
    }

    [SerializeField] private float _xVoidRate = 0.08f;
    public float XVoidRate
    {
        get { return _xVoidRate; }
        set { _xVoidRate = value; }
    }

    [SerializeField] private float _xVoidCooldown = 1f;
    public float XVoidCooldown
    {
        get { return _xVoidCooldown; }
        set { _xVoidCooldown = value; }
    }

    [SerializeField] private float _xVoidSpacing = 0.4f;
    public float XVoidSpacing
    {
        get { return _xVoidSpacing; }
        set { _xVoidSpacing = value; }
    }

    [SerializeField] private int _xVoidSize = 10;
    public int XVoidSize
    {
        get { return _xVoidSize; }
        set { _xVoidSize = value; }
    }

    [SerializeField] private float _scatterVoidRate = 0.16f;
    public float ScatterVoidRate
    {
        get { return _scatterVoidRate; }
        set { _scatterVoidRate = value; }
    }

    [SerializeField] private float _scatterVoidRadius = 1f;
    public float ScatterVoidRadius
    {
        get { return _scatterVoidRadius; }
        set { _scatterVoidRadius = value; }
    }

    [SerializeField] private int _scatterVoidPlayerFrequency = 6;
    public int ScatterVoidPlayerFrequency
    {
        get { return _scatterVoidPlayerFrequency; }
        set { _scatterVoidPlayerFrequency = value; }
    }

    [SerializeField] private int _scatterVoidCount = 18;
    public int ScatterVoidCount
    {
        get { return _scatterVoidCount; }
        set { _scatterVoidCount = value; }
    }

    [SerializeField] private float _spiralVoidRate = 0.08f;
    public float SpiralVoidRate
    {
        get { return _spiralVoidRate; }
        set { _spiralVoidRate = value; }
    }

    [SerializeField] private int _spiralVoidLoops = 4;
    public int SpiralVoidLoops
    {
        get { return _spiralVoidLoops; }
        set { _spiralVoidLoops = value; }
    }

    [SerializeField] private int _spiralVoidSimultaneousSpawns = 2;
    public int SpiralVoidSimultaneousSpawns
    {
        get { return _spiralVoidSimultaneousSpawns; }
        set { _spiralVoidSimultaneousSpawns = value; }
    }

    [SerializeField] private int _spiralVoidAngleGranularity = 16;
    public int SpiralVoidAngleGranularity
    {
        get { return _spiralVoidAngleGranularity; }
        set { _spiralVoidAngleGranularity = value; }
    }

    [SerializeField] private float _spiralVoidCooldown = 1.5f;
    public float SpiralVoidCooldown
    {
        get { return _spiralVoidCooldown; }
        set { _spiralVoidCooldown = value; }
    }

    [SerializeField] private float _spiralVoidSpacing = 1f;
    public float SpiralVoidSpacing
    {
        get { return _spiralVoidSpacing; }
        set { _spiralVoidSpacing = value; }
    }

    [SerializeField] private float _selfVoidDuration = 5f;
    public float SelfVoidDuration
    {
        get { return _selfVoidDuration; }
        set { _selfVoidDuration = value; }
    }

    [SerializeField] private float _playerVoidDuration = 3f;
    public float PlayerVoidDuration
    {
        get { return _playerVoidDuration; }
        set { _playerVoidDuration = value; }
    }

    [SerializeField] private float _torchShuffleCooldown = 1f;
    public float TorchShuffleCooldown
    {
        get { return _torchShuffleCooldown; }
        set { _torchShuffleCooldown = value; }
    }

    [SerializeField] private float _torchExtinguishCooldown = 3f;
    public float TorchExtinguishCooldown
    {
        get { return _torchExtinguishCooldown; }
        set { _torchExtinguishCooldown = value; }
    }

    [SerializeField] private GameObject _smallMiscVoidObject;
    public GameObject SmallMiscVoidObject
    {
        get { return _smallMiscVoidObject; }
        set { _smallMiscVoidObject = value; }
    }

    [SerializeField] private GameObject _mediumPlayerVoidObject;
    public GameObject MediumPlayerVoidObject
    {
        get { return _mediumPlayerVoidObject; }
        set { _mediumPlayerVoidObject = value; }
    }

    [SerializeField] private GameObject _largeSelfVoidObject;
    public GameObject LargeSelfVoidObject
    {
        get { return _largeSelfVoidObject; }
        set { _largeSelfVoidObject = value; }
    }

    private Dictionary<int, Dictionary<int, int>> _attacks = new Dictionary<int, Dictionary<int, int>>();
    public Dictionary<int, Dictionary<int, int>> Attacks
    {
        get { return _attacks; }
        set { _attacks = value; }
    }
}

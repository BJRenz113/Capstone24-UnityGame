using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerStateManager _playerStateManager;
    private PlayerInputManager _playerInputManager;
    private Teleporter currentTeleporter;
    private AudioSource audioSource;
    public AudioClip loseMoneySound;
    public AudioClip loseHealthSound;

    public AudioClip gainHealthSound;

    public AudioClip dashSound;
    public AudioClip moneyUpSound;
    public bool _dashSoundCooldown = false;
    private float _dashSoundCooldownDuration = 0.25f;

    public AudioClip rangedSound;
    private bool _rangedSoundCooldown = false;
    private float _rangedSoundCooldownDuration = 0.5f;

    // Start is called before the first frame update
    public void Start()
    {
        _playerStateManager = new PlayerStateManager();
        _playerInputManager = new PlayerInputManager(this);

        if(GameObject.FindGameObjectsWithTag("Player").Length > 1)
        {
            GameObject.Destroy(gameObject);
            return;
        }

        if (_isDebugMode)
        {
            MaxHealth = 999;
            MaxSanity = 999;
            MaxMoney = 999;
            AddMoney(MaxMoney);
            MaxDashes = 999;
            MaxAttacks = 999;
            DashDamage = 999;
            MeleeDamage = 999;
            RangedDamage = 999;
            SelfStabDamage = 999;
            SpinAttackDamage = 999;
        }

        CurrentHealth = MaxHealth;
        CurrentSanity = MaxSanity;
        AddMoney(InitialMoney);
        CurrentDashes = MaxDashes;
        CurrentAttacks = MaxAttacks;

        audioSource = gameObject.GetComponent<AudioSource>();

        _areasClearedDict["Gluttony"] = false;
        _areasClearedDict["Treachery"] = false;
        _areasClearedDict["Wrath"] = false;
        _areasClearedDict["Greed"] = false;
        _areasClearedDict["Heresy"] = false;
        _areasClearedDict["Violence"] = false;

        StartCoroutine(SanityDrainCoroutine());

        _playerStateManager.Start(this);
    }

    public void Update()
    {
        _playerInputManager.RefreshInputs();

        if(Input.GetButtonDown("Cancel") && _isDebugMode)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < enemies.Length; i++)
            {
                GameObject.Destroy(enemies[i]);
            }
        }
    }

    public void FixedUpdate()
    {
        _playerStateManager.FixedUpdate();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        _playerStateManager.OnTriggerEnter2D(other);
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        _playerStateManager.OnTriggerStay2D(other);
    }

    public PlayerStateManager GetPlayerStateManager()
    {
        return _playerStateManager;
    }

    public PlayerInputManager GetPlayerInputManager()
    {
        return _playerInputManager;
    }

public void PlayRangedSound()
{
    if (!_rangedSoundCooldown && audioSource && rangedSound != null)
    {
        audioSource.PlayOneShot(rangedSound);
        _rangedSoundCooldown = true;
        StartCoroutine(RangedSoundCooldown());
    }
}

private IEnumerator RangedSoundCooldown()
{
    yield return new WaitForSeconds(_rangedSoundCooldownDuration);
    _rangedSoundCooldown = false;
}

public void PlayDashSound()
{
    if (!_dashSoundCooldown && audioSource && dashSound != null)
    {
        audioSource.PlayOneShot(dashSound);
        _dashSoundCooldown = true;
        StartCoroutine(dashSoundCooldown());
    }
}

private IEnumerator dashSoundCooldown()
{
    yield return new WaitForSeconds(_dashSoundCooldownDuration);
    _dashSoundCooldown = false;
}

    private void TryTeleport()
    {
        // Check if the player is near a teleporter
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Teleporter"))
            {
                Teleport(collider.GetComponent<Teleporter>());
                break;
            }
        }
    }



    private void Teleport(Teleporter teleporter)
    {
        if (currentTeleporter == null)
        {
            // First teleporter activated
            currentTeleporter = teleporter;
        }
        else
        {
            // Second teleporter activated, perform teleportation
            transform.position = teleporter.GetTeleportPosition();
            currentTeleporter = null;
        }
    }

    // helper methods other classes can use

    // heal or buff player

    public void AddMoney(int value)
    {
        CurrentMoney += value;
        if (CurrentMoney > MaxMoney) CurrentMoney = MaxMoney;
        // if (moneyUpSound != null) {
        //     audioSource.PlayOneShot(moneyUpSound);
        // }
    }

    public void HealHealth(int value)
    {
        CurrentHealth += value;
        if (CurrentHealth > MaxHealth) CurrentHealth = MaxHealth;

        if (gainHealthSound != null)
        {
            audioSource.PlayOneShot(gainHealthSound);
        }
    }

    public void HealSanity(int value)
    {
        CurrentSanity += value;
        if (CurrentSanity > MaxSanity) CurrentSanity = MaxSanity;
    }

    public void BuffMaxHealth(int value, bool changeCurrent = true)
    {
        MaxHealth += value;
        if (changeCurrent) HealHealth(value);
    }

    public void BuffMaxSanity(int value, bool changeCurrent = true)
    {
        MaxSanity += value;
        if (changeCurrent) HealSanity(value);
    }

    // damage or hinder player

    public void LoseMoney(int value)
    {
        CurrentMoney -= value;
        if(CurrentMoney < 0) CurrentMoney = 0;

        if (loseMoneySound != null)
        {
            audioSource.PlayOneShot(loseMoneySound);
        }
    }

    public void TakeHealthDamage(int baseDamage, bool skipResist = false)
    {
        if (_isDead) return;
        if (_hasHealthIFrames) return;
        if (!skipResist && (_isHealthInvulnerable || baseDamage <= _minHealthToHurt)) return;

        int damageResisted = baseDamage * _resistHealthPercentage / 100;

        if (skipResist)
        {
            damageResisted = 0;
        }

        if (loseHealthSound != null)
        {
            audioSource.PlayOneShot(loseHealthSound);
        }

        int damageDealt = baseDamage - damageResisted;
        if (damageDealt > 0) StartCoroutine(StartHealthIFrames());
        _currentHealth -= damageDealt;

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            _playerStateManager.TransitionToState(new DeathState());
        }
    }

    public void TakeSanityDamage(int baseDamage, bool skipResist = false)
    {
        if (_isDead) return;
        if (_hasSanityIFrames) return;
        if (!skipResist && (_isSanityInvulnerable || baseDamage <= _minSanityToHurt)) return;

        int damageResisted = baseDamage * _resistSanityPercentage / 100;

        if (skipResist)
        {
            damageResisted = 0;
        }

        int damageDealt = baseDamage - damageResisted;
        if (damageDealt > 0) StartCoroutine(StartSanityIFrames());
        _currentSanity -= damageDealt;

        if (_currentSanity <= 0)
        {
            _currentSanity = 0;
            _playerStateManager.TransitionToState(new DeathState());
        }
    }

    private IEnumerator FlickerSpriteRenderer(float duration)
    {
        int flickerCount = (int) Math.Ceiling(duration * 5);

        for(int i = 0; i < flickerCount; i++)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds((duration / flickerCount) / 2);
            GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds((duration / flickerCount) / 2);
        }
    }

    private IEnumerator StartHealthIFrames()
    {
        _hasHealthIFrames = true;
        StartCoroutine(FlickerSpriteRenderer(_healthIFramesDuration));
        yield return new WaitForSeconds(_healthIFramesDuration);
        _hasHealthIFrames = false;
    }

    private IEnumerator StartSanityIFrames()
    {
        _hasSanityIFrames = true;
        StartCoroutine(FlickerSpriteRenderer(_sanityIFramesDuration));
        yield return new WaitForSeconds(_sanityIFramesDuration);
        _hasSanityIFrames = false;
    }

    private IEnumerator SanityDrainCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_sanityDrainPeriod);

            if (_currentSanity > _sanityDrainCap)
            {
                _currentSanity--;

                if (_currentSanity <= 0)
                {
                    _currentSanity = 0;
                    _playerStateManager.TransitionToState(new DeathState());
                }
            }
        }
    }

    private Coroutine _dashCooldownCoroutine;

    public void StartDashCooldown()
    {
        if (_dashCooldownCoroutine == null)
        {
            _dashCooldownCoroutine = StartCoroutine(PerformDashCooldown());
        }
        else
        {
            StopCoroutine(_dashCooldownCoroutine);
            _dashCooldownCoroutine = StartCoroutine(PerformDashCooldown());
        }
    }

    private IEnumerator PerformDashCooldown()
    {
        yield return new WaitForSeconds(_dashCooldown);
        _currentDashes = _maxDashes;
    }

    private Coroutine _meleeCooldownCoroutine;

    public void StartMeleeCooldown()
    {
        if (_meleeCooldownCoroutine == null)
        {
            _meleeCooldownCoroutine = StartCoroutine(PerformMeleeCooldown());
        }
        else
        {
            StopCoroutine(_meleeCooldownCoroutine);
            _meleeCooldownCoroutine = StartCoroutine(PerformMeleeCooldown());
        }
    }

    private IEnumerator PerformMeleeCooldown()
    {
        yield return new WaitForSeconds(_meleeAttackCooldown);
        _currentAttacks = _maxAttacks;
    }

    private Coroutine _spinAttackCooldownCoroutine;

    public void StartSpinAttackCooldown()
    {
        if (_spinAttackCooldownCoroutine == null)
        {
            _spinAttackCooldownCoroutine = StartCoroutine(PerformSpinAttackCooldown());
        }
        else
        {
            StopCoroutine(_spinAttackCooldownCoroutine);
            _spinAttackCooldownCoroutine = StartCoroutine(PerformSpinAttackCooldown());
        }
    }

    private IEnumerator PerformSpinAttackCooldown()
    {
        _canSpinAttack = false;
        yield return new WaitForSeconds(_spinAttackCooldown);
        _canSpinAttack = true;
    }

    // min/max properties that can be changed by upgrades
    [SerializeField] private bool _isDebugMode = false;
    public bool IsDebugMode
    {
        get { return _isDebugMode; }
        set { _isDebugMode = value; }
    }

    [SerializeField] private int _maxHealth = 100;
    public int MaxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }

    [SerializeField] private int _maxSanity = 100;
    public int MaxSanity
    {
        get { return _maxSanity; }
        set { _maxSanity = value; }
    }

    [SerializeField] private int _maxMoney = 100;
    public int MaxMoney
    {
        get { return _maxMoney; }
        set { _maxMoney = value; }
    }

    [SerializeField] private int _initialMoney = 20;
    public int InitialMoney
    {
        get { return _initialMoney; }
        set { _initialMoney = value; }
    }

    [SerializeField] private float _moveSpeed = 80f;
    public float MoveSpeed
    {
        get { return _moveSpeed; }
        set { _moveSpeed = value; }
    }

    [SerializeField] private float _heavyMoveSpeed = 20f;
    public float HeavyMoveSpeed
    {
        get { return _heavyMoveSpeed; }
        set { _heavyMoveSpeed = value; }
    }

    [SerializeField] private int _heavyMoveResistPercentageBonus = 50;
    public int HeavyMoveResistPercentageBonus
    {
        get { return _heavyMoveResistPercentageBonus; }
        set { _heavyMoveResistPercentageBonus = value; }
    }

    [SerializeField] private int _heavyMoveMinDamageBonus = 1;
    public int HeavyMoveMinDamageBonus
    {
        get { return _heavyMoveMinDamageBonus; }
        set { _heavyMoveMinDamageBonus = value; }
    }

    [SerializeField] private float _dashDistance = 1.4f;
    public float DashDistance
    {
        get { return _dashDistance; }
        set { _dashDistance = value; }
    }

    [SerializeField] private float _dashCooldown = 0.5f;
    public float DashCooldown
    {
        get { return _dashCooldown; }
        set { _dashCooldown = value; }
    }

    [SerializeField] private int _dashDamage = 8;
    public int DashDamage
    {
        get { return _dashDamage; }
        set { _dashDamage = value; }
    }

    [SerializeField] private float _dashDuration = 0.17f;
    public float DashDuration
    {
        get { return _dashDuration; }
        set { _dashDuration = value; }
    }

    [SerializeField] private int _maxDashes = 1;
    public int MaxDashes
    {
        get { return _maxDashes; }
        set { _maxDashes = value; }
    }

    [SerializeField] private int _minHealthToHurt = 0;
    public int MinHealthToHurt
    {
        get { return _minHealthToHurt; }
        set { _minHealthToHurt = value; }
    }

    [SerializeField] private int _minSanityToHurt = 0;
    public int MinSanityToHurt
    {
        get { return _minSanityToHurt; }
        set { _minSanityToHurt = value; }
    }

    [SerializeField] private int _resistHealthPercentage = 0;
    public int ResistHealthPercentage
    {
        get { return _resistHealthPercentage; }
        set { _resistHealthPercentage = value; }
    }

    [SerializeField] private int _resistSanityPercentage = 0;
    public int ResistSanityPercentage
    {
        get { return _resistSanityPercentage; }
        set { _resistSanityPercentage = value; }
    }

    [SerializeField] private float _healthIFramesDuration = 0.5f;
    public float HealthIFramesDuration
    {
        get { return _healthIFramesDuration; }
        set { _healthIFramesDuration = value; }
    }

    [SerializeField] private float _sanityIFramesDuration = 0.5f;
    public float SanityIFramesDuration
    {
        get { return _sanityIFramesDuration; }
        set { _sanityIFramesDuration = value; }
    }

    [SerializeField] private float _sanityDrainPeriod = 15f;
    public float SanityDrainPeriod
    {
        get { return _sanityDrainPeriod; }
        set { _sanityDrainPeriod = value; }
    }

    [SerializeField] private int _sanityDrainCap = 10;
    public int SanityDrainCap
    {
        get { return _sanityDrainCap; }
        set { _sanityDrainCap = value; }
    }

    [SerializeField] private int _sanityRegenOnRoomLeave = 5;
    public int SanityRegenOnRoomLeave
    {
        get { return _sanityRegenOnRoomLeave; }
        set { _sanityRegenOnRoomLeave = value; }
    }

    // Properties that represent the current state
    private bool _isDead = false;
    public bool IsDead
    {
        get { return _isDead; }
        set { _isDead = value; }
    }

    private int _currentHealth = 0;
    public int CurrentHealth
    {
        get { return _currentHealth; }
        set { _currentHealth = value; }
    }

    private int _currentSanity = 0;
    public int CurrentSanity
    {
        get { return _currentSanity; }
        set { _currentSanity = value; }
    }

    private string _textUI = "Limbo (Room Cleared)";
    public string getText
    {
        get { return _textUI; }
        set { _textUI = value; }
    }

    private int _currentMoney = 0;
    public int CurrentMoney
    {
        get { return _currentMoney; }
        set { _currentMoney = value; }
    }

    private int _currentDashes = 0;
    public int CurrentDashes
    {
        get { return _currentDashes; }
        set { _currentDashes = value; }
    }

    private int _currentAttacks = 0;
    public int CurrentAttacks
    {
        get { return _currentAttacks; }
        set { _currentAttacks = value; }
    }

    private bool _canSpinAttack = true;
    public bool CanSpinAttack
    {
        get { return _canSpinAttack; }
        set { _canSpinAttack = value; }
    }

    private bool _isHealthInvulnerable = false;
    public bool IsHealthInvulnerable
    {
        get { return _isHealthInvulnerable; }
        set { _isHealthInvulnerable = value; }
    }

    private bool _hasHealthIFrames = false;
    public bool HasHealthIFrames
    {
        get { return _hasHealthIFrames; }
        set { _hasHealthIFrames = value; }
    }

    private bool _isSanityInvulnerable = false;
    public bool IsSanityInvulnerable
    {
        get { return _isSanityInvulnerable; }
        set { _isSanityInvulnerable = value; }
    }

    private bool _hasSanityIFrames = false;
    public bool HasSanityIFrames
    {
        get { return _hasSanityIFrames; }
        set { _hasSanityIFrames = value; }
    }

    // trackers
    private int _currentRoomsCleared = 0;
    public int CurrentRoomsCleared
    {
        get { return _currentRoomsCleared; }
        set { _currentRoomsCleared = value; }
    }

    private int _totalRoomsCleared = 0;
    public int TotalRoomsCleared
    {
        get { return _totalRoomsCleared; }
        set { _totalRoomsCleared = value; }
    }

    private int _currentAreasCleared = 0;
    public int CurrentAreasCleared
    {
        get { return _currentAreasCleared; }
        set { _currentAreasCleared = value; }
    }

    private Dictionary<string, bool> _areasClearedDict = new Dictionary<string, bool>();
    public Dictionary<string, bool> AreasClearedDict
    {
        get { return _areasClearedDict; }
        set { _areasClearedDict = value; }
    }

    // attack and damage stuff
    [SerializeField] private GameObject _meleeWeaponObject;
    public GameObject MeleeWeaponObject
    {
        get { return _meleeWeaponObject; }
        set { _meleeWeaponObject = value; }
    }

    [SerializeField] private int _maxAttacks = 3;
    public int MaxAttacks
    {
        get { return _maxAttacks; }
        set { _maxAttacks = value; }
    }

    [SerializeField] private float _consecutiveMeleeDamageMultiplier = 1.05f;
    public float ConsecutiveMeleeDamageMultiplier
    {
        get { return _consecutiveMeleeDamageMultiplier; }
        set { _consecutiveMeleeDamageMultiplier = value; }
    }

    [SerializeField] private int _meleeDamage = 10;
    public int MeleeDamage
    {
        get { return _meleeDamage; }
        set { _meleeDamage = value; }
    }

    [SerializeField] private float _meleeWeaponOffset = 0.25f;
    public float MeleeWeaponOffset
    {
        get { return _meleeWeaponOffset; }
        set { _meleeWeaponOffset = value; }
    }

    [SerializeField] private float _meleeAttackTime = 0.35f;
    public float MeleeAttackTime
    {
        get { return _meleeAttackTime; }
        set { _meleeAttackTime = value; }
    }

    [SerializeField] private float _meleeAttackCooldown = 0.5f;
    public float MeleeAttackCooldown
    {
        get { return _meleeAttackCooldown; }
        set { _meleeAttackCooldown = value; }
    }

    [SerializeField] private GameObject _rangedObject;
    public GameObject RangedObject
    {
        get { return _rangedObject; }
        set { _rangedObject = value; }
    }

    [SerializeField] private float _rangedThrowSpeed = 100f;
    public float RangedThrowSpeed
    {
        get { return _rangedThrowSpeed; }
        set { _rangedThrowSpeed = value; }
    }

    [SerializeField] private float _rangedThrowStateDuration = 0.25f;
    public float RangedThrowStateDuration
    {
        get { return _rangedThrowStateDuration; }
        set { _rangedThrowStateDuration = value; }
    }

    [SerializeField] private float _rangedMoveStateDuration = 0.25f;
    public float RangedMoveStateDuration
    {
        get { return _rangedMoveStateDuration; }
        set { _rangedMoveStateDuration = value; }
    }

    [SerializeField] private float _rangedLifespan = 1.75f;
    public float RangedLifespan
    {
        get { return _rangedLifespan; }
        set { _rangedLifespan = value; }
    }

    [SerializeField] private int _rangedDamage = 5;
    public int RangedDamage
    {
        get { return _rangedDamage; }
        set { _rangedDamage = value; }
    }

    [SerializeField] private int _spinAttackDamage = 5;
    public int SpinAttackDamage
    {
        get { return _spinAttackDamage; }
        set { _spinAttackDamage = value; }
    }

    [SerializeField] private int _spinAttackHits = 3;
    public int SpinAttackHits
    {
        get { return _spinAttackHits; }
        set { _spinAttackHits = value; }
    }

    [SerializeField] private float _spinAttackMoveSpeedMultiplier = 1.5f;
    public float SpinAttackMoveSpeedMultiplier
    {
        get { return _spinAttackMoveSpeedMultiplier; }
        set { _spinAttackMoveSpeedMultiplier = value; }
    }

    [SerializeField] private GameObject _spinAttackWeaponObject;
    public GameObject SpinAttackWeaponObject
    {
        get { return _spinAttackWeaponObject; }
        set { _spinAttackWeaponObject = value; }
    }

    [SerializeField] private float _spinAttackTime = 0.6f;
    public float SpinAttackTime
    {
        get { return _spinAttackTime; }
        set { _spinAttackTime = value; }
    }

    [SerializeField] private float _spinAttackCooldown = 1f;
    public float SpinAttackCooldown
    {
        get { return _spinAttackCooldown; }
        set { _spinAttackCooldown = value; }
    }

    [SerializeField] private float _selfStabDuration = 1f;
    public float SelfStabDuration
    {
        get { return _selfStabDuration; }
        set { _selfStabDuration = value; }
    }

    [SerializeField] private int _selfStabDamage = 15;
    public int SelfStabDamage
    {
        get { return _selfStabDamage; }
        set { _selfStabDamage = value; }
    }
}

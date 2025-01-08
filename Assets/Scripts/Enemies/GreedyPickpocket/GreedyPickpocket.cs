using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreedyPickpocket : Enemy
{

    public AudioClip dashSound;
    public override void Start()
    {
        base.Start();

        deathStateToTrigger = new GreedyPickpocketDeathState();

        int areas = GetAreasCleared();
        StealAmount = StealAmountList[areas];

        UnityEngine.AI.NavMeshAgent agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();

        agent.speed = _speed * Time.fixedDeltaTime;
        agent.angularSpeed = _angularSpeed * Time.fixedDeltaTime;
        agent.acceleration = _acceleration * Time.fixedDeltaTime;

        agent.updateRotation = false;
        agent.updateUpAxis = false;

        enemyStateManager.TransitionToState(new GreedyPickpocketDashState());
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
        if (gameObject.GetComponent<Animator>().GetBool("IsRunning") && _canSteal)
        {
            int actualMoneyStolen = player.CurrentMoney;
            player.LoseMoney(_stealAmount);
            actualMoneyStolen -= player.CurrentMoney;
            TotalMoneyStolen += actualMoneyStolen;
            StartCoroutine(StealCooldownPeriod());
        }
    }

    private IEnumerator StealCooldownPeriod()
    {
        _canSteal = false;
        yield return new WaitForSeconds(_stealCooldown);
        _canSteal = true;
    }

    private bool _canSteal = true;

    private int _totalMoneyStolen = 0;
    public int TotalMoneyStolen
    {
        get { return _totalMoneyStolen; }
        set { _totalMoneyStolen = value; }
    }

    [SerializeField] private List<int> _stealAmountList = new List<int>() { 5, 5, 5, 5, 5, 5 };
    public List<int> StealAmountList
    {
        get { return _stealAmountList; }
        set { _stealAmountList = value; }
    }

    private int _stealAmount = 5;
    public int StealAmount
    {
        get { return _stealAmount; }
        set { _stealAmount = value; }
    }

    [SerializeField] private float _stealRecoveryFactor = 0.75f;
    public float StealRecoveryFactor
    {
        get { return _stealRecoveryFactor; }
        set { _stealRecoveryFactor = value; }
    }

    [SerializeField] private float _stealCooldown = 1f;
    public float StealCooldown
    {
        get { return _stealCooldown; }
        set { _stealCooldown = value; }
    }

    [SerializeField] private float _stealRadius = 4f;
    public float StealRadius
    {
        get { return _stealRadius; }
        set { _stealRadius = value; }
    }

    [SerializeField] private float _speed = 90f;
    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    [SerializeField] private float _angularSpeed = 360f;
    public float AngularSpeed
    {
        get { return _angularSpeed; }
        set { _angularSpeed = value; }
    }

    [SerializeField] private float _acceleration = 720f;
    public float Acceleration
    {
        get { return _acceleration; }
        set { _acceleration = value; }
    }

    [SerializeField] private float _dashDurationMin = 0.7f;
    public float DashDurationMin
    {
        get { return _dashDurationMin; }
        set { _dashDurationMin = value; }
    }

    [SerializeField] private float _dashDurationMax = 1.3f;
    public float DashDurationMax
    {
        get { return _dashDurationMax; }
        set { _dashDurationMax = value; }
    }

    [SerializeField] private float _dashCooldownMin = 0.6f;
    public float DashCooldownMin
    {
        get { return _dashCooldownMin; }
        set { _dashCooldownMin = value; }
    }

    [SerializeField] private float _dashCooldownMax = 1.0f;
    public float DashCooldownMax
    {
        get { return _dashCooldownMax; }
        set { _dashCooldownMax = value; }
    }
}

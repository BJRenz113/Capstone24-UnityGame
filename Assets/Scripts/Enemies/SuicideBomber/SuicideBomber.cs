using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideBomber : Enemy
{
    public AudioClip explodeSound;
    
    public override void Start()
    {
        base.Start();
        deathStateToTrigger = new SuicideBomberDeathState();

        int areas = GetAreasCleared();
        ExplosionDamage = ExplosionDamageList[areas];

        UnityEngine.AI.NavMeshAgent agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();

        agent.speed = _speed * Time.fixedDeltaTime;
        agent.angularSpeed = _angularSpeed * Time.fixedDeltaTime;
        agent.acceleration = _acceleration * Time.fixedDeltaTime;

        agent.updateRotation = false;
        agent.updateUpAxis = false;

        enemyStateManager.TransitionToState(new SuicideBomberPatrolState());
    }

    public override void FixedUpdate()
    {
        Vector3 playerPos = GameObject.FindWithTag("Player").transform.position;
        gameObject.GetComponent<SpriteRenderer>().flipX = gameObject.transform.position.x > playerPos.x;
        base.FixedUpdate();
    }

    [SerializeField] private float _patrolSpeed = 20f;
    public float PatrolSpeed
    {
        get { return _patrolSpeed; }
        set { _patrolSpeed = value; }
    }

    [SerializeField] private float _patrolRadius = 0.5f;
    public float PatrolRadius
    {
        get { return _patrolRadius; }
        set { _patrolRadius = value; }
    }

    [SerializeField] private float _speed = 10f;
    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    [SerializeField] private float _angularSpeed = 45f;
    public float AngularSpeed
    {
        get { return _angularSpeed; }
        set { _angularSpeed = value; }
    }

    [SerializeField] private float _acceleration = 20f;
    public float Acceleration
    {
        get { return _acceleration; }
        set { _acceleration = value; }
    }

    [SerializeField] private float _chaseRadius = 2f;
    public float ChaseRadius
    {
        get { return _chaseRadius; }
        set { _chaseRadius = value; }
    }

    [SerializeField] private GameObject _explosionObject;
    public GameObject ExplosionObject
    {
        get { return _explosionObject; }
        set { _explosionObject = value; }
    }

    private int _explosionDamage;
    public int ExplosionDamage
    {
        get { return _explosionDamage; }
        set { _explosionDamage = value; }
    }

    [SerializeField] private List<int> _explosionDamageList = new List<int>() {30, 30, 30, 30, 30, 30};
    public List<int> ExplosionDamageList
    {
        get { return _explosionDamageList; }
        set { _explosionDamageList = value; }
    }

    [SerializeField] private float _explosionDelay = 0.8f;
    public float ExplosionDelay
    {
        get { return _explosionDelay; }
        set { _explosionDelay = value; }
    }

    [SerializeField] private float _explosionRadius = 0.25f;
    public float ExplosionRadius
    {
        get { return _explosionRadius; }
        set { _explosionRadius = value; }
    }
}

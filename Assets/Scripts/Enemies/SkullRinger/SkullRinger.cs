using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullRinger : Enemy
{

    public AudioClip throwSound;
    public override void Start()
    {
        base.Start();
        deathStateToTrigger = new SkullRingerDeathState();
        HomePosition = gameObject.transform.position;
        enemyStateManager.TransitionToState(new SkullRingerMoveState());
    }

    public override void FixedUpdate()
    {
        Vector3 playerPos = GameObject.FindWithTag("Player").transform.position;
        gameObject.GetComponent<SpriteRenderer>().flipX = gameObject.transform.position.x > playerPos.x;
        base.FixedUpdate();
    }

    // initial min/max properties
    [SerializeField] private List<GameObject> _rings = new List<GameObject>();
    public List<GameObject> Rings
    {
        get { return _rings; }
        set { _rings = value; }
    }

    [SerializeField] private Vector3 _ringOffset = new Vector3(0f, -0.25f, 0f);
    public Vector3 RingOffset
    {
        get { return _ringOffset; }
        set { _ringOffset = value; }
    }

    private Vector2 _homePosition;
    public Vector2 HomePosition
    {
        get { return _homePosition; }
        set { _homePosition = value; }
    }

    [SerializeField] private float _patrolSpeed = 20f;
    public float PatrolSpeed
    {
        get { return _patrolSpeed; }
        set { _patrolSpeed = value; }
    }

    [SerializeField] private float _patrolRadius = 0.75f;
    public float PatrolRadius
    {
        get { return _patrolRadius; }
        set { _patrolRadius = value; }
    }

    [SerializeField] private float _throwRangeMin = 1f;
    public float ThrowRangeMin
    {
        get { return _throwRangeMin; }
        set { _throwRangeMin = value; }
    }

    [SerializeField] private float _throwRangeMax = 3f;
    public float ThrowRangeMax
    {
        get { return _throwRangeMax; }
        set { _throwRangeMax = value; }
    }

    [SerializeField] private float _throwSpeed = 240f;
    public float ThrowSpeed
    {
        get { return _throwSpeed; }
        set { _throwSpeed = value; }
    }
}

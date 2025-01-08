using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeresyBossSkeleton : Enemy
{

    public AudioClip attackSound;
    public override void Start()
    {
        base.Start();
        enemyStateManager.TransitionToState(new HeresyBossSkeletonWalkState());
        if (_emitEmbers) StartCoroutine(EmberEmission());
    }

    private IEnumerator EmberEmission()
    {
        while(true)
        {
            yield return new WaitForSeconds(_emissionRate);

            GameObject newEmber = GameObject.Instantiate(_ember);
            newEmber.transform.position = gameObject.transform.position;
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player == null) return;

        _canAttack = false;
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player == null) return;

        if (!_canAttack) return;

        player.TakeHealthDamage(_damage);
        if (_dealSanity) player.TakeSanityDamage(_damage);

        _canAttack = false;
    }

    private bool _canAttack;
    public bool CanAttack
    {
        get { return _canAttack; }
        set { _canAttack = value; }
    }

    [SerializeField] private float _minAttackDelay = 1.5f;
    public float MinAttackDelay
    {
        get { return _minAttackDelay; }
        set { _minAttackDelay = value; }
    }

    [SerializeField] private float _maxAttackDelay = 2.5f;
    public float MaxAttackDelay
    {
        get { return _maxAttackDelay; }
        set { _maxAttackDelay = value; }
    }

    [SerializeField] private float _attackDuration = 0.5f;
    public float AttackDuration
    {
        get { return _attackDuration; }
        set { _attackDuration = value; }
    }

    [SerializeField] private float _walkSpeed = 40f;
    public float WalkSpeed
    {
        get { return _walkSpeed; }
        set { _walkSpeed = value; }
    }

    [SerializeField] private int _damage = 2;
    public int Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }

    [SerializeField] private bool _dealSanity;
    public bool DealSanity
    {
        get { return _dealSanity; }
        set { _dealSanity = value; }
    }

    [SerializeField] private bool _emitEmbers;
    public bool EmitEmbers
    {
        get { return _emitEmbers; }
        set { _emitEmbers = value; }
    }

    [SerializeField] private float _emissionRate = 0.5f;
    public float EmissionRate
    {
        get { return _emissionRate; }
        set { _emissionRate = value; }
    }

    [SerializeField] private GameObject _ember;
    public GameObject Ember
    {
        get { return _ember; }
        set { _ember = value; }
    }
}

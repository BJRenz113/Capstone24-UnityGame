using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreacheryBossVoid : MonoBehaviour
{
    public void Start()
    {
        _currentRadius = _minRadius;
        _startTime = Time.time;
        _isDamaging = false;
        _isCoroutineExecuting = false;
    }

    public void FixedUpdate()
    {
        float elapsedTime = Time.time - _startTime;
        CircleCollider2D collider = gameObject.GetComponent<CircleCollider2D>();

        if (elapsedTime < _growthDuration)
        {
            float t = elapsedTime / _growthDuration;
            _currentRadius = Mathf.Lerp(_minRadius, _maxRadius, t);
            transform.localScale = new Vector3(_currentRadius, _currentRadius, 1f);
        }
        else
        {
            _currentRadius = _maxRadius;
            transform.localScale = new Vector3(_currentRadius, _currentRadius, 1f);
        }

        if (elapsedTime >= _growthDuration + _lifespan)
        {
            _isDamaging = false;
            GameObject.Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player == null) return;

        _isDamaging = true;
        StartCoroutine(DamageOverTime(player));
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player == null) return;

        _isDamaging = true;
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player == null) return;

        _isDamaging = false;
    }

    private IEnumerator DamageOverTime(Player player)
    {
        if (_isCoroutineExecuting) yield break;

        _isCoroutineExecuting = true;

        while(_isDamaging)
        {
            yield return new WaitForSeconds(_damageRate / 2);

            if (Time.time - _startTime >= _damageHesitation && _isDamaging)
            {
                player.TakeSanityDamage(_damage);
            }

            yield return new WaitForSeconds(_damageRate / 2);
        }

        _isCoroutineExecuting = false;
    }

    private float _startTime;
    private float _currentRadius;
    private bool _isDamaging;
    private bool _isCoroutineExecuting;

    [SerializeField] private float _minRadius = 0f;
    public float MinRadius
    {
        get { return _minRadius; }
        set { _minRadius = value; }
    }

    [SerializeField] private float _maxRadius = 1f;
    public float MaxRadius
    {
        get { return _maxRadius; }
        set { _maxRadius = value; }
    }

    [SerializeField] private float _growthDuration = 1f;
    public float GrowthDuration
    {
        get { return _growthDuration; }
        set { _growthDuration = value; }
    }

    [SerializeField] private float _lifespan = 1f;
    public float LifeSpan
    {
        get { return _lifespan; }
        set { _lifespan = value; }
    }

    [SerializeField] private float _damageHesitation = 1f;
    public float DamageHesitation
    {
        get { return _damageHesitation; }
        set { _damageHesitation = value; }
    }

    [SerializeField] private float _damageRate = 1f;
    public float DamageRate
    {
        get { return _damageRate; }
        set { _damageRate = value; }
    }

    [SerializeField] private int _damage = 1;
    public int Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }
}

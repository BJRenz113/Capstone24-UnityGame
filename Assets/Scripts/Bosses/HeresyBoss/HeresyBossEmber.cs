using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeresyBossEmber : MonoBehaviour
{
    public void Start()
    {
        _startTime = Time.time;
        _isDamaging = false;
        _isCoroutineExecuting = false;
    }

    public void FixedUpdate()
    {
        float elapsedTime = Time.time - _startTime;

        if (elapsedTime >= _lifespan)
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

        while (_isDamaging)
        {
            player.TakeHealthDamage(_damage);
            yield return new WaitForSeconds(_damageRate);
        }

        _isCoroutineExecuting = false;
    }

    private float _startTime;
    private bool _isDamaging;
    private bool _isCoroutineExecuting;

    [SerializeField] private float _lifespan = 2f;
    public float LifeSpan
    {
        get { return _lifespan; }
        set { _lifespan = value; }
    }

    [SerializeField] private float _damageRate = 0.25f;
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

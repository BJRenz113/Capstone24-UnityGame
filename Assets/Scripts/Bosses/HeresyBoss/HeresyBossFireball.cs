using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeresyBossFireball : MonoBehaviour
{
    public void Start()
    {
        _startTime = Time.time;
    }

    public void FixedUpdate()
    {
        float elapsedTime = Time.time - _startTime;
        if (elapsedTime >= _lifespan) GameObject.Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player == null) return;

        player.TakeHealthDamage(_damage);
    }

    private float _startTime;

    [SerializeField] private float _lifespan = 2f;
    public float LifeSpan
    {
        get { return _lifespan; }
        set { _lifespan = value; }
    }

    [SerializeField] private int _damage = 15;
    public int Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }
}

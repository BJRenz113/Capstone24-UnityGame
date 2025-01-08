using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SuicideBomberExplosion : MonoBehaviour
{
    public void Start()
    {
        StartCoroutine(MainState());
    }

    private IEnumerator MainState()
    {
        yield return new WaitForSeconds(_lifespan);
        GameObject.Destroy(gameObject);
    }

    public void FixedUpdate()
    {

    }

    public void OnTriggerStay2D(Collider2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (player != null) player.TakeHealthDamage(_damage);
        if (enemy != null) enemy.TakeDamage(_damage);
    }

    private int _damage;
    public int Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }

    [SerializeField] private float _lifespan = 0.4f;
    public float Lifespan
    {
        get { return _lifespan; }
        set { _lifespan = value; }
    }
}

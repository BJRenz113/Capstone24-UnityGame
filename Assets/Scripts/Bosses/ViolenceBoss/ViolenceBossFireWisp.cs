using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ViolenceBossFireWisp : MonoBehaviour
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
        if (player == null) return;

        player.TakeHealthDamage(_damage);
    }

    [SerializeField] private int _damage = 10;
    public int Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }

    [SerializeField] private float _lifespan = 3f;
    public float Lifespan
    {
        get { return _lifespan; }
        set { _lifespan = value; }
    }
}

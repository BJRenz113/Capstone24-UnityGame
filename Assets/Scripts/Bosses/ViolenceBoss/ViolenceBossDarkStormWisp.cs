using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class ViolenceBossDarkStormWisp : MonoBehaviour
{
    public void Start()
    {
        StartCoroutine(LifeCoroutine());
    }

    private IEnumerator LifeCoroutine()
    {
        yield return new WaitForSeconds(_lifespan);
        GameObject.Destroy(gameObject);
    }

    public void FixedUpdate()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        Vector3 playerPos = playerObject.transform.position;

        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        Vector2 directionToTarget = (playerPos - gameObject.transform.position).normalized;
        Vector2 currentDirection = rb.velocity.normalized;
        Vector2 newDirection = Vector2.Lerp(currentDirection, directionToTarget, _turnForce).normalized;
        rb.velocity = newDirection * rb.velocity.magnitude;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player == null) return;

        player.TakeSanityDamage(_damage);

        GameObject.Destroy(gameObject);
    }

    [SerializeField] private float _moveSpeed = 90f;
    public float MoveSpeed
    {
        get { return _moveSpeed; }
        set { _moveSpeed = value; }
    }

    [SerializeField] [Range(0f, 1f)] private float _turnForce = 0.1f;
    public float TurnForce
    {
        get { return _turnForce; }
        set { _turnForce = value; }
    }

    [SerializeField] private float _lifespan = 6f;
    public float Lifespan
    {
        get { return _lifespan; }
        set { _lifespan = value; }
    }

    [SerializeField] private int _damage = 1;
    public int Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViolenceBossScytheBoomerang : MonoBehaviour
{


    public void Start()
    {
        
    }

    public void FixedUpdate()
    {
        gameObject.transform.Rotate(0, 0, _rotateSpeed * Time.fixedDeltaTime);
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player == null) return;

        player.TakeHealthDamage(_damage);


    }

    [SerializeField] private int _damage = 20;
    public int Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }

    [SerializeField] private float _rotateSpeed = 180f;
    public float RotateSpeed
    {
        get { return _rotateSpeed; }
        set { _rotateSpeed = value; }
    }
}

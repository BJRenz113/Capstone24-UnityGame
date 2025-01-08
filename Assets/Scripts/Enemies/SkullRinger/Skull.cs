using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skull : MonoBehaviour
{
    public void Start()
    {
        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();

        int areas = player.CurrentAreasCleared;
        if (areas < 0) areas = 0;
        if (areas > 5) areas = 5;

        _damage = _damageList[areas];
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player == null) return;

        player.TakeHealthDamage(_damage);

        GameObject.Destroy(gameObject);
    }

    private int _damage;
    public int Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }

    [SerializeField] private List<int> _damageList = new List<int>() {10, 10, 10, 10, 10, 10 };
    public List<int> DamageList
    {
        get { return _damageList; }
        set { _damageList = value; }
    }
}

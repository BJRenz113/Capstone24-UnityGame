using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeWeapon : MonoBehaviour
{
    private List<Enemy> _enemiesHit;
    private Player _player;
    private int _damage;
    private float _damageMultiplier;
    private AudioSource audioSource;
    public AudioClip attackSound;


    public void Start()
    {
        _enemiesHit = new List<Enemy>();
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        _damage = _player.MeleeDamage;
        _damageMultiplier = _player.ConsecutiveMeleeDamageMultiplier;

        audioSource = GetComponent<AudioSource>();
            if (audioSource && attackSound != null)
            {
                audioSource.PlayOneShot(attackSound);
            }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null && !_enemiesHit.Contains(enemy))
        {
            float damageF = _damage * (1 + (_damageMultiplier - 1) * (_player.MaxAttacks - _player.CurrentAttacks + 1));
            enemy.TakeDamage((int)damageF);
            _enemiesHit.Add(enemy);

        }
    }
}

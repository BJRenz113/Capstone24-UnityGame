using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpinAttackWeapon : MonoBehaviour
{
    private List<Enemy> _enemiesHit;
    private Player _player;
    private int _damage;
    private int _hits;
    private float _time;
    private float _damageMultiplier;
    private AudioSource audioSource;
    public AudioClip spinAttackSound;

    public void Start()
    {
        _enemiesHit = new List<Enemy>();
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        _hits = _player.SpinAttackHits;
        _damage = _player.SpinAttackDamage;
        _time = _player.SpinAttackTime;
        audioSource = GetComponent<AudioSource>(); 

        if (spinAttackSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(spinAttackSound);
        }

        StartCoroutine(MultiHitClearEnemies());
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null && !_enemiesHit.Contains(enemy))
        {
            enemy.TakeDamage(_damage);
            _enemiesHit.Add(enemy);
        }
    }

    private IEnumerator MultiHitClearEnemies()
    {
        for (int i = 0; i < _hits; i++)
        {
            _enemiesHit.Clear();
            yield return new WaitForSeconds(_time / _hits);
        }
    }
}

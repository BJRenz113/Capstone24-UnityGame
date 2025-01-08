using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangeProjectile : MonoBehaviour
{
    private List<Enemy> _enemiesHit;
    private AudioSource audioSource;
    public AudioClip launchSound;

    public void Start()
    {
        _enemiesHit = new List<Enemy>();
        GameObject playerObject = GameObject.FindWithTag("Player");
        _damage = playerObject.GetComponent<Player>().RangedDamage;
        _lifespan = playerObject.GetComponent<Player>().RangedLifespan;


        StartCoroutine(ExecuteLifespan());
    }

    private IEnumerator ExecuteLifespan()
    {        
        audioSource = GetComponent<AudioSource>(); 

        if (launchSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(launchSound);
        }


        yield return new WaitForSeconds(_lifespan);
        GameObject.Destroy(gameObject);
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

    private int _damage;
    private float _lifespan;
}

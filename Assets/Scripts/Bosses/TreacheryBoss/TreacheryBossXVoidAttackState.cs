using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreacheryBossXVoidAttackState : BaseEnemyState
{
    private TreacheryBoss _treacheryBoss;
    private GameObject _voidObject;
    private bool _ready;
    private int _size;
    private float _rate;
    private float _cooldown;
    private float _spacing;

    // Define an AudioSource variable to play sound effects
    public AudioSource soundEffect;

    public override void EnterState(EnemyStateManager enemyStateManager)
    {
        _treacheryBoss = ((TreacheryBoss)enemyStateManager.GetEnemy());
        _treacheryBoss.gameObject.GetComponent<Animator>().SetInteger("AnimationIndex", 1);
        _voidObject = _treacheryBoss.SmallMiscVoidObject;
        _size = _treacheryBoss.XVoidSize;
        _rate = _treacheryBoss.XVoidRate;
        _spacing = _treacheryBoss.XVoidSpacing;
        _cooldown = _treacheryBoss.XVoidCooldown;

        _ready = false;
        _treacheryBoss.StartCoroutine(PerformAttack());

        // Play the sound effect when entering the state
        if (soundEffect != null)
        {
            soundEffect.Play();
        }
    }

    private IEnumerator PerformAttack()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");

        Vector3 ul = new Vector3(-1, 1, 0).normalized;
        Vector3 ur = new Vector3(1, 1, 0).normalized;
        Vector3 dl = new Vector3(-1, -1, 0).normalized;
        Vector3 dr = new Vector3(1, -1, 0).normalized;

        List<Vector3> dirs = new List<Vector3>() {ul, ur, dl, dr};

        for(int i = 0; i < _size ; i++)
        {
            for(int j = 0; j < 4; j++)
            {
                GameObject voidObject = GameObject.Instantiate(_voidObject);
                voidObject.transform.position = _treacheryBoss.gameObject.transform.position + dirs[j] * (i + 1) * _spacing;
            }

            yield return new WaitForSeconds(_rate);
        }

        yield return new WaitForSeconds(_cooldown);

        _ready = true;
    }

    public override void FixedUpdateState(EnemyStateManager enemyStateManager)
    {
        if (_ready) enemyStateManager.TransitionToState(new TreacheryBossIdleState());
    }

    public override void ExitState(EnemyStateManager enemyStateManager)
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class GreedyPickpocketDashState : BaseEnemyState
{
    private GreedyPickpocket _pickpocket;
    private bool _ready;
    private UnityEngine.AI.NavMeshAgent _agent;


    public override void EnterState(EnemyStateManager enemyStateManager)
    {
        _pickpocket = ((GreedyPickpocket)enemyStateManager.GetEnemy());
        _ready = false;
        _agent = _pickpocket.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        _pickpocket.StartCoroutine(PerformState());

        if (_pickpocket.dashSound != null)
        {
            AudioSource audioSource = _pickpocket.gameObject.GetComponent<AudioSource>();
            if (audioSource)
            {
                audioSource.PlayOneShot(_pickpocket.dashSound);
            }
        }



    }

    private IEnumerator PerformState()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");

        Animator animator = _pickpocket.gameObject.GetComponent<Animator>();
        animator.SetBool("IsRunning", true);
        _agent.SetDestination(playerObject.transform.position);

        yield return new WaitForSeconds(UnityEngine.Random.Range(_pickpocket.DashDurationMin, _pickpocket.DashDurationMax));

        animator.SetBool("IsRunning", false);
        _agent.ResetPath();

        yield return new WaitForSeconds(UnityEngine.Random.Range(_pickpocket.DashCooldownMin, _pickpocket.DashCooldownMax));
        _ready = true;
    }

    public override void FixedUpdateState(EnemyStateManager enemyStateManager)
    {
        Vector3 playerPosition = GameObject.FindWithTag("Player").transform.position;
        Vector3 enemyPosition = _pickpocket.gameObject.transform.position;

        float dist = Vector2.Distance(enemyPosition, playerPosition);

        if (_ready && dist < _pickpocket.StealRadius) enemyStateManager.TransitionToState(new GreedyPickpocketDashState());
    }

    public override void ExitState(EnemyStateManager enemyStateManager)
    {
        _agent.ResetPath();
    }
}

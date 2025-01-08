using UnityEngine;

public class SummonerChaseState : BaseEnemyState
{
    private float backingOutRadius = 1f; // Radius at which the enemy backs out
    private float chaseSpeed = 1f;
    private float backingOutSpeed = 0.5f;
    private bool _isRun = false;
    private bool _isAttack = false;

    private GameObject playerGameObject;
    private GameObject enemyGameObject;
    private Rigidbody2D rb;

    public override void EnterState(EnemyStateManager enemyStateManager)
    {
        Debug.Log("Entering Chase State");
        playerGameObject = GameObject.FindWithTag("Player");
        Enemy enemy = enemyStateManager.GetEnemy();
        enemyGameObject = enemy.gameObject;
        rb = enemyGameObject.GetComponent<Rigidbody2D>();
    }

    public override void FixedUpdateState(EnemyStateManager enemyStateManager)
    {
        ChasePlayer();
        if (_isRun)
        {
            Debug.Log("RUNNING AWAY FROM PLAYER");
        }
        if (_isAttack)
        {
            Debug.Log("Attacking Player (go to Attack State)");
        }
    }

    public override void ExitState(EnemyStateManager enemyStateManager)
    {
        // Reset the frame counter when exiting the chase state
    }



    private void ChasePlayer()
    {
        Vector2 direction = (playerGameObject.transform.position - enemyGameObject.transform.position).normalized;
        // Move the enemy towards the player using Rigidbody2D
        rb.velocity = direction * chaseSpeed;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("SummonerRunArea"))
        {
            _isRun = true;
        }

        if (other.gameObject.CompareTag("SummonerAttackArea"))
        {
            _isAttack = true;
        }
        else
        {
            _isAttack = false;
            _isRun = false;
        }
    }

    private void BackOutFromPlayer()
    {
        Vector2 direction = (enemyGameObject.transform.position - playerGameObject.transform.position).normalized;
        // Move the enemy away from the player using Rigidbody2D
        rb.velocity = direction * backingOutSpeed;
    }

    public void SetInSpawn(bool stuffYIPEEEE)
    {
        _isAttack = stuffYIPEEEE;
        Debug.Log("WORKS"); 
    }
}

 




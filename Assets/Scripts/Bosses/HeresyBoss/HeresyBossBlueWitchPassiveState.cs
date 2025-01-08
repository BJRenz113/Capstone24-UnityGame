using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeresyBossBlueWitchPassiveState : BaseEnemyState
{
    private HeresyBossBlueWitch _witch;
    private bool _ready;
    private Vector3 _targetPosition;

    public override void EnterState(EnemyStateManager enemyStateManager)
    {
        _witch = ((HeresyBossBlueWitch) enemyStateManager.GetEnemy());
        _witch.gameObject.GetComponent<Animator>().SetInteger("AnimationIndex", 1);
        _ready = false;

        GameObject playerObject = GameObject.FindWithTag("Player");
        GameObject whiteWitchObject = GameObject.Find("HeresyBossWhiteWitch");
        GameObject redWitchObject = GameObject.Find("HeresyBossRedWitch");

        List<Vector3> positions = new List<Vector3>();

        if (playerObject != null) positions.Add(playerObject.transform.position);
        if (whiteWitchObject != null) positions.Add(whiteWitchObject.transform.position);
        if (redWitchObject != null) positions.Add(redWitchObject.transform.position);

        System.Random rng = new System.Random();
        int positionIndex = rng.Next(positions.Count);

        _targetPosition = positions[positionIndex];

        _witch.StartCoroutine(WaitForStateDuration());
    }

    private IEnumerator WaitForStateDuration()
    {
        yield return new WaitForSeconds(_witch.PassiveDuration);
        _ready = true;
    }

    public override void FixedUpdateState(EnemyStateManager enemyStateManager)
    {
        Vector3 enemyPosition = _witch.gameObject.transform.position;

        Vector2 direction = (_targetPosition - enemyPosition).normalized;
        Rigidbody2D rb = _witch.gameObject.GetComponent<Rigidbody2D>();
        
        rb.velocity = direction * _witch.WalkSpeed * Time.fixedDeltaTime;

        if(_ready)
        {
            rb.velocity = Vector3.zero;
            enemyStateManager.TransitionToState(new HeresyBossSpikePatternAttackState());
        }
    }

    public override void ExitState(EnemyStateManager enemyStateManager)
    {

    }
}

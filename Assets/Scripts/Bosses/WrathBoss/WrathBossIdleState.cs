using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrathBossIdleState : BaseEnemyState
{
    private WrathBoss _boss;
    private bool _ready;

    public override void EnterState(EnemyStateManager enemyStateManager)
    {
        _boss = ((WrathBoss)enemyStateManager.GetEnemy());
        _ready = false;
        _boss.StartCoroutine(MoveToDest());
    }

    private IEnumerator MoveToDest()
    {
        Vector3 randomDest = GetRandomPatrolDestination();

        float dist = Vector2.Distance(randomDest, _boss.gameObject.transform.position);
        Vector2 direction = (randomDest - _boss.gameObject.transform.position).normalized;

        _boss.gameObject.GetComponent<Rigidbody2D>().velocity = direction * dist / _boss.GetToLocationInTime;
        yield return new WaitForSeconds(_boss.GetToLocationInTime);

        _boss.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        yield return new WaitForSeconds(_boss.WaitAtDestinationTime);

        _ready = true;
    }

    private Vector3 GetRandomPatrolDestination()
    {
        float randomDist = UnityEngine.Random.Range(_boss.MinWalkDistance, _boss.MaxWalkDistance);
        float randomAngle = UnityEngine.Random.Range(0f, 360f);

        float xOffset = Mathf.Cos(randomAngle * Mathf.PI / 180) * randomDist;
        float yOffset = Mathf.Sin(randomAngle * Mathf.PI / 180) * randomDist;

        return new Vector3(_boss.HomePosition.x + xOffset, _boss.HomePosition.y + yOffset, 0);
    }

    public override void FixedUpdateState(EnemyStateManager enemyStateManager)
    {
        for(int i = 0; i < _boss.ActiveRings.Count; i++)
        {
            _boss.ActiveRings[i].transform.position = _boss.gameObject.transform.position + _boss.RingsOffset;
        }

        if(_ready) enemyStateManager.TransitionToState(new WrathBossIdleState());
    }

    public override void ExitState(EnemyStateManager enemyStateManager)
    {

    }
}
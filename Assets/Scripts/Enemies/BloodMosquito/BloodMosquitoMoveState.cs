using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodMosquitoMoveState : BaseEnemyState
{
    private BloodMosquito _mosquito;
    private bool _waitForNewTarget;
    private bool _ready;
    private Vector3 _target;

    public override void EnterState(EnemyStateManager enemyStateManager)
    {
        _mosquito = ((BloodMosquito)enemyStateManager.GetEnemy());
        _waitForNewTarget = false;
        _ready = false;
        _target = CalculateTargetPosition();
    }

    public override void FixedUpdateState(EnemyStateManager enemyStateManager)
    {
        if (_mosquito.HasTouched && _ready) enemyStateManager.TransitionToState(new BloodMosquitoBitingState());

        Vector3 enemyPosition = _mosquito.gameObject.transform.position;

        if (Vector2.Distance(_target, enemyPosition) < 0.05f)
        {
            _mosquito.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            _waitForNewTarget = true;
            _ready = true;
            _mosquito.StartCoroutine(GiveNewTarget());
        }
        else if(!_waitForNewTarget)
        {
            Vector2 direction = (_target - enemyPosition).normalized;
            int angle = (int) Vector2.SignedAngle(Vector2.right, direction);

            while(angle < 0)
            {
                angle += 360;
            }

            int animationIndex = ((angle + 45) / 90) % 4;
            _mosquito.gameObject.GetComponent<Animator>().SetInteger("AnimationIndex", animationIndex);

            _mosquito.gameObject.GetComponent<Rigidbody2D>().velocity = direction * _mosquito.MoveSpeed * Time.fixedDeltaTime;
        }
    }

    private IEnumerator GiveNewTarget()
    {
        _target = CalculateTargetPosition();
        yield return new WaitForSeconds(UnityEngine.Random.Range(_mosquito.MoveWaitMin, _mosquito.MoveWaitMax));
        _waitForNewTarget = false;
    }

    private Vector3 CalculateTargetPosition()
    {
        float randomRadius = UnityEngine.Random.Range(0f, _mosquito.HomeRadius);
        float randomAngle = UnityEngine.Random.Range(0f, 360f);
        float xOffset = Mathf.Cos(randomAngle * Mathf.PI / 180) * randomRadius;
        float yOffset = Mathf.Sin(randomAngle * Mathf.PI / 180) * randomRadius;

        return new Vector3(_mosquito.HomePosition.x + xOffset, _mosquito.HomePosition.y + yOffset, 0f);
    }

    public override void ExitState(EnemyStateManager enemyStateManager)
    {

    }
}

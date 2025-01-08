using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TreacheryBossIdleState : BaseEnemyState
{
    private TreacheryBoss _treacheryBoss;
    private float _startTime;
    private float _transitionCooldown;
    private bool _ready;

    public override void EnterState(EnemyStateManager enemyStateManager)
    {
        _treacheryBoss = ((TreacheryBoss) enemyStateManager.GetEnemy());
        _treacheryBoss.gameObject.GetComponent<Animator>().SetInteger("AnimationIndex", 0);
        _transitionCooldown = _treacheryBoss.TransitionCooldown;
        _ready = false;
        _startTime = Time.time;

        _treacheryBoss.StartCoroutine(WaitToAttack());
    }

    private IEnumerator WaitToAttack()
    {
        yield return new WaitForSeconds(_transitionCooldown);
        _ready = true;
    }

    public override void FixedUpdateState(EnemyStateManager enemyStateManager)
    {
        if (!_ready) return;

        Dictionary<int, int> potentialAttacks = _treacheryBoss.Attacks[_treacheryBoss.GetCloseness()];

        List<int> attackPool = new List<int>();

        foreach(KeyValuePair<int, int> attack in potentialAttacks)
        {
            for(int i = 0; i < attack.Value; i++)
            {
                attackPool.Add(attack.Key);
            }
        }

        System.Random rng = new System.Random();
        int attackIndex = rng.Next(attackPool.Count);

        switch(attackPool[attackIndex])
        {
            case 10:
                enemyStateManager.TransitionToState(new TreacheryBossSelfVoidAttackState());
                break;
            case 11:
                enemyStateManager.TransitionToState(new TreacheryBossPlayerVoidAttackState());
                break;
            case 12:
                enemyStateManager.TransitionToState(new TreacheryBossXVoidAttackState());
                break;
            case 13:
                enemyStateManager.TransitionToState(new TreacheryBossScatterVoidAttackState());
                break;
            case 14:
                enemyStateManager.TransitionToState(new TreacheryBossSpiralVoidAttackState());
                break;
            case 20:
                enemyStateManager.TransitionToState(new TreacheryBossTorchShuffleState());
                break;
            /*case 21:
                enemyStateManager.TransitionToState(new TreacheryBossTorchAttackState());
                break;*/
            case 22:
                enemyStateManager.TransitionToState(new TreacheryBossTorchExtinguishState());
                break;
            default:
                break;
        }
    }

    public override void ExitState(EnemyStateManager enemyStateManager)
    {

    }
}

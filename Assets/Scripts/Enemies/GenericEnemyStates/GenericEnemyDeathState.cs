using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEnemyDeathState : BaseEnemyState
{
    public override void EnterState(EnemyStateManager enemyStateManager)
    {
        Enemy enemy = enemyStateManager.GetEnemy();
        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();

        int moneyOnKill = UnityEngine.Random.Range(enemy.MinMoneyOnKill, enemy.MaxMoneyOnKill);

        if (player != null) player.AddMoney(moneyOnKill);

        Object.Destroy(enemyStateManager.GetEnemy().gameObject);
    }

    public override void FixedUpdateState(EnemyStateManager enemyStateManager)
    {

    }

    public override void ExitState(EnemyStateManager enemyStateManager)
    {

    }
}

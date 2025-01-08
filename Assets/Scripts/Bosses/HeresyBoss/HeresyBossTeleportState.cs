using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeresyBossTeleportState : BaseEnemyState
{
    private HeresyBossWhiteWitch _witch;
    private bool _ready;

    public override void EnterState(EnemyStateManager enemyStateManager)
    {
        _witch = ((HeresyBossWhiteWitch)enemyStateManager.GetEnemy());
        _witch.gameObject.GetComponent<Animator>().SetInteger("AnimationIndex", 3);
        _ready = false;
        _witch.StartCoroutine(ExecuteTeleport());
    }

    private IEnumerator ExecuteTeleport()
    {
        int ig = _witch.TeleportInvisibleGranularity;

        for(int i = 0; i < ig; i++)
        {
            SpriteRenderer renderer = _witch.gameObject.GetComponent<SpriteRenderer>();
            Color color = renderer.color;
            color.a = (4 - i) * (1f / ig);
            renderer.color = color;

            yield return new WaitForSeconds(_witch.TeleportTransitionDuration / ig);
        }

        float angle = UnityEngine.Random.Range(0f, 360f);
        float dist = _witch.TeleportRadius;

        float xOffset = Mathf.Cos(angle * Mathf.PI / 180) * dist;
        float yOffset = Mathf.Sin(angle * Mathf.PI / 180) * dist;

        _witch.transform.position = new Vector3(_witch.HomePosition.x + xOffset, _witch.HomePosition.y + yOffset, _witch.HomePosition.z);

        yield return new WaitForSeconds(_witch.TeleportInvisibleDuration);

        for (int i = 0; i < ig; i++)
        {
            SpriteRenderer renderer = _witch.gameObject.GetComponent<SpriteRenderer>();
            Color color = renderer.color;
            color.a = (i + 1) * (1f / ig);
            renderer.color = color;

            yield return new WaitForSeconds(_witch.TeleportTransitionDuration / ig);
        }

        _ready = true;
    }

    public override void FixedUpdateState(EnemyStateManager enemyStateManager)
    {
        if (_ready) enemyStateManager.TransitionToState(new HeresyBossWhiteWitchPassiveState());
    }

    public override void ExitState(EnemyStateManager enemyStateManager)
    {

    }
}

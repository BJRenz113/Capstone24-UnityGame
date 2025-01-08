using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePlayerState
{
    public abstract void EnterState(PlayerStateManager playerStateManager);
    public abstract void FixedUpdateState(PlayerStateManager playerStateManager);
    public abstract void ExitState(PlayerStateManager playerStateManager);
    public virtual void OnTriggerEnter2D(PlayerStateManager playerStateManager, Collider2D other)
    {

    }
    public virtual void OnTriggerStay2D(PlayerStateManager playerStateManager, Collider2D other)
    {

    }
}

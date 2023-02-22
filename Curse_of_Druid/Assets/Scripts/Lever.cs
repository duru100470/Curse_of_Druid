using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Lever
{
    public void Interact()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            pc.rigid2d.AddForce(new Vector2(wallDir * -15f, pc.WallJumpForce), ForceMode2D.Impulse);
            pc.StopCoroutine(pc.DelayWallJumpInput());
            pc.stateMachine.SetState(new PlayerJump(pc));
            return;
        }
    }

    public abstract bool OnUse(Entity user);
}

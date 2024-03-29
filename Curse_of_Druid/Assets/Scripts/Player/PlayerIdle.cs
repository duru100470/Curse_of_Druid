using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : IState
{
    private PlayerController pc;

    public PlayerIdle(PlayerController playerController)
    {
        this.pc = playerController;
    }

    public void OperateEnter()
    {
        pc.IsWallJumpEnable = true;
    }

    public void OperateExit()
    {
    }
    public void OperateUpdate()
    {
        pc.Step();

        // Transition
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            pc.stateMachine.SetState(new PlayerRun(pc));
        }
        else
        {
            pc.rigid2d.velocity = new Vector2(0, pc.rigid2d.velocity.y);
        }

        if (Input.GetAxisRaw("Vertical") == -1 && Input.GetKeyDown(KeyCode.Space))
        {
            pc.stateMachine.SetState(new PlayerDownJump(pc));
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            pc.stateMachine.SetState(new PlayerJump(pc));
        }

        if (pc.IsThereLand() == false)
        {
            pc.stateMachine.SetState(new PlayerJump(pc));
        }
    }
    public void OperateFixedUpdate()
    {

    }
}

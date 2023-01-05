using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : IState
{
    private PlayerController playerController;

    public PlayerIdle(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    public void OperateEnter()
    {
    }

    public void OperateExit()
    {
    }
    public void OperateUpdate()
    {
        // Transition
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            playerController.stateMachine.SetState(new PlayerRun(playerController));
        }
        else
        {
            playerController.rigid2d.velocity = new Vector2(0, playerController.rigid2d.velocity.y);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerController.stateMachine.SetState(new PlayerJump(playerController));
        }

        if (playerController.IsThereLand() == false)
        {
            playerController.stateMachine.SetState(new PlayerJump(playerController));
        }
    }
    public void OperateFixedUpdate()
    {

    }
}

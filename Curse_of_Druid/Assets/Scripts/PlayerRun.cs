using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRun : IState
{
    private PlayerController playerController;

    public PlayerRun(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    public void OperateEnter()
    {
        playerController.anim.SetBool("isRunning", true);
        playerController.anim.speed = 0.8f;
    }

    public void OperateExit()
    {
        playerController.anim.SetBool("isRunning", false);
        playerController.anim.speed = 0.3f;
    }

    public void OperateUpdate()
    {
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
        float h = Input.GetAxisRaw("Horizontal");

        playerController.HorizontalMove(h);

        // Transition
        if (h == 0)
        {
            playerController.stateMachine.SetState(new PlayerIdle(playerController));
        }
    }
}
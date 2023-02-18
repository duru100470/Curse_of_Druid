using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimbing : IState
{
    private PlayerController playerController;

    public PlayerClimbing(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    public void OperateEnter()
    {
        playerController.anim.SetBool("isClimbing", true);
        playerController.anim.speed = 0.5f;
        playerController.rigid2d.velocity = Vector2.zero;
    }
    public void OperateExit()
    {
        playerController.anim.speed = 0.3f;
        playerController.anim.SetBool("isClimbing", false);
        playerController.IsClimbingLeft = false;
        playerController.IsClimbingRight = false;
    }
    public void OperateUpdate()
    {
        // 화살표 + 점프키 동시 감지?
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            playerController.rigid2d.AddForce(Vector2.left * playerController.JumpPower, ForceMode2D.Impulse);
            playerController.stateMachine.SetState(new PlayerJump(playerController));
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            playerController.rigid2d.AddForce(Vector2.right * playerController.JumpPower, ForceMode2D.Impulse);
            playerController.stateMachine.SetState(new PlayerJump(playerController));
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            playerController.rigid2d.velocity = new Vector2(0, playerController.ClimbingSpeed);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            playerController.rigid2d.velocity = new Vector2(0, -playerController.ClimbingSpeed);
        }
        else playerController.rigid2d.velocity = Vector2.zero;
    }
    public void OperateFixedUpdate()
    {
        
    }
}
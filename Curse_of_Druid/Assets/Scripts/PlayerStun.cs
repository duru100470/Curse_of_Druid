using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStun : IState
{
    private PlayerController playerController;

    public PlayerStun(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    public void OperateEnter()
    {
        playerController.anim.SetBool("isStunned", true);
        playerController.anim.speed = 1.8f;
    }
    public void OperateExit()
    {
        playerController.anim.SetBool("isStunned", false);
        playerController.anim.speed = 0.3f;
    }
    public void OperateUpdate()
    {
    }
    public void OperateFixedUpdate()
    {
        playerController.rigid2d.velocity = new Vector2(playerController.rigid2d.velocity.x * 0.9f, playerController.rigid2d.velocity.y);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : IState
{
    private PlayerController playerController;

    public PlayerDead(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    public void OperateEnter()
    {
        playerController.anim.SetBool("isDead", true);
        playerController.anim.speed = 1f;
    }
    public void OperateExit()
    {
        playerController.anim.SetBool("isDead", false);
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
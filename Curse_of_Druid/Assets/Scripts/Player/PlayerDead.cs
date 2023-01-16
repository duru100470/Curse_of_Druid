using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : IState
{
    private PlayerController pc;

    public PlayerDead(PlayerController playerController)
    {
        this.pc = playerController;
    }

    public void OperateEnter()
    {
        pc.anim.SetBool("isDead", true);
        pc.anim.speed = 1f;
    }
    public void OperateExit()
    {
        pc.anim.SetBool("isDead", false);
        pc.anim.speed = 0.3f;
    }
    public void OperateUpdate()
    {
    }
    public void OperateFixedUpdate()
    {
        pc.rigid2d.velocity = new Vector2(pc.rigid2d.velocity.x * 0.9f, pc.rigid2d.velocity.y);
    }
}
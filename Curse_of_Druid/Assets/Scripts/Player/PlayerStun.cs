using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStun : IState
{
    private PlayerController pc;

    public PlayerStun(PlayerController playerController)
    {
        this.pc = playerController;
    }

    public void OperateEnter()
    {
        pc.anim.SetBool("isStunned", true);
        pc.anim.speed = 1.8f;
    }
    public void OperateExit()
    {
        pc.anim.SetBool("isStunned", false);
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
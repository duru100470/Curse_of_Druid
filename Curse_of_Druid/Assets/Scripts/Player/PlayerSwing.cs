using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwing : IState
{
    private PlayerController pc;

    public PlayerSwing(PlayerController playerController)
    {
        this.pc = playerController;
    }

    public void OperateEnter()
    {
        pc.anim.SetBool("isSwing", true);
    }
    public void OperateExit()
    {
        pc.anim.SetBool("isSwing", false);
    }
    public void OperateUpdate()
    {
    }
    public void OperateFixedUpdate()
    {
        pc.rigid2d.velocity = new Vector2(pc.rigid2d.velocity.x * 0.9f, pc.rigid2d.velocity.y);
    }
}
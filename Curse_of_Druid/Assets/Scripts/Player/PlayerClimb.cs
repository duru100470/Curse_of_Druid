using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimb : IState
{
    private PlayerController pc;
    private float maxFallingSpeed = 0;

    public PlayerClimb(PlayerController playerController)
    {
        this.pc = playerController;
    }

    public void OperateEnter()
    {
        pc.IsWallJumpEnable = false;
        pc.rigid2d.gravityScale = 0;
        pc.anim.SetBool("isClimbing", true);
        pc.anim.speed = 0.5f;
    }
    public void OperateExit()
    {
        pc.StartCoroutine(pc.DelayWallJump());
        pc.rigid2d.gravityScale = 1;
        pc.anim.SetBool("isClimbing", false);
        pc.anim.speed = 0.3f;
    }
    public void OperateUpdate()
    {
        var wallDir = pc.IsThereWall();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            pc.rigid2d.AddForce(new Vector2(wallDir * -15f, pc.WallJumpForce), ForceMode2D.Impulse);
            pc.stateMachine.SetState(new PlayerJump(pc));
            return;
        }

        if (wallDir == 0 ||
            Input.GetAxisRaw("Horizontal") == 0)
        {
            pc.stateMachine.SetState(new PlayerJump(pc));
            return;
        }
    }
    public void OperateFixedUpdate()
    {
        pc.rigid2d.velocity = new Vector2(0f, Mathf.Min(pc.rigid2d.velocity.y * 0.94f, 0.5f));

        // fixme

        // if (maxFallingSpeed > pc.rigid2d.velocity.y)
        //     maxFallingSpeed = pc.rigid2d.velocity.y;

        // if (pc.rigid2d.velocity.y <= 0)
        // {
        //     if (pc.IsThereLand())
        //     {
        //         // Check Falling Damage
        //         if (maxFallingSpeed == -20)
        //             pc.player.GetDamage(99, DAMAGE_TYPE.Falling);
        //         else if (maxFallingSpeed < -17)
        //             pc.player.GetDamage(20, DAMAGE_TYPE.Falling);
        //         else if (maxFallingSpeed < -15)
        //             pc.player.GetDamage(10, DAMAGE_TYPE.Falling);
        //     }
        // }
    }
}
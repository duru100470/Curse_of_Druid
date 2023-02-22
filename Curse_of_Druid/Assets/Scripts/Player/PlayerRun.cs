using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRun : IState
{
    private PlayerController pc;

    public PlayerRun(PlayerController playerController)
    {
        this.pc = playerController;
    }

    public void OperateEnter()
    {
        SoundManager.Inst.PlayEffectSound(SOUND_NAME.PlayerRun);
        pc.anim.SetBool("isRunning", true);
        pc.anim.speed = 0.8f;
        pc.IsWallJumpEnable = true;
    }

    public void OperateExit()
    {
        SoundManager.Inst.StopEffectSound(SOUND_NAME.PlayerRun);
        pc.anim.SetBool("isRunning", false);
        pc.anim.speed = 0.3f;
    }

    public void OperateUpdate()
    {
        pc.Step();

        if (Input.GetKeyDown(KeyCode.Space))
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
        float h = Input.GetAxisRaw("Horizontal");

        pc.HorizontalMove(h);

        // Transition
        if (h == 0)
        {
            pc.stateMachine.SetState(new PlayerIdle(pc));
        }
    }
}
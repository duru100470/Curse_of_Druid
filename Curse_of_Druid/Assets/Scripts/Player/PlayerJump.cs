using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : IState
{
    private PlayerController pc;
    private float maxFallingSpeed = 0;

    public PlayerJump(PlayerController playerController)
    {
        this.pc = playerController;
    }

    public void OperateEnter()
    {
        SoundManager.Inst.PlayEffectSound(SOUND_NAME.PlayerJump);
        pc.anim.SetBool("isJumping", true);
        pc.anim.speed = 0.7f;

        if ((pc.IsThereLand() == false || pc.JumpCount == 0) && pc.HasSteppedEntity == false)
        {
            // Debug.Log(pc.JumpCount);
            return;
        }

        pc.rigid2d.velocity = new Vector2(pc.rigid2d.velocity.x, 0);
        pc.IsCoyoteTimeEnable = false;
        pc.StartCoroutine(ControlJump());

        if (pc.HasSteppedEntity)
        {
            pc.rigid2d.AddForce(Vector2.up * pc.JumpPower * 0.2f, ForceMode2D.Impulse);
        }

        pc.IsJumping = true;
        pc.JumpCount--;
        pc.HasSteppedEntity = false;
    }
    public void OperateExit()
    {
        pc.IsJumping = false;
        pc.JumpCount = pc.JumpMaxCount;
        pc.IsCoyoteTimeEnable = true;

        pc.anim.SetBool("isJumping", false);
        pc.anim.speed = 0.3f;
    }
    public void OperateUpdate()
    {
        if (Input.GetAxisRaw("Horizontal") == 0) {
            pc.rigid2d.velocity = new Vector2(0, pc.rigid2d.velocity.y);
        }
        else if (Input.GetAxisRaw("Horizontal") == 1 && pc.IsThereWall(1)) {
            pc.stateMachine.SetState(new PlayerClimb(pc));
            pc.IsClimbingRight = true;
        }
        else if (Input.GetAxisRaw("Horizontal") == -1 && pc.IsThereWall(-1)) {
            pc.stateMachine.SetState(new PlayerClimb(pc));
            pc.IsClimbingLeft = true;
        }
        pc.anim.SetFloat("ySpeed", pc.rigid2d.velocity.y);
        if (Input.GetAxisRaw("Horizontal") == 0)
            pc.rigid2d.velocity = new Vector2(0, pc.rigid2d.velocity.y);

        pc.anim.SetFloat("ySpeed", pc.rigid2d.velocity.y);
    }
    public void OperateFixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");

        if (pc.rigid2d.velocity.y <= 0)
            pc.Step();

        pc.HorizontalMove(h);
        if (maxFallingSpeed > pc.rigid2d.velocity.y)
            maxFallingSpeed = pc.rigid2d.velocity.y;

        // Transition
        var wallDir = pc.IsThereWall();
        if (wallDir != 0 && pc.IsWallJumpEnable)
        {
            if (wallDir == -1 && Input.GetAxisRaw("Horizontal") == -1)
            {
                pc.stateMachine.SetState(new PlayerClimb(pc));
                return;
            }
            else if (wallDir == 1 && Input.GetAxisRaw("Horizontal") == 1)
            {
                pc.stateMachine.SetState(new PlayerClimb(pc));
                return;
            }
        }

        if (pc.rigid2d.velocity.y <= 0)
        {
            if (pc.IsThereLand())
            {
                // Check Falling Damage
                if (maxFallingSpeed == -20)
                    pc.player.GetDamage(99, DAMAGE_TYPE.Falling);
                else if (maxFallingSpeed < -17)
                    pc.player.GetDamage(20, DAMAGE_TYPE.Falling);
                else if (maxFallingSpeed < -15)
                    pc.player.GetDamage(10, DAMAGE_TYPE.Falling);
                // Change State
                else if (h == 0)
                    pc.stateMachine.SetState(new PlayerIdle(pc));
                else
                    pc.stateMachine.SetState(new PlayerRun(pc));
            }
        }
    }

    private IEnumerator ControlJump()
    {
        for (pc.JumpTime = 0; pc.JumpTime <= pc.JumpMaxTime; pc.JumpTime += 0.05f)
        {
            pc.rigid2d.AddForce(Vector2.up * pc.JumpPower * (pc.JumpMaxTime - pc.JumpTime), ForceMode2D.Impulse);

            if (Input.GetKey(KeyCode.Space))
                yield return new WaitForSeconds(0.05f);
            else
                break;
        }
        yield return null;
    }
}
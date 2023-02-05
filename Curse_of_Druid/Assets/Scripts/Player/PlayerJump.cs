using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : IState
{
    private PlayerController playerController;
    private float maxFallingSpeed = 0;

    public PlayerJump(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    public void OperateEnter()
    {
        playerController.anim.SetBool("isJumping", true);
        playerController.anim.speed = 0.7f;

        if (playerController.IsThereLand() == false || playerController.JumpCount == 0)
        {
            return;
        }

        playerController.rigid2d.velocity = new Vector2(playerController.rigid2d.velocity.x, 0);
        playerController.IsCoyoteTimeEnable = false;
        playerController.StartCoroutine(ControlJump());

        playerController.IsJumping = true;
        playerController.JumpCount--;
    }
    public void OperateExit()
    {
        playerController.IsJumping = false;
        playerController.JumpCount = playerController.JumpMaxCount;
        playerController.IsCoyoteTimeEnable = true;

        playerController.anim.SetBool("isJumping", false);
        playerController.anim.speed = 0.3f;
    }
    public void OperateUpdate()
    {
        if (Input.GetAxisRaw("Horizontal") == 0) {
            playerController.rigid2d.velocity = new Vector2(0, playerController.rigid2d.velocity.y);
        }
        else if (Input.GetAxisRaw("Horizontal") == 1 && playerController.IsThereWall(1)) {
            playerController.stateMachine.SetState(new PlayerClimbing(playerController));
            playerController.IsClimbingRight = true;
        }
        else if (Input.GetAxisRaw("Horizontal") == -1 && playerController.IsThereWall(-1)) {
            playerController.stateMachine.SetState(new PlayerClimbing(playerController));
            playerController.IsClimbingLeft = true;
        }
        playerController.anim.SetFloat("ySpeed", playerController.rigid2d.velocity.y);
    }
    public void OperateFixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");

        playerController.HorizontalMove(h);
        if (maxFallingSpeed > playerController.rigid2d.velocity.y)
            maxFallingSpeed = playerController.rigid2d.velocity.y;

        // Transition
        if (playerController.rigid2d.velocity.y <= 0)
        {
            if (playerController.IsThereLand())
            {
                // Check Falling Damage
                if (maxFallingSpeed == -20)
                    playerController.player.GetDamage(99, DAMAGE_TYPE.Falling);
                else if (maxFallingSpeed < -17)
                    playerController.player.GetDamage(20, DAMAGE_TYPE.Falling);
                else if (maxFallingSpeed < -15)
                    playerController.player.GetDamage(10, DAMAGE_TYPE.Falling);
                // Change State
                else if (h == 0)
                    playerController.stateMachine.SetState(new PlayerIdle(playerController));
                else
                    playerController.stateMachine.SetState(new PlayerRun(playerController));
            }
        }
    }

    private IEnumerator ControlJump()
    {
        for (playerController.JumpTime = 0; playerController.JumpTime <= playerController.JumpMaxTime; playerController.JumpTime += 0.05f)
        {
            playerController.rigid2d.AddForce(Vector2.up * playerController.JumpPower * (playerController.JumpMaxTime - playerController.JumpTime), ForceMode2D.Impulse);

            if (Input.GetKey(KeyCode.Space))
                yield return new WaitForSeconds(0.05f);
            else
                break;
        }
        yield return null;
    }
}
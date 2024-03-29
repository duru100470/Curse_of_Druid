using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity, IDamageable
{
    private Item curItem;
    public Item CurItem => curItem;
    private PlayerController playerController;
    public PlayerController PlayerController => playerController;
    [SerializeField]
    private Transform attackLocation;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private Animator attackAnimator;

    protected override void Awake()
    {
        base.Awake();
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        coor = Coordinate.WorldPointToCoordinate(transform.position);

        SelectSlot();
    }

    private void SelectSlot()
    {
        // Select Inventory slots
        if (Input.GetKeyDown(KeyCode.A)) {
            UIManager.Inst.Inventory.GetItemInfo(0)?.OnUse(this);
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            UIManager.Inst.Inventory.GetItemInfo(1)?.OnUse(this);
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackLocation.position, attackRange);
    }

    // 이 메소드 호출하면 앞에 있는 적 공격함
    public override void AttackEntity(int damageAmount, DAMAGE_TYPE dmgType)
    {
        StartCoroutine(Swing(1f));
        Collider2D[] damage = Physics2D.OverlapCircleAll(attackLocation.position, attackRange);

        for (int i = 0; i < damage.Length; i++)
        {
            damage[i].GetComponent<IDamageable>()?.GetDamage(damageAmount, dmgType);
        }
    }

    public void PlayAttackAnim(string swingType)
    {
        attackAnimator.speed = 0.3f;

        switch (swingType)
        {
            case "Pickaxe":
                attackAnimator.Play("PlayerAttackPickaxe");
                break;
            case "Machete":
                attackAnimator.Play("PlayerAttackMachete");
                break;
        }
    }

    public void GetDamage(int amount, DAMAGE_TYPE dmgType)
    {
        SoundManager.Inst.PlayEffectSound(SOUND_NAME.PlayerHurt);
        health = Mathf.Max(0, health - amount);
        EventManager.Inst.PostNotification(EVENT_TYPE.PlayerHPChange, this, health);
        if (health == 0)
        {
            EventManager.Inst.PostNotification(EVENT_TYPE.GameOver, this, dmgType);
            Dead();
            return;
        }
        StartCoroutine(Stun(1f));
    }

    public void Dead()
    {
        playerController.stateMachine.SetState(new PlayerDead(playerController));
        playerController.stateMachine.isStateLocked = true;
    }

    private IEnumerator Stun(float duration)
    {
        StopCoroutine(Swing(1f));
        playerController.stateMachine.SetState(new PlayerStun(playerController));
        yield return new WaitForSeconds(duration);
        playerController.stateMachine.SetState(new PlayerIdle(playerController));
    }

    private IEnumerator Swing(float duration)
    {
        playerController.stateMachine.SetState(new PlayerSwing(playerController));
        yield return new WaitForSeconds(duration);
        playerController.stateMachine.SetState(new PlayerIdle(playerController));
        attackAnimator.Play("PlayerAttackNone");
    }
}

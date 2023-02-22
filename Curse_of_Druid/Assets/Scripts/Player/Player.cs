using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity, IDamageable
{
    private Item curItem;
    public Item CurItem => curItem;
    private PlayerController playerController;
    public PlayerController PlayerController => playerController;

    protected override void Awake()
    {
        base.Awake();
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        coor = Coordinate.WorldPointToCoordinate(transform.position);

        SelectSlot();

        if (Input.GetMouseButtonDown(0))
        {
            curItem?.OnUse(this);
        }
    }

    private void SelectSlot()
    {
        // Select Inventory slots
        if (Input.GetKeyDown(KeyCode.Alpha1))
            UIManager.Inst.Inventory.SelectedSlot = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            UIManager.Inst.Inventory.SelectedSlot = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3))
            UIManager.Inst.Inventory.SelectedSlot = 2;
        if (Input.GetKeyDown(KeyCode.Alpha4))
            UIManager.Inst.Inventory.SelectedSlot = 3;

        curItem = UIManager.Inst.Inventory.GetItemInfo(UIManager.Inst.Inventory.SelectedSlot);
    }

    public void GetDamage(int amount, DAMAGE_TYPE dmgType)
    {
        health = Mathf.Max(0, health - amount);
        Debug.Log("Y");
        EventManager.Inst.PostNotification(EVENT_TYPE.PlayerHPChange, this, health);
        Debug.Log("X");
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
        playerController.stateMachine.SetState(new PlayerStun(playerController));
        yield return new WaitForSeconds(duration);
        playerController.stateMachine.SetState(new PlayerIdle(playerController));
    }
}

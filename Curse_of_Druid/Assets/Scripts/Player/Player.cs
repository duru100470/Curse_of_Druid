using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity, IDamageable
{
    private Item curItem;
    public Item CurItem => curItem;
    private PlayerController playerController;


    public Coordinate Coor { get; set; }
    protected override void Awake()
    {
        base.Awake();
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        coor = Coordinate.WorldPointToCoordinate(transform.position);
    }

    public void GetDamage(int amount, DAMAGE_TYPE dmgType)
    {
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
    }

    private IEnumerator Stun(float duration)
    {
        playerController.stateMachine.SetState(new PlayerStun(playerController));
        yield return new WaitForSeconds(duration);
        playerController.stateMachine.SetState(new PlayerIdle(playerController));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugEnemy : Enemy, IDamageable, IStep
{
    public StateMachine stateMachine;

    protected override void Awake()
    {
        stateMachine = new StateMachine(new DebugEnemyIdle(this));    
    }

    public void OnStep(Entity entity, bool _bool)
    {
        GetDamage(1, DAMAGE_TYPE.Step);
    }

    public void GetDamage(int amount, DAMAGE_TYPE damageType)
    {
        health = Mathf.Max(0, health - amount);
        Debug.Log(health);
        if (health == 0)
        {
            Dead();
            return;
        }
    }

    public void Dead()
    {
        Destroy(this.gameObject);
    }
}

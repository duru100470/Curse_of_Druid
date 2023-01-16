using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugEnemy : Enemy, IDamageable
{
    public StateMachine stateMachine;

    protected override void Awake()
    {
        stateMachine = new StateMachine(new DebugEnemyIdle(this));    
    }

    public void GetDamage(int amount, DAMAGE_TYPE damageType)
    {
    }

    public void Dead()
    {
    }
}

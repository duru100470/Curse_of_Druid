using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugEnemyIdle : IState
{
    private DebugEnemy debugEnemy;

    public DebugEnemyIdle(DebugEnemy debugEnemy)
    {
        this.debugEnemy = debugEnemy;
    }

    public void OperateEnter()
    {
    }

    public void OperateExit()
    {
    }
    public void OperateUpdate()
    {
    }
    public void OperateFixedUpdate()
    {
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeMove : IState
{
    private Bee bee;

    public BeeMove(Bee bee)
    {
        this.bee = bee;
    }

    public void OperateEnter()
    {
        if (bee.IsHeadingRight) bee.rigid2d.velocity = new Vector2(bee.Speed, 0);
        else bee.rigid2d.velocity = new Vector2(-bee.Speed, 0);
        bee.stateMachine.SetState(new BeeIdle(bee));
    }

    public void OperateExit()
    {
        bee.IsHeadingRight = !bee.IsHeadingRight;
    }
    public void OperateUpdate()
    {

    }
    public void OperateFixedUpdate()
    {

    }


}

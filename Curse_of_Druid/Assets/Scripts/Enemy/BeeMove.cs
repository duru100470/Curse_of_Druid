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
        if (bee.IsHeadingRight) bee.rigid2d.velocity = new Vector2(bee.Speed, 0f);
        else bee.rigid2d.velocity = new Vector2(-bee.Speed, 0f);
        bee.StartCoroutine(Wait());
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
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.0f);
        bee.stateMachine.SetState(new BeeIdle(bee));
    }
}

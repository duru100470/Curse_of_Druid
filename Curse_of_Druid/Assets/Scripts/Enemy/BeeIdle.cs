using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeIdle : IState
{
    private Bee bee;

    public BeeIdle(Bee bee)
    {
        this.bee = bee;
    }

    public void OperateEnter()
    {
        bee.IsHeadingRight = !(bee.IsHeadingRight);
        bee.rigid2d.velocity = Vector2.zero;
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
        bee.stateMachine.SetState(new BeeMove(bee));
    }
}

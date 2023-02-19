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
        bee.StartCoroutine(Wait());
    }

    public void OperateExit()
    {
        bee.stateMachine.SetState(new BeeMove(bee));
        bee.IsHeadingRight = !bee.IsHeadingRight;
    }
    public void OperateUpdate()
    {
    }
    public void OperateFixedUpdate()
    {

    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.0f);
    }
}

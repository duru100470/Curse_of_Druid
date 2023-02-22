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
        
    }

    public void OperateExit()
    {
        bee.IsHeadingRight = !bee.IsHeadingRight;
    }
    public void OperateUpdate()
    {
        bee.StartCoroutine(Wait());
        bee.stateMachine.SetState(new BeeMove(bee));
    }
    public void OperateFixedUpdate()
    {

    }
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.0f);
    }
}

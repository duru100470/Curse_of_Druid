using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockIdle : IState
{
    private Rock rock;

    public RockIdle(Rock rock)
    {
        this.rock = rock;
    }

    public void OperateEnter()
    {
        rock.StartCoroutine(Wait());
    }

    public void OperateExit()
    {
        rock.stateMachine.SetState(new RockRoll(rock));
        rock.IsHeadingRight = !rock.IsHeadingRight;
    }
    public void OperateUpdate()
    {
    }
    public void OperateFixedUpdate()
    {
        
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3.0f);
    }

}

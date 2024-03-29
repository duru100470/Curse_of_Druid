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
        rock.IsHeadingRight = !(rock.IsHeadingRight);
        rock.rigid2d.velocity = Vector2.zero;
        rock.StartCoroutine(Wait());
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
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3.0f);
        rock.stateMachine.SetState(new RockRoll(rock));
    }

}

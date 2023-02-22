using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfIdle : IState
{
    private Wolf wolf;

    public WolfIdle(Wolf wolf)
    {
        this.wolf = wolf;
    }

    public void OperateEnter()
    {
        wolf.IsHeadingRight = !(wolf.IsHeadingRight);
        wolf.rigid2d.velocity = Vector2.zero;
        wolf.StartCoroutine(Wait());
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
        // rock.stateMachine.SetState(new RockRoll(rock));
    }

}

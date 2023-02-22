using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockRoll : IState
{
    private Rock rock;

    public RockRoll(Rock rock)
    {
        this.rock = rock;
    }

    public void OperateEnter()
    {
        if (rock.IsHeadingRight) rock.rigid2d.velocity = new Vector2(rock.Speed, 0);
        else rock.rigid2d.velocity = new Vector2(-rock.Speed, 0);
    }

    public void OperateExit()
    {

    }
    public void OperateUpdate()
    {
        if (rock.WallCollide() == 1) {
            Debug.Log("wall");
            rock.stateMachine.SetState(new RockIdle(rock));
        }
        else if (rock.WallCollide() == -1) {
            Debug.Log("wall");
            rock.stateMachine.SetState(new RockIdle(rock));
        }
    }
    public void OperateFixedUpdate()
    {

    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockRoll : IState
{
    private Rock rock;

    private float timer = 0f;

    public RockRoll(Rock rock)
    {
        this.rock = rock;
    }

    public void OperateEnter()
    {
        if (rock.IsHeadingRight) rock.rigid2d.velocity = new Vector2(rock.Speed, 0);
        else rock.rigid2d.velocity = new Vector2(-rock.Speed, 0);
        if (rock.WallCollide() == 1) {
            rock.stateMachine.SetState(new RockIdle(rock));
        }
        else if (rock.WallCollide() == -1) {
            rock.stateMachine.SetState(new RockIdle(rock));
        }
    }

    public void OperateExit()
    {
        rock.IsHeadingRight = !rock.IsHeadingRight;
    }
    public void OperateUpdate()
    {

    }
    public void OperateFixedUpdate()
    {

    }
}


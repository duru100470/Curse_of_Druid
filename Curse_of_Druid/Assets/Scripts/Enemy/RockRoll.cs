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
        if (rock.IsHeadingRight) rock.rigid2d.velocity = new Vector2(rock.Speed, 0f);
        else rock.rigid2d.velocity = new Vector2(-rock.Speed, 0f);
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
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.0f);
                if (rock.WallCollide() == 1) {
            Debug.Log("wall");
            rock.stateMachine.SetState(new RockIdle(rock));
        }
        else if (rock.WallCollide() == -1) {
            Debug.Log("wall");
            rock.stateMachine.SetState(new RockIdle(rock));
        }
    }
}


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
        Debug.Log("Rock Rolling");
        if (rock.IsHeadingRight) {
            Debug.Log("Heading Right");
            // rock.rigid2d.velocity = new Vector2(rock.Speed, 0f);
        }
        else {
            Debug.Log("Heading Left");
            // rock.rigid2d.velocity = new Vector2(-rock.Speed, 0f);
        }
        // rock.StartCoroutine(Wait());
    }

    public void OperateExit()
    {

    }
    public void OperateUpdate()
    {
        int isCollided = rock.WallCollide();
        // Debug.Log(isCollided);
        if (isCollided == 1 && rock.IsHeadingRight) {
            Debug.Log("Right wall");
            rock.stateMachine.SetState(new RockIdle(rock));
        }
        else if (isCollided == -1 && !rock.IsHeadingRight) {
            Debug.Log("Left wall");
            rock.stateMachine.SetState(new RockIdle(rock));
        }
        else if (rock.IsHeadingRight) {
            rock.transform.Translate(Vector3.right * 0.05f);
        }
        else if (!rock.IsHeadingRight) {
            rock.transform.Translate(Vector3.left * 0.05f);
        }
    }
    public void OperateFixedUpdate()
    {

    }
    private IEnumerator Wait()
    {
        int isCollided = rock.WallCollide();
        Debug.Log(isCollided);
        yield return new WaitWhile(() => (isCollided == 0));
        if (isCollided == 1) {
            Debug.Log("wall");
            rock.stateMachine.SetState(new RockIdle(rock));
        }
        else if (isCollided == -1) {
            Debug.Log("wall");
            rock.stateMachine.SetState(new RockIdle(rock));
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeMove : IState
{
    private Bee bee;

    public BeeMove(Bee bee)
    {
        this.bee = bee;
    }

    public void OperateEnter()
    {
        if (bee.IsHeadingRight) bee.rigid2d.velocity = new Vector2(bee.Speed, 0);
        else bee.rigid2d.velocity = new Vector2(-bee.Speed, 0);
        bee.StartCoroutine(Move());
    }

    public void OperateExit()
    {
        bee.IsHeadingRight = !bee.IsHeadingRight;
    }
    public void OperateUpdate()
    {
        
    }
    public void OperateFixedUpdate()
    {
        
    }
    private IEnumerator Move()
    {
        var runTime = 0.0f;
        var duration = 3.0f;

        while (runTime < duration)
        {
            runTime += Time.deltaTime;

            bee.transform.position = Vector2.Lerp(bee.transform.position, bee.transform.position + new Vector2(0.1f, 0), runTime / duration);
            
            yield return null;
        }
    }

}

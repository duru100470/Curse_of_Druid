using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackedPlatform : RuleTile, IStep
{
    private bool isSuccess;

    public void OnStep(Entity entity, bool _bool)
    {
        if (entity is Player)
        {
            Invoke("DestroyPlatform", 1.5f);
        }
    }

    void DestroyPlatform()
    {
        Destroy();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornTrap : Obstacle, IStep
{
    private bool lastBool;

    public void OnStep(Entity entity, bool _bool)
    {
        if (entity is IDamageable && lastBool)
        {
            (entity as IDamageable ).Dead();
        }

        lastBool = _bool;
    }
}

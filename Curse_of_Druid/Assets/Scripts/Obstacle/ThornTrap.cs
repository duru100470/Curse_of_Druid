using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornTrap : Obstacle, IStep
{
    private bool lastBool;

    private void Start()
    {
        supportingTileCoorList.Add(new Coordinate(0, -1));
    }

    public void OnStep(Entity entity, bool _bool)
    {
        if (entity is IDamageable && lastBool)
        {
            (entity as IDamageable).Dead();
        }

        lastBool = _bool;
    }
}

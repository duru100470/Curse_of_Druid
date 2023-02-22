using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornTrap : Obstacle, IStep
{
    private void Start()
    {
        supportingTileCoorList.Add(new Coordinate(0, -1));
    }

    public void OnStep(Entity entity, bool _bool)
    {
        if (entity is IDamageable)
        {
            (entity as IDamageable).GetDamage(9999, DAMAGE_TYPE.Melee);
        }
    }
}

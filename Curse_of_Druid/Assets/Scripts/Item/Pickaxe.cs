using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickaxe : Item
{
    public override bool OnUse(Entity user)
    {
        if (user is Player)
        {
            Player player = user as Player;
            int x = player.Coor.X;
            int y = player.Coor.Y;
            Coordinate coor_target = new Coordinate(x, y);
            Tile target;
            TileManager.Inst.TileDict.TryGetValue(coor_target, out target);
            Durability--;
            if (Durability == 0)
            {
                // Destroy(gameObject);
            }
            if (target is DestroyedPlatform)
            {
                target.Destroy();
                Debug.Log("Item is used");
                return true;
            }
            else return false;
        }
        else return false;
    }
}
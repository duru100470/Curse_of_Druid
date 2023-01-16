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
            Coordinate target = new Coordinate(x+1, y);
            if (TileManager.Inst.TileArray[x + 1, y] is DestroyedPlatform)
            {
                bool isDestroyed = TileManager.Inst.DestroyTile(target);
                Debug.Log("Item is used");
                Durability--;
                if (Durability == 0)
                {
                    // Destroy(gameObject);
                }
                return true;
            }
            else return false;
        }
        else return false;
    }
}
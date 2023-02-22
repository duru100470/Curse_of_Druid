using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Machete : Item
{
    public override bool OnUse(Entity user)
    {
        if (user is Player)
        {
            Player player = user as Player;
            bool IsRight = player.PlayerController.IsHeadingRight;
            Coordinate coor_target;
            if (IsRight) coor_target = player.Coor + new Coordinate(1, 0);
            else coor_target = player.Coor + new Coordinate(-1, 0);
            Tile target;
            TileManager.Inst.TileDict.TryGetValue(coor_target, out target);
            Debug.Log($"{target}");
            Durability--;
            if (Durability == 0)
            {
                // Destroy(gameObject);
            }
            if (target is GrassTile)
            {
                target.Destroy();
                Debug.Log(UIManager.Inst.Inventory.UseItem(this));
                Debug.Log("Item is used");
                return true;
            }
            else if (target is VineTile)
            {
                target.Destroy();
                Debug.Log(UIManager.Inst.Inventory.UseItem(this));
                Debug.Log("Item is used");
                return true;
            }
            else return false;
        }
        else return false;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : Item
{
    public override bool OnUse(Entity user)
    {
        // Todo : 30초 시간제한
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
            if (target is IFlammable)
            {
                IFlammable flammable_target = target as IFlammable;
                flammable_target.Burn(3);
                // 불 붙은 장애물에 닿았을 때 1데미지 구현은 IDamageable
                Debug.Log("Item is used");
                return true;
            }
            else return false;
        }
        else return false;
    }
}
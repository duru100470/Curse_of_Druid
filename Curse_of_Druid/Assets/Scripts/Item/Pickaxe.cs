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
            bool IsRight = player.PlayerController.IsHeadingRight;
            Coordinate coor_target;
            if (IsRight) coor_target = player.Coor + new Coordinate(1, 0);
            else coor_target = player.Coor + new Coordinate(-1, 0);
            Tile target;
            TileManager.Inst.TileDict.TryGetValue(coor_target, out target);
            Debug.Log($"{target}");
            Durability--;
            player.AttackEntity(3, DAMAGE_TYPE.Pickaxe);
            Debug.Log(UIManager.Inst.Inventory.UseItem(this));
            if (Durability == 0)
            {
                // Destroy(gameObject);
            }
            if (target is DestroyedPlatform)
            {
                // SoundManager.Inst.PlayEffectSound(SOUND_NAME.Swing);
                // SoundManager.Inst.PlayEffectSound(SOUND_NAME.Pickaxe);
                // SoundManager.Inst.PlayEffectSound(SOUND_NAME.PickaxeHitRock);
                target.Destroy();
                return true;
            }
            else
            {
                // SoundManager.Inst.PlayEffectSound(SOUND_NAME.Swing);
                return false;
            }
        }
        else return false;
    }
}
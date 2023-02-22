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
            if (Durability == 0)
            {
                // Destroy(gameObject);
            }
            if (target is GrassTile)
            {
                SoundManager.Inst.PlayEffectSound(SOUND_NAME.Swing);
                SoundManager.Inst.PlayEffectSound(SOUND_NAME.Cut);
                Durability--;
                target.Destroy();
                Debug.Log(UIManager.Inst.Inventory.UseItem(this));
                Debug.Log("Item is used");
                return true;
            }
            else if (target is VineTile)
            {
                SoundManager.Inst.PlayEffectSound(SOUND_NAME.Swing);
                SoundManager.Inst.PlayEffectSound(SOUND_NAME.Cut);
                Durability--;
                target.Destroy();
                Debug.Log(UIManager.Inst.Inventory.UseItem(this));
                Debug.Log("Item is used");
                return true;
            }
            else
            {
                SoundManager.Inst.PlayEffectSound(SOUND_NAME.Swing);
                return false;
            }
        }
        else return false;
    }
}
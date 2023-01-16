using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Item
{
    public ItemInfo ItemInfo { get; set; }
    public Sprite itemSprite { get; set; }
    private int durability;

    public int Durability
    {
        get { return durability; }
        set { Mathf.Clamp(value, 0, ItemInfo.maxDurability); }
    }
    
    /// <summary>
    /// 아이템 사용 메소드 (성공여부 반환)
    /// </summary>
    public abstract bool OnUse(Entity user);
}

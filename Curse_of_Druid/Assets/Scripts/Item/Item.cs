using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Item
{
    public ItemInfo ItemInfo { get; set; }
    public virtual Sprite ItemSprite { get; set; }
    private int durability;

    public int Durability
    {
        get { return durability; }
        set { durability = Mathf.Clamp(value, 0, ItemInfo.maxDurability); }
    }

    /// <summary>
    /// 아이템 사용 메소드 (파괴여부 반환)
    /// </summary>
    public abstract bool OnUse(Entity user);
}

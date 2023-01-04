using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item
{
    private string name;
    private ITEM_ID itemID;
    private Sprite itemSprite;
    private int durability;

    public string Name => name;
    public ITEM_ID ItemID => itemID;
    public int Durability => durability;
    public Sprite ItemSprite => itemSprite;

    public abstract bool OnUse();
}

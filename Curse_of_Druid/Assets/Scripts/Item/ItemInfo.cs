using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ItemInfo", menuName = "Item/Item Info")]
public class ItemInfo : ScriptableObject
{
    public string itemName;
    public ITEM_ID itemID;
    public int maxDurability;
    public Sprite[] itemSprite;
}

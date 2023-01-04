using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ItemInfo", menuName = "Item/Item Info")]
public class ItemInfo : ScriptableObject
{
    public readonly string itemName;
    public readonly ITEM_ID itemID;
    public readonly int maxDurability;
    public readonly Sprite[] itemSprite;
}

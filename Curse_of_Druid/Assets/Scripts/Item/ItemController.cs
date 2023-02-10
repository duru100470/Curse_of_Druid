using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : Entity
{
    [SerializeField]
    private Item curItem;
    private SpriteRenderer spriteRenderer;

    protected override void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = curItem.ItemInfo.itemSprite[0];
        curItem.ItemSprite = curItem.ItemInfo.itemSprite[0];
    }

    public Item CurItem
    {
        get { return curItem; }
        set
        {
            curItem = value;
            spriteRenderer.sprite = value.ItemSprite;
        }
    }
}

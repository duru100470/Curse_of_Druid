using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField]
    private Item curItem;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = curItem.ItemInfo.itemSprite[0];
        curItem.itemSprite = curItem.ItemInfo.itemSprite[0];
    }

    public Item CurItem
    {
        get { return curItem; }
        set
        {
            curItem = value;
            spriteRenderer.sprite = value.itemSprite;
        }
    }
}

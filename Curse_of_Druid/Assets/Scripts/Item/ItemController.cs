using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : Entity, IInteractive
{
    [SerializeField]
    private Item curItem;
    private SpriteRenderer spriteRenderer;

    public bool IsAvailable { get; set; } = true;

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

    public void Interact(Entity entity)
    {
        if (entity == null) return;

        if (entity is Player)
        {
            UIManager.Inst.Inventory.AcquireItem(ref curItem);
        }
        Destroy(this.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : Entity, IInteractive
{
    [SerializeField]
    private Item curItem;
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private ItemInfo itemInfo;

    public bool IsAvailable { get; set; } = true;

    protected override void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        curItem = new Pickaxe();
        curItem.ItemInfo = itemInfo;
        curItem.Durability = itemInfo.maxDurability;
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
        Debug.Log("Interacted");
        if (entity == null) return;

        if (entity is Player)
        {
            Debug.Log(UIManager.Inst.Inventory.AcquireItem(ref curItem));
        }
        Destroy(this.gameObject);
    }
}

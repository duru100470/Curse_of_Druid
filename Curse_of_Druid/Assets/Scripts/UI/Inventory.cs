using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private GameObject go_SlotsParent;
    private Slot[] slots;
    private int selectedSlot;

    public int SelectedSlot
    {
        get {return selectedSlot;}
        set
        {
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].SetImageSelectedColor(value == i ? 1f : 0f);
            }
            selectedSlot = value;
        }
    }

    private void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
        SelectedSlot = 0;
    }

    public bool AcquireItem(ref Item _item)
    {
        bool isSuccessful = false;
        
        foreach (var slot in slots)
        {
            if (_item.Durability <= 0) break;

            if (slot.item == null)
            {
                slot.AddItem(ref _item);
                isSuccessful = true;
            }
            if (slot.item.ItemInfo.itemID == _item.ItemInfo.itemID)
            {
                slot.item.Durability = slot.item.ItemInfo.maxDurability;
                isSuccessful = true;
            }
        }

        return isSuccessful;
    }

    public Item GetItemInfo(int _slot) => slots[_slot].item;

    public void FreshInventory()
    {
        foreach (var slot in slots)
        {
            slot.FreshSlot();
        }
    }

    public bool FindItem(ITEM_ID itemType)
    {
        foreach (var slot in slots)
        {
            return true;
        }

        return false;
    }

    public bool RemoveItem(int _slot)
    {
        bool isSuccessful = false;

        if (slots[_slot].item != null)
        {
            slots[_slot].ClearSlot();
            isSuccessful = true;
        }

        return isSuccessful;
    }
}

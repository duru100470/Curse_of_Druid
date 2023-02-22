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

    private void Awake()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
        SelectedSlot = 0;
    }

    public bool AcquireItem(ref Item _item)
    {
        bool isSuccessful = false;
    
        if (_item.ItemInfo.itemID == ITEM_ID.Pickaxe)
        {
            if (slots[0].item == null)
            {
                slots[0].AddItem(ref _item);
                slots[0].item.Durability = slots[0].item.ItemInfo.maxDurability;
                isSuccessful = true;
                return isSuccessful;
            }
            else 
            {
                isSuccessful = false;
                return isSuccessful;
            }
        }
        else if(_item.ItemInfo.itemID == ITEM_ID.Machete)
        {
            if (slots[1].item == null)
            {
                slots[1].AddItem(ref _item);
                slots[1].item.Durability = slots[1].item.ItemInfo.maxDurability;
                isSuccessful = true;
                return isSuccessful;
            }
            else 
            {
                isSuccessful = false;
                return isSuccessful;
            }
        }
        else if(_item.ItemInfo.itemID == ITEM_ID.Torch)
        {
            if (slots[2].item == null)
            {
                slots[2].AddItem(ref _item);
                slots[2].item.Durability = slots[2].item.ItemInfo.maxDurability;
                isSuccessful = true;
                return isSuccessful;
            }
            else 
            {
                isSuccessful = false;
                return isSuccessful;
            }
        }

        return isSuccessful;
    }

    public Item GetItemInfo(int _slot) {
        if (slots[_slot].item != null) return slots[_slot].item;
        else return null;
    }

    public void FreshInventory()
    {
        foreach (var slot in slots)
        {
            slot.FreshSlot();
        }
    }

    public bool UseItem(Item _item)
    {
        // Debug.Log(_item);
        bool isSuccessful = false;
        if (_item.ItemInfo.itemID == ITEM_ID.Pickaxe)
        {
            if (slots[0].item == null)
            {
                return isSuccessful;
            }
            else 
            {
                if (slots[0].item.Durability == 0) RemoveItem(0);
                FreshInventory();
                isSuccessful = true;
                return isSuccessful;
            }
        }
        else if(_item.ItemInfo.itemID == ITEM_ID.Machete)
        {
            if (slots[1].item == null)
            {
                return isSuccessful;
            }
            else
            {
                if (slots[1].item.Durability == 0) RemoveItem(1);
                FreshInventory();
                isSuccessful = true;
                return isSuccessful;
            }
        }
        return isSuccessful;
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

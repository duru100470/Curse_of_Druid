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
        
        slots[0].AddItem(ref _item);
        foreach (var slot in slots)
        {
            if (_item.Durability <= 0) break;

            if (slot.item == null)
            {
                slot.AddItem(ref _item);
                isSuccessful = true;
                break;
            }
            if (slot.item.ItemInfo.itemID == _item.ItemInfo.itemID)
            {
                slot.item.Durability = slot.item.ItemInfo.maxDurability;
                isSuccessful = true;
                break;
            }
        }

        /*UI 기획대로 칸을 고정하기 위해서 필요할 것 같은 코드.
        아이템 줍기 테스트가 아직 안 되기 때문에 코드 테스트를 아직 못함.
        if(_item.ItemInfo.itemID == Pickaxe)
        {
            if(slots[0].item == null)
            {
                slot.AddItem(ref _item);
                isSuccessful = true;
                return isSuccessful;
            }
            if(slots[0].item != null)
            {
                slots[0].item.Durability = slots[0].item.ItemInfo.maxDurability;
                isSuccessful = true;
                return isSuccessful;
            }
        }
        else if(_item.ItemInfo.itemID == Machete)
        {
            if(slots[1].item == null)
            {
                slot.AddItem(ref _item);
                isSuccessful = true;
                return isSuccessful;
            }
            if(slots[1].item != null)
            {
                slots[1].item.Durability = slots[1].item.ItemInfo.maxDurability;
                isSuccessful = true;
                return isSuccessful;
            }
        }
        else if(_item.ItemInfo.itemID == Torch)
        {
            if(slots[2].item == null)
            {
                slot.AddItem(ref _item);
                isSuccessful = true;
                return isSuccessful;
            }
            if(slots[2].item != null)
            {
                slots[2].item.Durability = slots[2].item.ItemInfo.maxDurability;
                isSuccessful = true;
                return isSuccessful;
            }
        }*/

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

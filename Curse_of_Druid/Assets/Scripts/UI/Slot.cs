using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Image imageSelected;
    [SerializeField] private TextMeshProUGUI textCount;

    public Item item { set; get; }

    private void Awake()
    {
        ClearSlot();
        imageSelected.color = new Color(1f, 1f, 1f, 0f);
    }

    private void SetColor(float _alpha)
    {
        Color color = image.color;
        color.a = _alpha;
        image.color = color;
    }

    private void SetCount(int _count)
    {
        textCount.text = _count.ToString();
        if (_count <= 1)
            textCount.alpha = 0f;
        else
            textCount.alpha = 1f;
    }

    public void SetImageSelectedColor(float _alpha)
    {
        imageSelected.color = new Color(1f, 1f, 1f, _alpha);
    }

    public void AddItem(ref Item _item)
    {
        if (item == null)
        {
            item = _item.Copy();
        }
        // item.ItemType == _item.ItemType
        else
        {
            item.Durability = item.ItemInfo.maxDurability;
        }

        FreshSlot();
    }

    public void FreshSlot()
    {
        image.sprite = item?.ItemSprite;
        SetColor((item != null) ? 1f : 0f);
        SetCount(item?.Durability ?? 0);
    }

    public void ClearSlot()
    {
        item = null;
        FreshSlot();
    }
}

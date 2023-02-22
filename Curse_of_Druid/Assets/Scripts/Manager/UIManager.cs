using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : SingletonBehavior<UIManager>, IListener
{
    [SerializeField]
    private Inventory inventory;

    public Inventory Inventory => inventory;

    private void Start()
    {

    }

    public void OnEvent(EVENT_TYPE eType, Component sender, object param = null)
    {
    }
}

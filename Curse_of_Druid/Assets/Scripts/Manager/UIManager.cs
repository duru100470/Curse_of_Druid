using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : SingletonBehavior<UIManager>, IListener
{
    [SerializeField]
    private Inventory inventory;
    [SerializeField]
    private Text lifeText;
    private int lifeInit;

    public Inventory Inventory => inventory;

    private void Start()
    {
        lifeInit = 100;
        SetPlayerLife(lifeInit);
    }

    public void SetPlayerLife(int life)
    {
        lifeText.text = life.ToString();
    }

    public void OnEvent(EVENT_TYPE eType, Component sender, object param = null)
    {
    }
}

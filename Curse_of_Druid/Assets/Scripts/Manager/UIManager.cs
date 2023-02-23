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
    private TextMeshProUGUI playerHealth;
    [SerializeField]
    private TextMeshProUGUI score;

    public Inventory Inventory => inventory;

    private void Start()
    {
        EventManager.Inst.AddListener(EVENT_TYPE.PlayerHPChange, this);
    }

    public void OnEvent(EVENT_TYPE eType, Component sender, object param = null)
    {
        switch (eType)
        {
            case EVENT_TYPE.PlayerHPChange:
                playerHealth.text = param.ToString();
                break;
        }
    }

    private void Update()
    {
        score.text = Timer.Inst.CurTime.ToString();
    }
}

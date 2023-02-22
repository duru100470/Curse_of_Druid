using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleIronBar : Tile, IListener
{
    [SerializeField]
    private int leverId = 0;
    [SerializeField]
    private bool isOpened = false;

    private void Awake()
    {
        EventManager.Inst.AddListener(EVENT_TYPE.LeverPulled, this);
    }
    
    public void OnEvent(EVENT_TYPE eType, Component sender, object param)
    {
        switch (eType)
        {
            case EVENT_TYPE.LeverPulled:
                if (leverId != (int)param || isOpened) break;

                isOpened = true;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                break;
        }
    }
}
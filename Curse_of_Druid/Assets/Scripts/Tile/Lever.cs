using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : Tile
{
    [SerializeField]
    private int leverId = 0;
    [SerializeField]
    private bool isPulled = false;
    
    public void PullLever()
    {
        if (isPulled) return;

        EventManager.Inst.PostNotification(EVENT_TYPE.LeverPulled, this, leverId);

        // 애니메이션 추후에 적용
        isPulled = true;
    }
}
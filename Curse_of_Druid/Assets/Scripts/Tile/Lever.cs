using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : Tile, IInteractive
{
    [SerializeField]
    private int leverId = 0;
    [SerializeField]
    private bool isPulled = false;
    private Animator animator;

    public bool IsAvailable { get; set; } = true;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Interact(Entity entity)
    {
        if (entity is not Player) return;

        PullLever();
    }

    public void PullLever()
    {
        if (isPulled) return;

        EventManager.Inst.PostNotification(EVENT_TYPE.LeverPulled, this, leverId);

        SoundManager.Inst.PlayEffectSound(SOUND_NAME.LeverSwitch);
        
        Debug.Log("lever is pulled!!");

        animator.SetBool("isPull", true);

        // 애니메이션 추후에 적용
        isPulled = true;
        IsAvailable = false;
    }
}
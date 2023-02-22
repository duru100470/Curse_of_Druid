using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : Tile, IInteractive
{
    [SerializeField]
    private bool isExited = false;

    [SerializeField]
    private string nextScene = "TitleScene";

    public bool IsAvailable { get; set; } = true;

    public void Interact(Entity entity)
    {
        if (entity is not Player) return;
        SoundManager.Inst.PlayEffectSound(SOUND_NAME.GateChoir);
        Invoke("ChangeScene", 1.0f);
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(nextScene);
    }
}
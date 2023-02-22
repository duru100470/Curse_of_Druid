using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackedPlatform : RuleTile, IStep
{
    [SerializeField]
    private Sprite[] crackAnimation;
    [SerializeField]
    private SpriteRenderer crackSpriteRenderer;
    private bool isCracked = false;

    public void OnStep(Entity entity, bool _bool)
    {
        if (isCracked) return;

        if (entity is Player)
        {
            isCracked = true;  
            Debug.Log("asdasd");
            StartCoroutine(DestroyPlatform());
        }
    }

    private IEnumerator DestroyPlatform()
    {
        crackSpriteRenderer.sprite = crackAnimation[0];
        yield return new WaitForSeconds(1f);
        crackSpriteRenderer.sprite = crackAnimation[1];
        yield return new WaitForSeconds(1f);
        crackSpriteRenderer.sprite = crackAnimation[2];
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}

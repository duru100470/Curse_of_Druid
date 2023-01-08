using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackedPlatform : RuleTile
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Invoke("DestroyPlatform", 1.5f);
        }
    }

    void DestroyPlatform()
    {
        this.gameObject.SetActive(false);
    }
}

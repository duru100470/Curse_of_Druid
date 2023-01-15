using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassingPlatform : RuleTile
{
    private bool isPlayerThere;
    PlatformEffector2D platformObject;

    void Start()
    {
        isPlayerThere = true;
        platformObject = GetComponent<PlatformEffector2D>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && Input.GetButton("Vertical") && isPlayerThere)
        {
            platformObject.rotationalOffset = 180f;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            platformObject.rotationalOffset = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isPlayerThere = true;
    }

    /*private void OnCollisionExit2D(Collision2D collision)
    {
        isPlayerThere = false;
    }*/
}
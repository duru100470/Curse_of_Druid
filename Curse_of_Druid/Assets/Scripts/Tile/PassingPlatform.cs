using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassingPlatform : RuleTile
{
    bool playerCheck;
    PlatformEffector2D platformObject;

    void Start()
    {
        playerCheck = true;
        platformObject = GetComponent<PlatformEffector2D>();
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Space) && Input.GetButton("Vertical") && playerCheck)
        {
            Debug.Log("down jump");
            platformObject.rotationalOffset = 180f;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            platformObject.rotationalOffset = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        playerCheck = true;
    }

    /*private void OnCollisionExit2D(Collision2D collision)
    {
        playerCheck = false;
    }*/
}
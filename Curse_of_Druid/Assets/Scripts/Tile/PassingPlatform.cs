using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassingPlatform : RuleTile
{
    PlatformEffector2D platformObject;

    void Start()
    {
        platformObject = GetComponent<PlatformEffector2D>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && Input.GetButton("Vertical"))
        {
            platformObject.rotationalOffset = 180f;
            Invoke("Restore", 0.3f);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            platformObject.rotationalOffset = 0f;
        }
    }

    void Restore()
    {
        platformObject.rotationalOffset = 0f;
    }
}
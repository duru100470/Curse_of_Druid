using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : SingletonBehavior<Timer>
{
    [SerializeField]
    private float curTime = 0f;
    [SerializeField]
    private bool checkTime = false;

    public float CurTime => (float)Math.Round(curTime, 3);

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if (!checkTime) return;

        curTime += Time.deltaTime;
    }

    public void ResetTimer()
    {
        checkTime = false;
        curTime = 0f;
    }

    public void StartTimer()
    {
        checkTime = true;
    }

    public void PauseTimer()
    {
        checkTime = false;
    }
}

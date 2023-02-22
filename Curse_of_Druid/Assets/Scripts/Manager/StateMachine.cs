using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public bool isStateLocked = false;
    public IState CurruentState { get; private set; }

    public StateMachine(IState defaultState)
    {
        CurruentState = defaultState;
        CurruentState.OperateEnter();
    }

    public void SetState(IState state)
    {
        if (isStateLocked) return;

        if (CurruentState == state)
        {
            return;
        }

        // Debug.Log($"{CurruentState} => {state}");

        CurruentState.OperateExit();

        CurruentState = state;

        CurruentState.OperateEnter();
    }

    public void DoOperateUpdate()
    {
        CurruentState.OperateUpdate();
    }

    public void DoOperateFixedUpdate()
    {
        CurruentState.OperateFixedUpdate();
    }
}

public interface IState
{
    void OperateEnter();
    void OperateUpdate();
    void OperateFixedUpdate();
    void OperateExit();
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public IState CurruentState { get; private set; }

    public StateMachine(IState defaultState)
    {
        CurruentState = defaultState;
    }

    public void SetState(IState state)
    {
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
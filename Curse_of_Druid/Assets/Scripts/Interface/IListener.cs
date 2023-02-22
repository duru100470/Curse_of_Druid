using UnityEngine;

public enum EVENT_TYPE
{
    TileDestroyed,
    LeverPulled,
    PlayerHPChange,
    GameOver
}

public interface IListener
{
    void OnEvent(EVENT_TYPE eType, Component sender, object param = null);
}

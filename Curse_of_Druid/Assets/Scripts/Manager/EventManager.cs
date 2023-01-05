using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : SingletonBehavior<EventManager>
{
    private Dictionary<EVENT_TYPE, List<IListener>> Listeners = new Dictionary<EVENT_TYPE, List<IListener>>();

    /// <summary>
    /// EventManager에 이벤트를 리슨하도록 등록
    /// </summary>
    public void AddListener(EVENT_TYPE eType, IListener listener)
    {
        List<IListener> ListenList = null;

        if (Listeners.TryGetValue(eType, out ListenList))
        {
            ListenList.Add(listener);
            return;
        }

        ListenList = new List<IListener>();
        ListenList.Add(listener);
        Listeners.Add(eType, ListenList);
    }

    /// <summary>
    /// EventManager에 이벤트를 더 이상 리슨하지 않도록 삭제
    /// </summary>
    public void RemoveListener(EVENT_TYPE eType, IListener listener) => Listeners[eType].Remove(listener);

    /// <summary>
    /// EVENT_TYPE을 리슨하는 모든 리스너 리스트 삭제
    /// </summary>
    public void RemoveEvent(EVENT_TYPE eType) => Listeners.Remove(eType);

    /// <summary>
    /// 이벤트를 구독한 리스너들에게 Post
    /// </summary>
    public void PostNotification(EVENT_TYPE eType, Component sender, object param = null)
    {
        List<IListener> ListenList = null;

        if (!Listeners.TryGetValue(eType, out ListenList))
            return;

        for (int i = 0; i < ListenList.Count; i++)
            ListenList[i]?.OnEvent(eType, sender, param);
    }

    public void RemoveRedundancies()
    {
        Dictionary<EVENT_TYPE, List<IListener>> newListeners = new Dictionary<EVENT_TYPE, List<IListener>>();

        foreach (KeyValuePair<EVENT_TYPE, List<IListener>> item in Listeners)
        {
            for (int i = item.Value.Count - 1; i >= 0; i--)
            {
                if (item.Value[i].Equals(null))
                    item.Value.RemoveAt(i);
            }

            if (item.Value.Count > 0)
                newListeners.Add(item.Key, item.Value);
        }

        Listeners = newListeners;
    }
}

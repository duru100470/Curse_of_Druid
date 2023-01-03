using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : SingletonBehavior<EventManager>
{
    private Dictionary<EVENT_TYPE, List<IListener>> Listeners = new Dictionary<EVENT_TYPE, List<IListener>>();

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

    public void RemoveListener(EVENT_TYPE eType, IListener listener) => Listeners[eType].Remove(listener);

    public void RemoveEvent(EVENT_TYPE eType) => Listeners.Remove(eType);

    public void PostNotification(EVENT_TYPE eType, Component sender, object param = null)
    {
        List<IListener> ListenList = null;

        if (!Listeners.TryGetValue(eType, out ListenList))
            return;

        for (int i = 0; i < ListenList.Count; i++)
            ListenList?[i].OnEvent(eType, sender, param);
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

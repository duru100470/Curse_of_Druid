using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : SingletonBehavior<ObjectPool<T>> where T : MonoBehaviour
{
    [SerializeField]
    private GameObject poolingObjectPrefab;

    Queue<T> poolingObjectQueue = new Queue<T>();

    private void Awake()
    {
        Initialize(10);
    }

    private void Initialize(int initCount)
    {
        for(int i = 0; i < initCount; i++)
        {
            poolingObjectQueue.Enqueue(CreateNewObject());
        }
    }

    private T CreateNewObject()
    {
        var newObj = Instantiate(poolingObjectPrefab).GetComponent<T>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        return newObj;
    }

    public static T GetObject()
    {
        if(Inst.poolingObjectQueue.Count > 0)
        {
            var obj = Inst.poolingObjectQueue.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = Inst.CreateNewObject();
            newObj.gameObject.SetActive(true);
            newObj.transform.SetParent(null);
            return newObj;
        }
    }

    public static void ReturnObject(T obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(Inst.transform);
        Inst.poolingObjectQueue.Enqueue(obj);
    }
}

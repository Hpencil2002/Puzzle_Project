using System;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    Queue<Key> keys = new Queue<Key>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (keys.Count > 0)
        {
            keys.Clear();
        }
    }

    public int ShowQueueCount()
    {
        return keys.Count;
    }

    public void InsertQueue(Key key)
    {
        keys.Enqueue(key);
    }

    public void PopQueue()
    {
        if (keys.Count > 0)
        {
            keys.Dequeue();
        }
    }

    public void CheckQueue()
    {
        foreach (Key key in keys)
        {
            Debug.Log(key.number);
        }
    }

    public Key CheckFirstKey()
    {
        if (keys.Count > 0)
        {
            return keys.Peek();
        }
        else
        {
            return null;
        }
    }
}

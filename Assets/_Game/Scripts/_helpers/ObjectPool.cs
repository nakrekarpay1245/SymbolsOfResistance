using System.Collections.Generic;
using System;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private readonly Func<T> _objectFactory;
    private readonly int _initialSize;
    private readonly Queue<T> _objects;

    public ObjectPool(Func<T> objectFactory, int initialSize)
    {
        this._objectFactory = objectFactory;
        this._initialSize = initialSize;
        this._objects = new Queue<T>(initialSize);

        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < _initialSize; i++)
        {
            T obj = _objectFactory();
            _objects.Enqueue(obj);
        }
    }

    public T GetObjectFromPool()
    {
        if (_objects.Count == 0)
        {
            T newObj = _objectFactory();
            return newObj;
        }

        return _objects.Dequeue();
    }

    public void ReturnObjectToPool(T obj)
    {
        obj.gameObject.SetActive(false);
        _objects.Enqueue(obj);
    }
}
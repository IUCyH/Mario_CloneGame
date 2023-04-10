using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : class
{
    public delegate T FuncDel();

    Queue<T> queue = new Queue<T>();
    FuncDel funcDel;
    int count;

    public ObjectPool(int count, FuncDel funcDel)
    {
        this.count = count;
        this.funcDel = funcDel;
        Allocate();
    }

    void Allocate()
    {
        for (int i = 0; i < count; i++)
        {
            queue.Enqueue(funcDel());
        }
    }

    public T Get()
    {
        if (queue.Count < 1)
        {
            return funcDel();
        }

        return queue.Dequeue();
    }

    public void Set(T obj)
    {
        queue.Enqueue(obj);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    Transform item;

    protected abstract void PlayShowAnimation();
    
    public void Show(Vector3 position)
    {
        item.position = position;
        gameObject.SetActive(true);

        PlayShowAnimation();
    }

    void Awake()
    {
        item = transform;
    }
}

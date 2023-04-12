using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using Random = System.Random;

public abstract class Item : MonoBehaviour
{
    Transform item;
    Vector3 itemPos;

    protected abstract void PlayShowAnimation(Vector3 targetPos);

    public void SetItemPosition(Vector3 position)
    {
        itemPos = position;
    }
    
    void OnEnable()
    {
        item.position = itemPos;

        PlayShowAnimation(itemPos);
    }

    void OnBecameInvisible()
    {
        ItemManager.Instance.DestroyItem(gameObject);
    }

    void Awake()
    {
        item = transform;
    }
}

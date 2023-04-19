using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UIElements;
using Random = System.Random;

public abstract class Item : MonoBehaviour
{
    Vector3 itemPos;
    [SerializeField] 
    float finalItemPosY = 1f;

    protected abstract void PlayShowAnimation(Vector3 targetPos);
    protected abstract void AddEffectToPlayer();
    protected virtual void OnStart(){}

    public void GiveEffectAndDestroy()
    {
        AddEffectToPlayer();
        DestroyItem();
    }
    void DestroyItem()
    {
        ItemManager.Instance.DestroyItem(gameObject);
    }
    
    public void SetItemPosition(Vector3 position)
    {
        itemPos = position;
    }
    
    void OnEnable()
    {
        var item = transform;
        item.position = itemPos;
        itemPos.y += finalItemPosY;

        OnStart();
        PlayShowAnimation(itemPos);
    }

    void OnBecameInvisible()
    {
        ItemManager.Instance.DestroyItem(gameObject);
    }
}

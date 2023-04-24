using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Random = System.Random;

using ItemType = ItemManager.ItemType;

[RequireComponent(typeof(ItemFuncExecuteMedium))]
[RequireComponent(typeof(Coin))]
[RequireComponent(typeof(MagicMushroom))]
[RequireComponent(typeof(UpMushroom))]
[RequireComponent(typeof(StarMan))]
public class ItemController : MonoBehaviour
{
    Transform itemTransform;
    ItemFuncExecuteMedium itemFuncExecuteMedium;
    
    Vector3 itemPos;
    [SerializeField]
    ItemType itemType;

    [SerializeField] 
    float finalItemPosY = 1f;
    
    public void SetItemType(ItemType type)
    {
        itemType = type;
    }
    
    public void GiveEffectAndDestroy()
    {
        itemFuncExecuteMedium.AddEffectToPlayer(itemType);
        DestroyItem();
    }

    public void SetItemPosition(Vector3 position)
    {
        itemPos = position;
    }

    public void OnShow()
    {
        itemTransform.position = itemPos;
        itemPos.y += finalItemPosY;
        
        itemFuncExecuteMedium.PlayShowAnimation(itemPos, itemType);
    }
    
    void DestroyItem()
    {
        ItemManager.Instance.DestroyItem(gameObject);
    }

    void Awake()
    {
        itemFuncExecuteMedium = GetComponent<ItemFuncExecuteMedium>();
        itemTransform = transform;
    }
    
    void Update()
    {
        if (!GameSystemManager.Instance.IsInsideTheCamera(transform))
        {
            DestroyItem();
        }
    }
}
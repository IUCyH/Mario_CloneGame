using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemFuncExecuteMedium : MonoBehaviour
{
    Dictionary<ItemManager.ItemType, Item> items = new Dictionary<ItemManager.ItemType, Item>();
    
    public void PlayShowAnimation(Vector3 targetPos, ItemManager.ItemType itemType)
    {
        items[itemType].PlayShowAnimation(targetPos);
    }

    public void AddEffectToPlayer(ItemManager.ItemType itemType)
    {
        items[itemType].AddEffectToPlayer();
    }

    public void Awake()
    {
        InitItems();
    }

    void Start()
    {
        foreach (var item in items.Values)
        {
            item.OnStart();
        }
    }

    void InitItems()
    {
        items.Add(ItemManager.ItemType.Coin, GetComponent<Coin>());
        items.Add(ItemManager.ItemType.MagicMushroom, GetComponent<MagicMushroom>());
        items.Add(ItemManager.ItemType.UpMushroom, GetComponent<UpMushroom>());
        items.Add(ItemManager.ItemType.StarMan, GetComponent<StarMan>());
    }
}

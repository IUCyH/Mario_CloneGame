using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = System.Random;

public class ItemManager : SingletonMonoBehaviour<ItemManager>
{
    enum ItemType
    {
        None = -1,
        Coin,
        MagicMushroom,
        UpMushroom,
        StarMan,
        Max
    }
    
    ObjectPool<GameObject> itemPool;
    
    [SerializeField]
    GameObject itemGameObj;
    [SerializeField]
    Sprite[] itemSprites;

    Random random = new Random();
    
    Transform itemManagerTransform;

    public void DestroyItem(GameObject item)
    {
        Destroy(item.GetComponent<Item>());
        
        item.SetActive(false);
        itemPool.Set(item);
    }
    
    public void ShowItem(Vector3 itemPosition)
    {
        var item = itemPool.Get();
        SetItem(item, itemPosition);

        item.SetActive(true);
    }
    
    void SetItem(GameObject item, Vector3 itemPosition)
    {
        var itemType = GetRandomItem();
        Item itemInstance = null;
        
        switch (itemType)
        {
            case ItemType.Coin:
                itemInstance = item.AddComponent<Coin>();
                break;
            case ItemType.MagicMushroom:
                itemInstance = item.AddComponent<MagicMushroom>();
                break;
            case ItemType.UpMushroom:
                itemInstance = item.AddComponent<UpMushroom>();
                break;
            case ItemType.StarMan:
                itemInstance = item.AddComponent<StarMan>();
                break;
        }

        var spriteRenderer = item.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = GetItemSprite((int)itemType);

        if (itemInstance != null)
        {
            itemInstance.SetItemPosition(itemPosition);
        }
    }
    
    ItemType GetRandomItem()
    {
        return (ItemType)random.Next((int)ItemType.None + 1, (int)ItemType.Max);
    }
    
    Sprite GetItemSprite(int type)
    {
        return itemSprites[type];
    }
    
    protected override void OnStart()
    {
        itemManagerTransform = transform;

        itemSprites = Resources.LoadAll<Sprite>("Item/Sprites");
        itemPool = new ObjectPool<GameObject>(itemSprites.Length * 2, () =>
        {
            var obj = Instantiate(itemGameObj);
            var objTransform = obj.transform;
            
            objTransform.SetParent(itemManagerTransform);
            objTransform.position = Vector3.zero;
            objTransform.localScale = Vector3.one;
            
            obj.SetActive(false);
            
            return obj;
        });
    }
}

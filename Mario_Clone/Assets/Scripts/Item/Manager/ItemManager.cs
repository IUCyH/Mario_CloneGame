using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = System.Random;

public class ItemManager : SingletonMonoBehaviour<ItemManager>
{
    public enum ItemType
    {
        None = -1,
        Coin,
        MagicMushroom,
        UpMushroom,
        StarMan,
        Max
    }
    
    ObjectPool<GameObject> itemPool;
    Random random = new Random();

    [SerializeField]
    GameObject itemObj;
    [SerializeField]
    GameObject interactiveTile;
    [SerializeField]
    Sprite[] itemSprites;

    Transform itemManagerTransform;
    Dictionary<GameObject, bool> usedItemBoxes = new Dictionary<GameObject, bool>();

    
    
    public void DestroyItem(GameObject item)
    {
        item.SetActive(false);
        itemPool.Set(item);
    }
    
    public void ShowItem(Vector3 itemPosition, GameObject itemBox)
    {
        if (usedItemBoxes[itemBox]) return;

        usedItemBoxes[itemBox] = true;

        var item = itemPool.Get();
        var itemController = SetItem(item, itemPosition);

        item.SetActive(true);
        itemController.OnShow();
    }
    
    ItemController SetItem(GameObject item, Vector3 itemPosition)
    {
        var itemType = GetRandomItem();
        var spriteRenderer = item.GetComponent<SpriteRenderer>();
        var itemInstance = item.GetComponent<ItemController>();

        itemInstance.SetItemType(itemType);
        itemInstance.SetItemPosition(itemPosition);
        
        spriteRenderer.sprite = GetItemSprite((int)itemType);

        return itemInstance;
    }
    
    ItemType GetRandomItem()
    {
        return (ItemType)random.Next((int)ItemType.None + 1, (int)ItemType.Max);
    }
    
    Sprite GetItemSprite(int type)
    {
        return itemSprites[type];
    }

    void InitUsedItemBoxes()
    {
        var itemBoxes = interactiveTile.GetComponentsInChildren<SpriteRenderer>();

        for (int i = 0; i < itemBoxes.Length; i++)
        {
            usedItemBoxes.Add(itemBoxes[i].gameObject, false);
        }
    }
    
    protected override void OnStart()
    {
        itemManagerTransform = transform;

        itemSprites = Resources.LoadAll<Sprite>("Item/Sprites");
        itemPool = new ObjectPool<GameObject>(itemSprites.Length, () =>
        {
            var obj = Instantiate(itemObj);
            var objTransform = obj.transform;
            
            objTransform.SetParent(itemManagerTransform);
            objTransform.position = Vector3.zero;
            objTransform.localScale = Vector3.one;
            
            obj.SetActive(false);
            
            return obj;
        });
        InitUsedItemBoxes();
    }
}

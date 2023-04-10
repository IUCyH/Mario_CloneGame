using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : SingletonMonoBehaviour<ItemManager>
{
    Dictionary<int, ObjectPool<Item>> itemDictionary = new Dictionary<int, ObjectPool<Item>>();
    [SerializeField]
    Item[] items;
    Transform itemManagerTransform;

    protected override void OnStart()
    {
        items = Resources.LoadAll<Item>("Item/Prefabs");
        itemManagerTransform = transform;
        
        var length = items.Length;
        for (int i = 0; i < length; i++)
        {
            var item = items[i];
            itemDictionary.Add(i, new ObjectPool<Item>(2, () =>
            {
                var obj = Instantiate(item);
                var objTransform = obj.transform;
                
                objTransform.SetParent(itemManagerTransform);
                objTransform.localPosition = Vector3.zero;
                objTransform.localScale = Vector3.one;
                obj.gameObject.SetActive(false);
                
                return obj;
            }));
        }
    }
}

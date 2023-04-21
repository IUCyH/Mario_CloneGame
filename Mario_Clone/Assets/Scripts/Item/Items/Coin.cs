using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, Item
{
    [SerializeField]
    float animDuration = 1f;

    IEnumerator Coroutine_ShowAnimation(Vector3 targetPos)
    {
        var coin = transform;
        float time = 0f;

        while (true)
        {
            var nextY = Mathf.Lerp(coin.position.y, targetPos.y, 0.008f);//수정예정
            var coinPos = coin.position;
            
            coin.position = new Vector3(coinPos.x, nextY, coinPos.z);
            
            time += Time.deltaTime / animDuration;

            if (time > 1f)
            {
                yield break;
            }
            
            yield return null;
        }
    }
    
    public void PlayShowAnimation(Vector3 targetPos)
    {
        StartCoroutine(Coroutine_ShowAnimation(targetPos));
    }

    public void AddEffectToPlayer()
    {
        DataManager.Instance.IncreaseCoin(1);
        UIManager.Instance.UpdateCoinText(DataManager.Instance.Coin);
    }

    public void OnStart()
    {
        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item
{
    Transform coin;
    [SerializeField]
    float animDuration = 1f;

    IEnumerator Coroutine_ShowAnimation(Vector3 targetPos)
    {
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
    
    protected override void PlayShowAnimation(Vector3 targetPos)
    {
        StartCoroutine(Coroutine_ShowAnimation(targetPos));
    }

    protected override void AddEffectToPlayer()
    {
        DataManager.Instance.IncreaseCoin(1);
    }

    protected override void OnStart()
    {
        coin = transform;
    }
}

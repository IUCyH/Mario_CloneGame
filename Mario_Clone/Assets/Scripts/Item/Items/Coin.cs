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
        Vector3 coinOrginPos = coin.position;
        float time = 0f;

        while (true)
        {
            var posProgress = Vector3.Lerp(coinOrginPos, targetPos, 0.5f); //수정예정
            posProgress.x = coin.position.x;
            posProgress.z = 0f;
            
            coin.position = posProgress;
            
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
        Debug.Log("It's Coin!");
    }
}

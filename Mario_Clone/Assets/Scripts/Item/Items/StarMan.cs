using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarMan : Item
{
    Transform starMan;
    float animDuration = 1f;
    
    IEnumerator Coroutine_ShowAnimation(Vector3 targetPos)
    {
        float time = 0f;

        while (true)
        {
            var nextY = Mathf.Lerp(starMan.position.y, targetPos.y, 0.008f);//수정예정
            var coinPos = starMan.position;
            
            starMan.position = new Vector3(coinPos.x, nextY, coinPos.z);
            
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
        
    }

    protected override void OnStart()
    {
        starMan = transform;
    }
}

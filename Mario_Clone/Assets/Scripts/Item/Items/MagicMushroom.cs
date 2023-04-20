using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMushroom : MonoBehaviour, Item
{
    [SerializeField]
    float animDuration = 1f;
    
    IEnumerator Coroutine_ShowAnimation(Vector3 targetPos)
    {
        var magicMushroom = transform;
        float time = 0f;

        while (true)
        {
            var nextY = Mathf.Lerp(magicMushroom.position.y, targetPos.y, 0.008f);//수정예정
            var coinPos = magicMushroom.position;
            
            magicMushroom.position = new Vector3(coinPos.x, nextY, coinPos.z);
            
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
        Debug.Log("magic mushroom!");
    }
}

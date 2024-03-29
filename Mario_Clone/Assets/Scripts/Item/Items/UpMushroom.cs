using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpMushroom : MonoBehaviour, Item
{
    [SerializeField]
    BoxCollider2D collider2D;
    [SerializeField]
    float animDuration = 1f;
    
    IEnumerator Coroutine_ShowAnimation(Vector3 targetPos)
    {
        var upMushroom = transform;
        float time = 0f;

        while (true)
        {
            var nextY = Mathf.Lerp(upMushroom.position.y, targetPos.y, 0.008f);//수정예정
            var coinPos = upMushroom.position;
            
            upMushroom.position = new Vector3(coinPos.x, nextY, coinPos.z);
            
            time += Time.deltaTime / animDuration;
            Debug.Log("Time : " + time);
            
            if (Mathf.Approximately(time, 0.5f))
            {
                collider2D.enabled = true;
            }

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
        Debug.Log("up mushroom!");
    }
    
    public void OnAwake()
    {
        collider2D.enabled = false;
    }
}

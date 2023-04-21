using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarMan : MonoBehaviour, Item
{
    PlayerController player;
    [SerializeField]
    float animDuration = 1f;
    
    IEnumerator Coroutine_ShowAnimation(Vector3 targetPos)
    {
        var starMan = transform;
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
    
    public void PlayShowAnimation(Vector3 targetPos)
    {
        StartCoroutine(Coroutine_ShowAnimation(targetPos));
    }
    
    public void AddEffectToPlayer()
    {
        player.SetPlayerState(PlayerController.PlayerState.Invincibility);
    }
    
    public void OnStart()
    {
        var playerObj = GameObject.FindWithTag("Player");
        player = playerObj.GetComponent<PlayerController>();
    }
}

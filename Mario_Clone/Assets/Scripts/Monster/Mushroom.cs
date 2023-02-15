using System;
using UnityEngine;

public class Mushroom : MonsterAI
{
    [SerializeField]
    float speed;

    public override void Move()
    {
        bool isAnimPlaying = monsterAnimator.GetBool(moveAnimParamId);
        if (!isAnimPlaying)
        {
            monsterAnimator.SetBool(moveAnimParamId, true);
        }
        
        monster.position += speed * Time.deltaTime * Vector3.left;
    }
}

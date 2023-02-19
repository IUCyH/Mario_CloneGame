using System;
using UnityEngine;

public class Goomba : MonsterAI
{
    [SerializeField]
    float speed;

    public override void Move()
    {
        monster.position += speed * Time.deltaTime * Vector3.left;
    }

    public override void SetDamage()
    {
        speed = 0f;
        monsterAnimator.enabled = false;
    }
}

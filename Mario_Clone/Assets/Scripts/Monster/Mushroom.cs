using System;
using UnityEngine;

public class Mushroom : MonsterAI
{
    [SerializeField]
    float speed;
    
    public override void Move()
    {
        monster.position += speed * Time.deltaTime * Vector3.left;
    }
}

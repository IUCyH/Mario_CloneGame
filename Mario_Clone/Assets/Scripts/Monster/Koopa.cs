using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koopa : MonsterAI
{
    [SerializeField]
    float speed;

    public override void Move()
    {
        monster.position += speed * Time.deltaTime * Vector3.left;
    }
}

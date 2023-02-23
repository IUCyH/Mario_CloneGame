using System;
using UnityEngine;

public class Goomba : MonsterAI
{
    [SerializeField]
    Collider2D monsterColl;
    [SerializeField]
    Rigidbody2D monsterRB;
    [SerializeField]
    float speed;

    public override void Move()
    {
        monster.position += speed * Time.deltaTime * Vector3.left;
    }

    protected override void AdditionalActionsWhenGotDamage()
    {
        StopPhysics();
    }

    void StopPhysics()
    {
        monsterRB.bodyType = RigidbodyType2D.Kinematic;   
        monsterColl.enabled = false;
    }

    protected override void OnStart()
    {
        monsterRB = GetComponent<Rigidbody2D>();
        monsterColl = GetComponent<Collider2D>();
    }
}

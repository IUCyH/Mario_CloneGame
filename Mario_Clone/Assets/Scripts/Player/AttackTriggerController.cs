using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTriggerController : InteractionTrigger
{
    const string monsterTag = "Monster";
    [SerializeField]
    PlayerController player;

    protected override void Interact(Collider2D otherCollider)
    {
        if (!otherCollider.CompareTag(monsterTag)) return;

        var monster = otherCollider.GetComponent<MonsterAI>();
        monster.SetDie();
        player.JumpWhenSteppingMonster();
    }
}

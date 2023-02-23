using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTriggerController : InteractionTrigger
{
    protected override void Interact(Collider2D otherCollider)
    {
        var monster = otherCollider.GetComponent<MonsterAI>();

        if (!ReferenceEquals(monster, null))
        {
            monster.SetDamage();
        }
    }
}

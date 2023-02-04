using System;
using UnityEngine;

public abstract class MonsterAI : MonoBehaviour
{
    const float rayCastDistance = 12f;
    protected Transform monster;
    
    Animator monsterAnimator;
    
    int playerLayer;
    bool isMoving;
    
    public abstract void Move();

    public bool IsCanMove()
    {
        if (isMoving) return true;

        RaycastHit2D raycastHit = Physics2D.Raycast(monster.position, Vector2.left, rayCastDistance, playerLayer);

        if (!ReferenceEquals(raycastHit.collider, null))
        {
            monsterAnimator.SetBool("Move", true);
            isMoving = true;
            return true;
        }

        return false;
    }

    void Start()
    {
        monsterAnimator = GetComponent<Animator>();
        monster = transform;
        playerLayer = 1 << LayerMask.NameToLayer("Player");
    }
}

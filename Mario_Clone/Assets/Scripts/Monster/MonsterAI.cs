using System;
using UnityEngine;

public abstract class MonsterAI : MonoBehaviour
{
    const float RayCastDistance = 12f;
    protected int moveAnimParamId;
    protected Transform monster;
    protected Animator monsterAnimator;
    
    Transform endPosOfCam;
    Transform player;
    
    int playerLayer;
    bool isMoving;
    
    public abstract void Move();

    public bool IsCanMove()
    {
        if (isMoving) return true;

        var raycastDir = player.position - monster.position;
        RaycastHit2D raycastHit = Physics2D.Raycast(monster.position, raycastDir, RayCastDistance, playerLayer);

        if (!ReferenceEquals(raycastHit.collider, null))
        {
            isMoving = true;
            return true;
        }

        return false;
    }

    public bool IsOutsideTheScreen()
    {
        if (endPosOfCam.position.x > monster.position.x)
        {
            return true;
        }

        return false;
    }

    public void SetCannotMove()
    {
        if (!gameObject.activeSelf)
        {
            return;
        }
        
        isMoving = false;
        gameObject.SetActive(false);
    }

    void Start()
    {
        var endOfCamObj = GameObject.FindGameObjectWithTag("EndOfCam");
        var playerObj = GameObject.FindGameObjectWithTag("Player");
        
        endPosOfCam = endOfCamObj.transform;
        player = playerObj.transform;
        monster = transform;
        
        monsterAnimator = GetComponent<Animator>();
        moveAnimParamId = Animator.StringToHash("Move");
        
        playerLayer = 1 << LayerMask.NameToLayer("Player");
    }
}

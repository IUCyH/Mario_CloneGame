using System;
using UnityEngine;

public enum MonsterState
{
    None = -1,
    Moving,
    Die,
}

public abstract class MonsterAI : MonoBehaviour
{
    const float RayCastDistance = 12f;
    protected Transform monster;
    protected Animator monsterAnimator;

    Transform endPosOfCam;
    Transform player;

    MonsterState currentState;
    int playerLayer;

    public abstract void Move();
    public abstract void SetDamage();

    public bool IsCanMove()
    {
        if (StateEquals(currentState, MonsterState.Moving)) return true;

        var raycastDir = player.position - monster.position;
        RaycastHit2D raycastHit = Physics2D.Raycast(monster.position, raycastDir, RayCastDistance, playerLayer);

        if (!ReferenceEquals(raycastHit.collider, null))
        {
            currentState = MonsterState.Moving;
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

    public void SetActiveToFalse()
    {
        if (!gameObject.activeSelf)
        {
            return;
        }
        
        gameObject.SetActive(false);
    }

    bool StateEquals(MonsterState state1, MonsterState state2)
    {
        return state1 == state2;
    }

    void Start()
    {
        var endOfCamObj = GameObject.FindGameObjectWithTag("EndOfCam");
        var playerObj = GameObject.FindGameObjectWithTag("Player");
        
        endPosOfCam = endOfCamObj.transform;
        player = playerObj.transform;
        monster = transform;

        monsterAnimator = GetComponent<Animator>();
        
        playerLayer = 1 << LayerMask.NameToLayer("Player");
        currentState = MonsterState.None;
    }
}

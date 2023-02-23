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
    

    Transform endPosOfCam;
    Transform player;
    SpriteRenderer monsterSprRenderer;
    Animator monsterAnimator;

    MonsterState currentState;
    
    string monsterName;
    int playerLayer;

    public abstract void Move();
    protected virtual void AdditionalActionsWhenGotDamage(){}
    protected virtual void OnStart(){}

    public void SetDamage()
    {
        monsterSprRenderer.sprite = MonsterManager.Instance.GetMonsterDieSprite(monsterName);
        monsterAnimator.enabled = false;
        currentState = MonsterState.Die;

        AdditionalActionsWhenGotDamage();
    }
    public bool IsCanMove()
    {
        if (StateEquals(currentState, MonsterState.Moving)) return true;
        else if (StateEquals(currentState, MonsterState.Die)) return false;

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
        monsterSprRenderer = GetComponent<SpriteRenderer>();
        
        playerLayer = 1 << LayerMask.NameToLayer("Player");
        currentState = MonsterState.None;

        var nameSplitResult = name.Split('_');
        monsterName = nameSplitResult[0];

        OnStart();
    }
}

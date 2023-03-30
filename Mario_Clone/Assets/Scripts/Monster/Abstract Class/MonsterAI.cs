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
    const float RaycastDistance = 12f;
    
    protected Transform monster;
    protected ushort id;

    Transform endPosOfCam;
    Transform player;
    SpriteRenderer monsterSprRenderer;
    Animator monsterAnimator;

    MonsterState currentState;
    
    int playerLayer;

    public abstract void Move();
    protected abstract void SetMonster();
    protected abstract void ChangeToOppositeDir();
    
    protected virtual void AdditionalActionsWhenGotDamage(){}
    protected virtual void OnStart(){}

    public void SetDie()
    {
        currentState = MonsterState.Die;
        monsterSprRenderer.sprite = MonsterManager.Instance.GetMonsterDieSprite(id);
        monsterAnimator.enabled = false;
        
        AdditionalActionsWhenGotDamage();
    }
    public bool IsCanMove()
    {
        if (StateEquals(currentState, MonsterState.Moving)) return true;
        else if (StateEquals(currentState, MonsterState.Die)) return false;

        var raycastDir = player.position - monster.position;
        RaycastHit2D raycastHit = Physics2D.Raycast(monster.position, raycastDir, RaycastDistance, playerLayer);

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

        OnStart();
        SetMonster();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        var contactTransform = col.transform;
        if (contactTransform.CompareTag("Player") || contactTransform.CompareTag("Map")) return;

        ChangeToOppositeDir();
    }
}

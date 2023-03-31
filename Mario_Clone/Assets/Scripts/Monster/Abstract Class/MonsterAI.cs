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
    
    Transform player;
    SpriteRenderer monsterSprRenderer;
    Animator monsterAnimator;

    MonsterState currentState;
    
    int playerLayer;
    bool becameVisible;
    
    public bool OutsideTheScreen { get; private set; }

    public abstract void Move();
    protected abstract void SetMonster();
    protected abstract void ChangeToOppositeDir();
    
    protected virtual void AdditionalActionsWhenGotDamage(){}
    protected virtual void AdditionalActionWhenCollided(){}
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
        var playerObj = GameObject.FindGameObjectWithTag("Player");
        
        player = playerObj.transform;
        monster = transform;

        monsterAnimator = GetComponent<Animator>();
        monsterSprRenderer = GetComponent<SpriteRenderer>();
        
        playerLayer = 1 << LayerMask.NameToLayer("Player");
        currentState = MonsterState.None;
        OutsideTheScreen = false;
        
        OnStart();
        SetMonster();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        AdditionalActionWhenCollided();
        
        var contactTransform = col.transform;
        if (contactTransform.CompareTag("Player") || contactTransform.CompareTag("Map")) return;

        ChangeToOppositeDir();
    }
    
    void OnBecameInvisible()
    {
        OutsideTheScreen = true;
    }
}

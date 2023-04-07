using System;
using UnityEngine;

public enum MonsterState
{
    None = -1,
    Moving,
    Die
}

public abstract class MonsterAI : MonoBehaviour
{
    const float RaycastDistance = 12f;
    
    protected Transform monster;
    protected SpriteRenderer monsterSprRenderer;
    protected MonsterState currentState;
    protected ushort id;
    
    Transform player;
    Animator monsterAnimator;


    const string PlayerTag = "Player";
    const string MapTag = "Map";
    const string MonsterTag = "Monster";
    const string MysteryBoxTag = "MysteryBox";
    
    
    int playerLayer;
    bool becameVisible;
    
    public bool OutsideTheScreen { get; private set; }

    public abstract void Move();
    protected abstract void SetMonster();
    protected abstract void ChangeToOppositeDir();
    
    protected virtual void AdditionalActionsWhenGotDamage(){}
    protected virtual void AdditionalActionWhenCollided(Collision2D col){}

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
        
        SetMonster();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        AdditionalActionWhenCollided(col);
        
        var contactTransform = col.transform;
        
        if (contactTransform.CompareTag(PlayerTag)) return;
        if (contactTransform.CompareTag(MapTag)) return;
        if (contactTransform.CompareTag(MonsterTag)) return;
        if (contactTransform.CompareTag(MysteryBoxTag)) return;

        ChangeToOppositeDir();
    }
    
    void OnBecameInvisible()
    {
        if (player.position.x < monster.position.x) return;
        
        OutsideTheScreen = true;
    }
}

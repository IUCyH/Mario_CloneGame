using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PlayerState
    {
        None = -1,
        Invincibility,
        BigMario,
        SmallMario
    }
    
    PlayerAnimation playerAnimController;
    
    [SerializeField]
    PlayerMove playerMove;
    [SerializeField]
    PlayerJump playerJump;
    [SerializeField]
    MovementLimit playerMovementLimit;
    
    [SerializeField]
    Transform feetPos;
    [SerializeField]
    Transform headPos;
    
    [Tooltip("*현재 설정해야 하는 List 크기 : 2*\n각 인덱스가 나타내는 PlayerState : (0 : Invincibility, 1 : BigMario)")]
    [SerializeField]
    [Range(0.1f, 1f)]
    List<float> stateCoolDowns = new List<float>();
    
    [SerializeField]
    PlayerState playerState;
    
    int jumpCount;
    [SerializeField]
    float stateCoolDown;
    [SerializeField]
    float stateCoolTimer;
    bool isPressedJumpKey;
    
    public PlayerState GetPlayerState { get => playerState; }

    public void SetPlayerState(PlayerState state)
    {
        playerState = state;
        stateCoolDown = GetStateCoolDown();
    }

    public void SetDie()
    {
        if (StateEquals(PlayerState.Invincibility)) return;
        
        playerAnimController.Play(PlayerMotion.Die);
        Time.timeScale = 0f;
    }

    public void PlayAnimation(PlayerMotion motion)
    {
        playerAnimController.Play(motion);
    }

    public void StopAnimation(PlayerMotion motion)
    {
        playerAnimController.Stop(motion);
    }

    public void SetAnimationSpeed(PlayerAnimSpeedParams animSpeedParam, float animSpeed = 1f)
    {
        playerAnimController.SetAnimationSpeed(animSpeedParam, animSpeed);
    }
    
    public bool IsPlayerCanMove(float dir)
    {
        return !playerMovementLimit.IsPlayerCannotMove(dir);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        var contactObj = col.contacts[0];
        
        if (contactObj.point.y > headPos.position.y) playerJump.ReleaseJump();
        ActionOnTag(col.collider, contactObj);
    }
    
    void ActionOnTag(Collider2D contactObjCollider, ContactPoint2D contactObj)
    {
        if (contactObjCollider.CompareTag("Monster"))
        {
            ActionWhenMonsterCollided(contactObjCollider.transform);  
        }
        else if(contactObjCollider.CompareTag("MysteryBox"))
        {
            ActionWhenMysteryBoxCollided(contactObj.point, contactObjCollider.gameObject);
        }
        else if (contactObjCollider.CompareTag("Item"))
        {
            ActionWhenItemCollided(contactObjCollider);
        }
    }

    void ActionWhenMonsterCollided(Transform monsterTransform)
    {
        var isInvisible = StateEquals(PlayerState.Invincibility);
        if (monsterTransform.position.y <= feetPos.position.y || isInvisible)
        {
            var monster = monsterTransform.GetComponent<MonsterAI>();
            if (monster != null)
            {
                monster.SetDie();
                if(!isInvisible) playerJump.JumpWhenSteppingMonster();
            }
        }
        else
        {
            this.SetDie();
        }
    }

    void ActionWhenMysteryBoxCollided(Vector3 point, GameObject collidedObj)
    {
        if(point.y < headPos.position.y) return;
        
        ItemManager.Instance.ShowItem(collidedObj.transform.position, collidedObj);
    }

    void ActionWhenItemCollided(Collider2D collidedObj)
    {
        var item = collidedObj.gameObject.GetComponent<ItemController>();
        if (item != null)
        {
            item.GiveEffectAndDestroy();
        }
    }

    float GetStateCoolDown()
    {
        if (StateEquals(PlayerState.None) || StateEquals(PlayerState.SmallMario)) return 0f;
        
        return stateCoolDowns[(int)playerState];
    }

    bool StateEquals(PlayerState targetState)
    {
        return playerState == targetState;
    }

    void CheckStateCoolDown()
    {
        if (StateEquals(PlayerState.None) || StateEquals(PlayerState.SmallMario)) return;

        stateCoolTimer += Time.deltaTime;

        if (stateCoolTimer > stateCoolDown)
        {
            stateCoolTimer = 0f;
            SetPlayerState(PlayerState.SmallMario);
        }
    }
    
    void Start()
    {
        playerAnimController = new PlayerAnimation(GetComponent<Animator>());
        SetPlayerState(PlayerState.None);
    }

    void Update()
    {
        playerJump.CheckJump();
        playerMove.SetMoveSpeed();
        CheckStateCoolDown();
        
        

        if (Input.GetKeyDown(KeyCode.P))
        {
            playerAnimController.Play(PlayerMotion.Idle);
            playerAnimController.Stop(PlayerMotion.Die);
            Time.timeScale = 1f;
        }
    }

    void FixedUpdate()
    {
        playerMove.Move();
        playerJump.Jump();
    }
}

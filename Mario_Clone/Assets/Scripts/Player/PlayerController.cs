using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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

    int jumpCount;
    bool isPressedJumpKey;

    public void SetDie()
    {
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
        if (monsterTransform.position.y <= feetPos.position.y)
        {
            var monster = monsterTransform.GetComponent<MonsterAI>();
            if (monster != null)
            {
                monster.SetDie();
                playerJump.JumpWhenSteppingMonster();
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

    void Start()
    {
        playerAnimController = new PlayerAnimation(GetComponent<Animator>());
    }

    void Update()
    {
        playerJump.CheckJump();
        playerMove.SetMoveSpeed();

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

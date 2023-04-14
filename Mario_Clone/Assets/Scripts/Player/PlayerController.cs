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
        GameObject prevGameObject = null;
        var contacts = col.contacts;
        int contactsCount = contacts.Length;

        for (int i = 0; i < contactsCount; i++)
        {
            var contactObj = contacts[i];
            var contactObjCollider = contactObj.collider;

            if (prevGameObject != null && contactObjCollider.gameObject == prevGameObject) continue;
            if(contactObj.point.y > headPos.position.y) playerJump.ReleaseJump();
            
            ActionOnTag(contactObjCollider, contactObj);

            prevGameObject = contactObjCollider.gameObject;
        }
    }

    void ActionOnTag(Collider2D contactObjCollider, ContactPoint2D contactObj)
    {
        if (contactObjCollider.CompareTag("Monster"))
        {
            ActionWhenMonsterCollided(contactObjCollider.transform);  
        }
        else if(contactObjCollider.CompareTag("MysteryBox"))
        {
            ActionWhenMysteryBoxCollided(contactObj.point);
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

    void ActionWhenMysteryBoxCollided(Vector3 point)
    {
        if (headPos.position.y > point.y) return;
        
        var hasTile = ItemGenerator.Instance.CalculatePositionAndCheckItHasTile(point);
        Debug.Log(hasTile);
        if (hasTile)
        {
            ItemGenerator.Instance.GenerateItem();
        }
    }

    void ActionWhenItemCollided(Collider2D collidedObj)
    {
        var item = collidedObj.gameObject.GetComponent<Item>();
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

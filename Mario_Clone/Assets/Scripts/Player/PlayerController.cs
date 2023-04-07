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
    TilemapPositionCalculate tilemapPosCalculator;
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
    
    public PlayerAnimation GetPlayerAnimController()
    {
        return playerAnimController;
    }
    
    public bool IsPlayerCanMove(float dir)
    {
        return !playerMovementLimit.IsPlayerCannotMove(dir);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        var contacts = col.contacts;
        int contactsCount = contacts.Length;
        
        for (int i = 0; i < contactsCount; i++)
        {
            var contactObj = contacts[i];
            var contactObjCollider = contactObj.collider;
            
            if (contactObjCollider.CompareTag("Monster"))
            {
                ActionWhenMonsterCollided(contactObjCollider.transform);  
            }
            if(contactObjCollider.CompareTag("MysteryBox"))
            {
                ActionWhenMysteryBoxCollied(contactObj.point);
            }
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

    void ActionWhenMysteryBoxCollied(Vector3 point)
    {
        if (headPos.position.y > point.y) return;
        
        tilemapPosCalculator.CalculatePosition(point);
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

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

    int jumpCount;
    bool isPressedJumpKey;

    public void SetDie()
    {
        Time.timeScale = 0f;
        Debug.Log("Im die..");
    }
    
    public PlayerAnimation GetPlayerAnimController()
    {
        return playerAnimController;
    }
    
    public void JumpWhenSteppingMonster()
    {
        
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
            SetDie();
        }
    }

    void Start()
    {
        playerAnimController = new PlayerAnimation(GetComponent<Animator>());
    }

    void Update()
    {
        playerJump.Jump();
        playerMove.SetMoveSpeed();

        if (Input.GetKeyDown(KeyCode.P))
            Time.timeScale = 1f;
    }

    void FixedUpdate()
    {
        playerMove.Move();
    }
}

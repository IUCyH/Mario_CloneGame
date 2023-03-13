using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    int groundLayer;

    [SerializeField]
    PlayerController player;
    PlayerAnimation playerAnimController;
    
    [SerializeField]
    Rigidbody2D playerRB;
    [SerializeField]
    Transform feetPos;

    [SerializeField]
    float groundCheckRadius;
    [SerializeField]
    float defaultJumpForce;
    [SerializeField]
    float jumpForceIncrement;
    [SerializeField]
    float jumpForce;
    [SerializeField]
    float jumpForceWhenSteppingMonster;
    float timer;
    [SerializeField]
    float maxTime;
    
    [SerializeField]
    bool pressJumpKey;
    [SerializeField]
    bool canJump;
    bool steppingMonsterNow;
    bool isJumping;

    const int MaxJumpCount = 1;
    [SerializeField]
    int jumpCount;

    public void CheckIsCanJumpAndActiveTriggers()
    {
        if (steppingMonsterNow)
        {
            canJump = false;
            return;
        }

        var playerOnGroundNow = IsPlayerOnGround();

        canJump = playerOnGroundNow;
        CalculateJumpForceAndJump();

        SetJumpAnimation(playerOnGroundNow);
        player.SetActiveInteractionTriggers(!playerOnGroundNow);
    }
    
    void CalculateJumpForceAndJump()
    {
        CheckIfJumpKeyPressed();

        if (pressJumpKey && timer <= maxTime)
        {
            Jump();
            
            timer += Time.deltaTime;
            jumpForce += jumpForceIncrement;
        }
        else
        {
            timer = 0f;
            jumpForce = defaultJumpForce;
        }
    }

    void Jump()
    {
        if (canJump)
        {
            playerRB.velocity = jumpForce * Time.fixedDeltaTime * Vector2.up;
        }
    }

    void CheckIfJumpKeyPressed()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            pressJumpKey = true;
        }

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            pressJumpKey = false;
        }
    }

    public void JumpWhenSteppingMonster()
    {
        var force = jumpForceWhenSteppingMonster * Time.fixedDeltaTime * Vector2.up;
        
        steppingMonsterNow = true;
        playerRB.AddForce(force, ForceMode2D.Impulse);
        steppingMonsterNow = false;
    }

    bool IsPlayerOnGround()
    {
        bool isGround = Physics2D.OverlapCircle(feetPos.position, groundCheckRadius, groundLayer);

        return isGround;
    }

    void SetJumpAnimation(bool playerOnGround)
    {
        if (playerOnGround)
        {
            playerAnimController.Stop(PlayerMotion.Jump);
        }
        else
        {
            playerAnimController.Play(PlayerMotion.Jump);
        }
    }

    void Start()
    {
        groundLayer = 1 << LayerMask.NameToLayer("Ground");
        playerAnimController = player.GetPlayerAnimController();
        defaultJumpForce = jumpForce;
    }
}

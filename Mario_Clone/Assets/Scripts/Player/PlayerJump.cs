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
    float jumpIncrement;
    [SerializeField]
    float jumpForce;
    [SerializeField]
    float jumpForceWhenSteppingMonster;
    float timer;
    [SerializeField]
    float maxTime;
    
    bool pressJumpKey;
    bool canJump;
    bool steppingMonsterNow;
    bool isJumping;

    const int MaxJumpCount = 1;
    [SerializeField]
    int jumpCount;

    void CalculateJumpForceAndCheckJump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && jumpCount < MaxJumpCount)
        {
            pressJumpKey = true;
            canJump = true;

            jumpCount++;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            pressJumpKey = false;
        }

        if (timer > maxTime || !pressJumpKey)
        {
            timer = 0f;
            jumpForce = defaultJumpForce;
            canJump = false;

            pressJumpKey = false;
        }
        else
        {
            timer += Time.deltaTime;
            jumpForce += jumpIncrement;
        }
    }

    public void CheckIsCanJumpAndActiveTriggers()
    {
        if (steppingMonsterNow)
        {
            canJump = false;
            return;
        }
        
        var playerOnGround = IsPlayerOnGround();
        if (!playerOnGround) return;
        
        CalculateJumpForceAndCheckJump();
        
        player.SetActiveInteractionTriggers(!playerOnGround);
        SetJumpAnimation(playerOnGround);
    }

    public void Jump()
    {
        if (canJump)
        {
            playerRB.velocity = jumpForce * Time.fixedDeltaTime * Vector2.up;
        }
    }

    public void JumpWhenSteppingMonster()
    {
        var force = jumpForceWhenSteppingMonster * Time.fixedDeltaTime * Vector2.up;
        
        steppingMonsterNow = true;
        Jump(force, ForceMode2D.Impulse);
        steppingMonsterNow = false;
    }

    bool IsPlayerOnGround()
    {
        bool isGround = Physics2D.OverlapCircle(feetPos.position, groundCheckRadius, groundLayer);

        if (isGround)
        {
            jumpCount = 0;
            return true;
        }
        
        return false;
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

    void Jump(Vector2 force, ForceMode2D mode = ForceMode2D.Force)
    {
        playerRB.AddForce(force, mode);
    }

    void Start()
    {
        groundLayer = 1 << LayerMask.NameToLayer("Ground");
        playerAnimController = player.GetPlayerAnimController();
        defaultJumpForce = jumpForce;
    }
}

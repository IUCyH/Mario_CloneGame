using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
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
    float getAxisJumpKey;

    [SerializeField]
    bool pressJumpKey;
    [SerializeField]
    bool canJump;
    [SerializeField]
    bool isJumping;
    bool steppingMonsterNow;
    bool playerOnGround;

    int groundLayer;

    public void CheckJump()
    {
        if (steppingMonsterNow)
        {
            canJump = false;
            return;
        }

        playerOnGround = IsPlayerOnGround();

        canJump = playerOnGround;
        CheckIfJumpKeyPressed();
    }

    public void Jump()
    {
        CalculateJumpForceAndJump();

        SetJumpAnimation(playerOnGround);
    }

    void CalculateJumpForceAndJump()
    {
        CheckIsJumping();

        if (pressJumpKey && timer <= maxTime)
        {
            playerRB.velocity = jumpForce * Time.fixedDeltaTime * Vector2.up;
            
            timer += Time.deltaTime;
            jumpForce += jumpForceIncrement;
        }
        else
        {
            pressJumpKey = false;

            timer = 0f;
            jumpForce = defaultJumpForce;
        }
    }

    void CheckIfJumpKeyPressed()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && canJump)
        {
            pressJumpKey = true;
            isJumping = true;
        }

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            pressJumpKey = false;
        }
    }

    void CheckIsJumping()
    {
        if (canJump && isJumping)
        {
            isJumping = false;
        }
        else if (!canJump && pressJumpKey)
        {
            isJumping = true;
        }
    }

    public void JumpWhenSteppingMonster()
    {
        var force = jumpForceWhenSteppingMonster * Time.fixedDeltaTime * Vector2.up;

        playerRB.velocity = Vector3.zero;
        
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
            if (isJumping)
            {
                playerAnimController.Play(PlayerMotion.Jump);
            }
        }
    }

    void Start()
    {
        groundLayer = 1 << LayerMask.NameToLayer("Ground");
        playerAnimController = player.GetPlayerAnimController();
        defaultJumpForce = jumpForce;
    }
}

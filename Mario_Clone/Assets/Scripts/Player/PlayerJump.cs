using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField]
    PlayerController player;

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

    public void ReleaseJump()
    {
        pressJumpKey = false;
    }

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

        SetJumpAnimation();
    }

    void CalculateJumpForceAndJump()
    {
        if (pressJumpKey && timer <= maxTime)
        {
            playerRB.velocity = jumpForce * Time.fixedDeltaTime * Vector2.up;
            
            timer += Time.deltaTime;
            jumpForce += jumpForceIncrement;
        }
        else
        {
            if (playerOnGround) isJumping = false;
            
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

    void SetJumpAnimation()
    {
        if (isJumping)
        {
            player.PlayAnimation(PlayerMotion.Jump);
        }

        else
        {
            player.StopAnimation(PlayerMotion.Jump);
        }
    }

    void Start()
    {
        groundLayer = 1 << LayerMask.NameToLayer("Ground");
        defaultJumpForce = jumpForce;
    }
}

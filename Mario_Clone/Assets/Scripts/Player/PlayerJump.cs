using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    int groundLayer;

    [SerializeField]
    PlayerController player;
    [SerializeField]
    Rigidbody2D playerRB;
    [SerializeField]
    Collider2D playerCollider;
    [SerializeField]
    float boxCastDistance;
    [SerializeField]
    float jumpForce;
    [SerializeField]
    float jumpForceWhenSteppingMonster;
    
    bool canJump;
    bool steppingMonsterNow;

    public void CheckIsCanJumpAndActiveTriggers()
    {
        if (steppingMonsterNow)
        {
            canJump = false;
            return;
        }
        
        var jumpKeyDown = Input.GetKeyDown(KeyCode.UpArrow);
        var playerOnGround = IsPlayerOnGround();

        if (jumpKeyDown && playerOnGround)
        {
            canJump = true;
        }
        
        player.SetActiveInteractionTriggers(!playerOnGround);
    }

    public void Jump()
    {
        if (canJump)
        {
            var force = jumpForce * Time.fixedDeltaTime * Vector2.up;

            canJump = false;
            Jump(force, ForceMode2D.Impulse);
        }
    }

    public void JumpWhenSteppingMonster()
    {
        var force = jumpForceWhenSteppingMonster * Time.fixedDeltaTime * Vector2.up;

        ResetAddForce();
        
        steppingMonsterNow = true;
        Jump(force, ForceMode2D.Impulse);
        steppingMonsterNow = false;
    }
    
    bool IsPlayerOnGround()
    {
        var raycast = Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0f, Vector2.down, boxCastDistance, groundLayer);

        if (!ReferenceEquals(raycast.collider, null))
        {
            return true;
        }

        return false;
    }

    void Jump(Vector2 force, ForceMode2D mode = ForceMode2D.Force)
    {
        playerRB.AddForce(force, mode);
    }

    void ResetAddForce()
    {
        playerRB.velocity = Vector3.zero;
    }

    void Start()
    {
        groundLayer = 1 << LayerMask.NameToLayer("Ground");
    }
}

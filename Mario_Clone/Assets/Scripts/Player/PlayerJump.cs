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
            canJump = false;
            playerRB.AddForce(jumpForce * Time.fixedDeltaTime * Vector2.up, ForceMode2D.Impulse);
        }
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

    void Start()
    {
        groundLayer = 1 << LayerMask.NameToLayer("Ground");
    }
}

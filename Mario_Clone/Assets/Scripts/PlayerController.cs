using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 nextPlayerPos;
    
    [SerializeField]
    Animator playerAnimator;
    [SerializeField]
    GameObject player;
    [SerializeField]
    Rigidbody2D playerRB;
    [SerializeField]
    Collider2D playerCollider;

    [SerializeField]
    string groundLayerName = "Ground";
    [SerializeField]
    float speed;
    [SerializeField]
    float jumpForce;
    [SerializeField]
    float boxCastDistance = 0.02f;
    [SerializeField]
    bool isJumping;
    [SerializeField]
    bool jump;

    void Jump()
    {
        var isJumpKeyDown = Input.GetKeyDown(KeyCode.UpArrow);

        if (isJumpKeyDown && !isJumping)
        {
            jump = true;
        }
    }

    void Move()
    {
        var dir = Input.GetAxis("Horizontal");
        nextPlayerPos.x = dir * speed * Time.deltaTime;
        
        transform.position += nextPlayerPos;

        SetPlayerRotation(dir);
        SetMoveAnimation(dir);
    }

    void SetMoveAnimation(float dir)
    {
        if (dir != 0)
        {
            playerAnimator.SetBool("IsMove", true);
        }
        else
        {
            playerAnimator.SetBool("IsMove", false);
        }
    }

    void SetPlayerRotation(float dir)
    {
        if (dir > 0)
        {
            player.transform.rotation = Quaternion.identity;
        }
        else if (dir < 0)
        {
            player.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }

    bool IsPlayerOnGround()
    {
        var raycast = Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0f, Vector2.down, boxCastDistance, 1 << LayerMask.NameToLayer(groundLayerName));

        if (raycast.collider != null)
        {
            return true;
        }

        return false;
    }
    
    void Start()
    {
        nextPlayerPos = Vector3.zero;
    }

    void Update()
    {
        Move();
        Jump();

        if (IsPlayerOnGround() && isJumping)
        {
            isJumping = false;
            playerAnimator.SetBool("IsJump", false);
        }
        else if(!IsPlayerOnGround() && !isJumping)
        {
            isJumping = true;
            playerAnimator.SetBool("IsJump", true);
        }
    }

    void FixedUpdate()
    {
        if (jump)
        {
            jump = false;
            playerRB.AddForce(jumpForce * Time.fixedDeltaTime * Vector2.up, ForceMode2D.Impulse);
        }
    }
}

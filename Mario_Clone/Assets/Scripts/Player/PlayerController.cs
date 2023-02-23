using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 nextPlayerPos;
    
    [SerializeField]
    InteractionTrigger[] interactionTriggers;
    [SerializeField]
    MovementLimit playerMovementLimit;
    [SerializeField]
    Animator playerAnimator;
    [SerializeField]
    Transform player;
    [SerializeField]
    Transform mario;
    [SerializeField]
    Rigidbody2D playerRB;
    [SerializeField]
    Collider2D playerCollider;
    
    int groundLayer;
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
    bool isPressedJumpKey;

    void Jump()
    {
        var isJumpKeyDown = Input.GetKeyDown(KeyCode.UpArrow);

        if (isJumpKeyDown && !isJumping)
        {
            jump = true;
            isPressedJumpKey = true;

            SetActiveInteractionTriggers(true);
            playerAnimator.SetBool("IsJump", true);
        }
    }

    void Move()
    {
        var dir = Input.GetAxis("Horizontal");
        nextPlayerPos.x = dir * speed * Time.deltaTime;

        if (!playerMovementLimit.IsPlayerCannotMove(dir))
        {
            player.position += nextPlayerPos;
        }

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
            mario.rotation = Quaternion.identity;
        }
        else if (dir < 0)
        {
            mario.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }

    bool CheckIsJumping()
    {
        if (!IsPlayerOnGround() && isPressedJumpKey)
        {
            return true;
        }

        return false;
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

    void SetActiveInteractionTriggers(bool value)
    {
        foreach (var trigger in interactionTriggers)
        {
            trigger.SetActive(value);
        }
    }
    void Start()
    {
        interactionTriggers = GetComponentsInChildren<InteractionTrigger>();
        nextPlayerPos = Vector3.zero;
        groundLayer = 1 << LayerMask.NameToLayer("Ground");
        SetActiveInteractionTriggers(false);
    }

    void Update()
    {
        bool jumpingNow = CheckIsJumping();
        
        if (!jumpingNow && isJumping)
        {
            isJumping = false;
            isPressedJumpKey = false;
            
            SetActiveInteractionTriggers(false);
            playerAnimator.SetBool("IsJump", false);
        }
        else if (jumpingNow && !isJumping)
        {
            isJumping = true;
        }
        
        Jump();
    }

    void FixedUpdate()
    {
        if (jump)
        {
            jump = false;
            playerRB.AddForce(jumpForce * Time.fixedDeltaTime * Vector2.up, ForceMode2D.Impulse);
        }
        
        Move();
    }
}

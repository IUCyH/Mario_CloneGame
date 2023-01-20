using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 nextPlayerPos;

    [SerializeField]
    PlayerIntoMap playerIntoMap;
    [SerializeField]
    Animator playerAnimator;
    [SerializeField]
    GameObject playerCharacter;
    [SerializeField]
    Rigidbody2D playerRB;
    [SerializeField]
    Collider2D playerCollider;

    [SerializeField]
    string groundLayerName = "Ground";
    [SerializeField]
    float waitCntForPositionPlayer;
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

    IEnumerator Coroutine_PositionPlayerIntoMap()
    {
        while (true)
        {
            for (int i = 0; i < waitCntForPositionPlayer; i++)
            {
                yield return null;
            }

            playerIntoMap.PositionPlayerIntoMap();
        }
    }
    
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
            playerCharacter.transform.rotation = Quaternion.identity;
        }
        else if (dir < 0)
        {
            playerCharacter.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
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
        StartCoroutine(Coroutine_PositionPlayerIntoMap());
    }

    void Update()
    {
        if (isJumping && IsPlayerOnGround())
        {
            isJumping = false;
            playerAnimator.SetBool("IsJump", false);
        }
        else if (!isJumping && !IsPlayerOnGround())
        {
            isJumping = true;
            playerAnimator.SetBool("IsJump", true);

        }
        
        Move();
        Jump();
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

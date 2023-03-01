using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 nextPlayerPos;

    [SerializeField]
    PlayerJump playerJump;
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
    
    int jumpCount;
    [SerializeField]
    float speed;
    bool isPressedJumpKey;
    
    public void SetActiveInteractionTriggers(bool value)
    {
        foreach (var trigger in interactionTriggers)
        {
            trigger.SetActive(value);
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
    
    void Start()
    {
        interactionTriggers = GetComponentsInChildren<InteractionTrigger>();
        nextPlayerPos = Vector3.zero;
        
        SetActiveInteractionTriggers(false);
    }

    void Update()
    {
        playerJump.CheckIsCanJumpAndActiveTriggers();
    }

    void FixedUpdate()
    {
        playerJump.Jump();
        Move();
    }
}

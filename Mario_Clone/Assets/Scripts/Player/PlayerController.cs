using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    [SerializeField]
    PlayerMove playerMove;
    [SerializeField]
    PlayerJump playerJump;

    [SerializeField]
    InteractionTrigger[] interactionTriggers;
    [SerializeField]
    MovementLimit playerMovementLimit;

    int jumpCount;
    bool isPressedJumpKey;

    public void JumpWhenSteppingMonster()
    {
        playerJump.JumpWhenSteppingMonster();
    }
    
    public void SetActiveInteractionTriggers(bool value)
    {
        foreach (var trigger in interactionTriggers)
        {
            trigger.SetActive(value);
        }
    }

    public bool IsPlayerCanMove(float dir)
    {
        return !playerMovementLimit.IsPlayerCannotMove(dir);
    }
    
    void Start()
    {
        interactionTriggers = GetComponentsInChildren<InteractionTrigger>();

        SetActiveInteractionTriggers(false);
    }

    void Update()
    {
        playerJump.CheckIsCanJumpAndActiveTriggers();
    }

    void FixedUpdate()
    {
        playerJump.Jump();
        playerMove.Move();
    }
}

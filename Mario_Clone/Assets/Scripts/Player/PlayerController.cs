using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerAnimation playerAnimController;
    
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

    public void SetDie()
    {
        Time.timeScale = 0f;
        Debug.Log("Im die..");
    }
    
    public PlayerAnimation GetPlayerAnimController()
    {
        return playerAnimController;
    }
    
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
        playerAnimController = new PlayerAnimation(GetComponent<Animator>());
        
        SetActiveInteractionTriggers(false);
    }

    void Update()
    {
        playerJump.CheckIsCanJumpAndActiveTriggers();

        if (Input.GetKeyDown(KeyCode.P))
            Time.timeScale = 1f;
    }

    void FixedUpdate()
    {
        playerMove.Move();
        playerJump.Jump();
    }
}

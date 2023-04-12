using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Vector3 nextPlayerPos;

    [SerializeField]
    PlayerController player;

    [SerializeField]
    Transform playerTransform;
    [SerializeField]
    Transform mario;

    [SerializeField]
    float runSpeed;
    [SerializeField]
    float walkSpeed;
    float speed;

    public void Move()
    {
        var dir = Input.GetAxisRaw("Horizontal");
        nextPlayerPos.x = dir * speed * Time.deltaTime;

        if (player.IsPlayerCanMove(dir))
        {
            playerTransform.Translate(nextPlayerPos);
        }

        SetPlayerRotation(dir);
        SetMoveAnimation(dir);
    }

    public void SetMoveSpeed()
    {
        var running = CheckIsRunKeyPressed();

        speed = running ? runSpeed : walkSpeed; 
    }

    bool CheckIsRunKeyPressed()
    {
        var runKeyPressed = Input.GetAxisRaw("Run");
        
        return runKeyPressed > 0;
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
    
    void SetMoveAnimation(float dir)
    {
        var animSpeed = speed - walkSpeed; //현재 속도에서 기본속도를 뺌
        
        if (dir != 0)
        {
            player.PlayAnimation(PlayerMotion.Walk);
        }
        else
        {
            player.StopAnimation(PlayerMotion.Walk);
        }
        
        player.SetAnimationSpeed(PlayerAnimSpeedParams.MoveSpeed, animSpeed);
    }
    
    void Start()
    {
        nextPlayerPos = Vector3.zero;
        playerTransform = transform;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Vector3 nextPlayerPos;

    [SerializeField]
    PlayerController player;
    PlayerAnimation playerAnimController;
    
    [SerializeField]
    Transform playerTransform;
    [SerializeField]
    Transform mario;
    
    [SerializeField]
    float speed;
    
    public void Move()
    {
        var dir = Input.GetAxis("Horizontal");
        nextPlayerPos.x = dir * speed * Time.deltaTime;

        if (player.IsPlayerCanMove(dir))
        {
            playerTransform.position += nextPlayerPos;
        }

        SetPlayerRotation(dir);
        SetMoveAnimation(dir);
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
        if (dir != 0)
        {
            playerAnimController.Play(PlayerMotion.Move);
        }
        else
        {
            playerAnimController.Stop(PlayerMotion.Move);
        }
    }
    
    void Start()
    {
        nextPlayerPos = Vector3.zero;
        playerTransform = transform;
        playerAnimController = player.GetPlayerAnimController();
    }
}

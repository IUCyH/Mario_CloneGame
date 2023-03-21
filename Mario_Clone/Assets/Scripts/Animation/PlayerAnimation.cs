using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public enum PlayerMotion
{
    None = -1,
    Idle,
    Jump,
    Walk,
}

public enum PlayerAnimSpeedParams
{
    MoveSpeed
}

public class PlayerAnimation : AnimationManagement
{
    StringBuilder stringBuilder = new StringBuilder();
    Dictionary<PlayerMotion, int> playerAnims = new Dictionary<PlayerMotion, int>();
    Dictionary<PlayerAnimSpeedParams, int> playerAnimSpeedParams = new Dictionary<PlayerAnimSpeedParams, int>();

    public PlayerAnimation(Animator animator)
    {
        base.animator = animator;
        SetAnimIdsFromAnimStates();
    }

    public void Play(PlayerMotion motion)
    {
        base.PlayWithBoolean(playerAnims[motion]);
    }

    public void Stop(PlayerMotion motion)
    {
        base.StopWithBoolean(playerAnims[motion]);
    }
    
    public void SetAnimationSpeed(PlayerAnimSpeedParams animSpeedParam, float speed = 1f)
    {
        base.SetAnimationSpeed(playerAnimSpeedParams[animSpeedParam], speed);
    }

    protected override void SetAnimIdsFromAnimStates()
    {
        foreach (PlayerMotion state in Enum.GetValues(typeof(PlayerMotion)))
        {
            if(state == PlayerMotion.None) continue;

            stringBuilder.Append(state);
            
            var id = Animator.StringToHash(stringBuilder.ToString());
            playerAnims.Add(state, id);
            
            stringBuilder.Clear();
        }

        foreach (PlayerAnimSpeedParams animSpeed in Enum.GetValues(typeof(PlayerAnimSpeedParams)))
        {
            stringBuilder.Append(animSpeed);
            
            var id = Animator.StringToHash(stringBuilder.ToString());
            playerAnimSpeedParams.Add(animSpeed, id);
            
            stringBuilder.Clear();
        }
    }
}

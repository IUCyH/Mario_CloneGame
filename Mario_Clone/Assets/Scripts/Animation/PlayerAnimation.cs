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

public class PlayerAnimation : AnimationManagement
{
    StringBuilder stringBuilder = new StringBuilder();
    Dictionary<PlayerMotion, int> playerAnims = new Dictionary<PlayerMotion, int>();

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
    }
}

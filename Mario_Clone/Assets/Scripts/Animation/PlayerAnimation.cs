using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public enum PlayerMotion
{
    None = -1,
    Idle,
    Jump,
    Move
}

public class PlayerAnimation : AnimationManagement
{
    StringBuilder stringBuilder = new StringBuilder();

    public PlayerAnimation(Animator animator)
    {
        base.animator = animator;
    }
    
    public void Play(PlayerMotion motion, bool isBlend = true)
    {
        stringBuilder.Append(motion);
        base.PlayWithBoolean(stringBuilder.ToString(), isBlend);
        stringBuilder.Clear();
    }

    public void Stop(PlayerMotion motion)
    {
        stringBuilder.Append(motion);
        base.StopWithBoolean(stringBuilder.ToString());
        stringBuilder.Clear();
    }
}

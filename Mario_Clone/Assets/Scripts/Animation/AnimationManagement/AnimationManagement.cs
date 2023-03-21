using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimationManagement
{
    protected Animator animator;

    protected abstract void SetAnimIdsFromAnimStates();

    protected void PlayWithBoolean(int animId)
    {
        animator.SetBool(animId, true);
    }

    protected void StopWithBoolean(int animId)
    {
        animator.SetBool(animId, false);
    }

    protected void SetAnimationSpeed(int animSpeedParamId, float speed = 1f)
    {
        if (Mathf.Approximately(speed, 0f)) speed = 1f;
        
        animator.SetFloat(animSpeedParamId, speed);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimationManagement
{
    protected Animator animator;

    protected void PlayWithBoolean(string animName, bool isBlend)
    {
        if (isBlend)
        {
            animator.SetBool(animName, true);
        }
        else
        {
            animator.Play(animName, 0, 0f);
        }
    }

    protected void StopWithBoolean(string animName)
    {
        animator.SetBool(animName, false);
    }
}

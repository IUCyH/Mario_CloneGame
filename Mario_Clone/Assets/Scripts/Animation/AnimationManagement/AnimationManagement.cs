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
}

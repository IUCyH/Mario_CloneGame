using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UIElements;
using Random = System.Random;

public interface Item
{
    void PlayShowAnimation(Vector3 targetPos);
    void AddEffectToPlayer();
    void OnStart();
}

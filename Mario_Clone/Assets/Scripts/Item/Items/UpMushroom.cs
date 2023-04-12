using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpMushroom : Item
{
    protected override void PlayShowAnimation(Vector3 targetPos)
    {
        Debug.Log("It's UpMushroom!");
    }
}

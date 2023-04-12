using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMushroom : Item
{
    protected override void PlayShowAnimation(Vector3 targetPos)
    {
        Debug.Log("It's MagicMushroom!");
    }
}

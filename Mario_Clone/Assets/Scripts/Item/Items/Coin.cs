using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item
{
    protected override void PlayShowAnimation()
    {
        Debug.Log("It's Coin!");
    }
}
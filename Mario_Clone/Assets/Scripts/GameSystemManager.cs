using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystemManager : SingletonMonoBehaviour<GameSystemManager>
{
    protected override void OnStart()
    {
        UIManager.Instance.UpdateCoinText(DataManager.Instance.Coin);
    }
}

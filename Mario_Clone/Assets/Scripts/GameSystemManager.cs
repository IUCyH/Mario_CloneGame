using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystemManager : SingletonMonoBehaviour<GameSystemManager>
{
    [SerializeField]
    CameraController cameraController;
    [SerializeField]
    short gameLimitTime = 400;

    IEnumerator Coroutine_Timer()
    {
        var waitForSecond = new WaitForSeconds(1);

        while (true)
        {
            gameLimitTime -= 1;
            UIManager.Instance.UpdateTimerText(gameLimitTime);

            yield return waitForSecond;
        }
    }

    public bool IsInsideTheCamera(Transform obj)
    {
        return cameraController.IsInsideTheCamera(obj);
    }

    protected override void OnStart()
    {
        StartCoroutine(Coroutine_Timer());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TGameManager : TSingleton<TGameManager>
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
        InitSetting();
    }

    private void InitSetting()
    {
        Application.targetFrameRate = 144;
    }
}

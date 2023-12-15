using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPhysicsManager : TSingleton<TPhysicsManager>
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
}

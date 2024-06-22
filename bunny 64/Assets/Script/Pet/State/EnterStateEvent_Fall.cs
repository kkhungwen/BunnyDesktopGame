using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[DisallowMultipleComponent]
public class EnterStateEvent_Fall : MonoBehaviour
{
    public event Action<EnterStateEvent_Fall> OnEnterState_Fall;

    public void CallOnEnterStateFall()
    {
        OnEnterState_Fall?.Invoke(this);
    }
}

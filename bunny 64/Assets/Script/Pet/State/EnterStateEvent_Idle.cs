using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[DisallowMultipleComponent]
public class EnterStateEvent_Idle : MonoBehaviour
{
    public event Action<EnterStateEvent_Idle> OnEnterState_Idle;

    public void CallOnEnterStateIdle()
    {
        OnEnterState_Idle?.Invoke(this);
    }
}

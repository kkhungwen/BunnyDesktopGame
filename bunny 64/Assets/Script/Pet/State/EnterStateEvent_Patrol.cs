using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[DisallowMultipleComponent]
public class EnterStateEvent_Patrol : MonoBehaviour
{
    public event Action<EnterStateEvent_Patrol> OnEnterState_Patrol;

    public void CallOnEnterStatePatrol()
    {
        OnEnterState_Patrol?.Invoke(this);
    }
}

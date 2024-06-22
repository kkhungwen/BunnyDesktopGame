using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[DisallowMultipleComponent]
public class EnterStateEvent_Drag : MonoBehaviour
{
    public event Action<EnterStateEvent_Drag> OnEnterState_Drag;

    public void CallOnEnterStateDrag()
    {
        OnEnterState_Drag?.Invoke(this);
    }
}

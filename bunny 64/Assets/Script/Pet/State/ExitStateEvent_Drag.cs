using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[DisallowMultipleComponent]
public class ExitStateEvent_Drag : MonoBehaviour
{
    public event Action<ExitStateEvent_Drag> OnExitState_Drag;

    public void CallOnExitStateDrag()
    {
        OnExitState_Drag?.Invoke(this);
    }
}

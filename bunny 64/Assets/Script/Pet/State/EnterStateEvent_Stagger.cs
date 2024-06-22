using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[DisallowMultipleComponent]
public class EnterStateEvent_Stagger : MonoBehaviour
{
    public event Action<EnterStateEvent_Stagger> OnEnterStateStagger;

    public void CallOnEnterStateStagger()
    {
        OnEnterStateStagger?.Invoke(this);
    }
}

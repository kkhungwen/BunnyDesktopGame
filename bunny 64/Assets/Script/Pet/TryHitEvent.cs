using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[DisallowMultipleComponent]
public class TryHitEvent : MonoBehaviour
{
    public event Action<TryHitEvent, TryHitEventArgs> OnTryHit;

    public void CallTryHit(Vector3 worldPosition)
    {
        OnTryHit?.Invoke(this, new TryHitEventArgs { worldPosition = worldPosition });
    }
}

public class TryHitEventArgs : EventArgs
{
    public Vector3 worldPosition;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[DisallowMultipleComponent]
public class GetHitEvent : MonoBehaviour
{
    public event Action<GetHitEvent, GetHitEventArgs> OnGetHit;

    public void CallGetHit(Vector3 worldPosition, bool isCritical, int damage)
    {
        OnGetHit?.Invoke(this, new GetHitEventArgs { worldPosition = worldPosition, isCritical = isCritical, damage = damage });
    }
}

public class GetHitEventArgs : EventArgs
{
    public Vector3 worldPosition;

    public bool isCritical;

    public int damage;
}

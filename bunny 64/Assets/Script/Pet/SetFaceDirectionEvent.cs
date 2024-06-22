using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[DisallowMultipleComponent]
public class SetFaceDirectionEvent : MonoBehaviour
{
    public event Action<SetFaceDirectionEventArgs> OnSetFaceDirection;

    public void CallOnSetFaceDirection(Vector2 direction)
    {
        OnSetFaceDirection?.Invoke(new SetFaceDirectionEventArgs { direction = direction});
    }
}

public class SetFaceDirectionEventArgs : EventArgs
{
    public Vector2 direction;
}

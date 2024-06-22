using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[DisallowMultipleComponent]
public class EnterStateEvent_Eat : MonoBehaviour
{
    public event Action<EnterStateEvent_Eat, SpriteAnimationSO> OnEnterState_Eat;

    public void CallOnEnterStateEat(SpriteAnimationSO getEatAnimation)
    {
        OnEnterState_Eat?.Invoke(this, getEatAnimation);
    }
}

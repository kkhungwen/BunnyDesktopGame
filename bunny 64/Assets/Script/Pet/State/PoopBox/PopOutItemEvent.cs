using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PopOutItemEvent : MonoBehaviour
{
    public event Action<bool> OnPopOutItem;

    public void CallPopOutItem(bool isBunny)
    {
        OnPopOutItem?.Invoke(isBunny);
    }
}

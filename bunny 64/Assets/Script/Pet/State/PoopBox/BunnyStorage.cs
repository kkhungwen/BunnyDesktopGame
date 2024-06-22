using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyStorage : MonoBehaviour
{
    public bool isBunny = true;
    [SerializeField] DragOnObject dragOnObject;

    private void Awake()
    {
        dragOnObject.OnDropOn += DragOnObject_OnDropOn;
    }

    private void DragOnObject_OnDropOn(bool isBunny)
    {
        if (isBunny)
            this.isBunny = true;
    }
}

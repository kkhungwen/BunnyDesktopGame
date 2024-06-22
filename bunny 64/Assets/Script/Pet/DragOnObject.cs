using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(BoxCollider2D))]
public class DragOnObject : MonoBehaviour
{
    public event Action<bool> OnDropOn;
    [SerializeField]BunnyStorage bunnyStorage;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Material dragOnMaterial;
    private Material originalMaterial;


    private void Awake()
    {
        originalMaterial = spriteRenderer.material;
    }

    public void DragOn()
    {
        spriteRenderer.material = dragOnMaterial;
    }

    public void DragOut()
    {
        spriteRenderer.material = originalMaterial;
    }

    public void DropOn(bool isBunny)
    {
        spriteRenderer.material = originalMaterial;

        if (bunnyStorage.isBunny)
            return;

        OnDropOn?.Invoke(isBunny);
    }
}

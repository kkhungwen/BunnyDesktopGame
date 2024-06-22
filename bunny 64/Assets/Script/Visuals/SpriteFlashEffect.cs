using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteFlashEffect : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    bool isFlash = false;

    // Flash time
    [SerializeField] private float flashTime;
    private float flashTimeCount;

    // Materials
    [SerializeField] private Material flashMaterial;
    private Material originalMaterial;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!isFlash)
            return;

        flashTimeCount += Time.deltaTime;

        if (flashTimeCount >= flashTime)
        {
            EndFlash();
        }
    }

    public void Flash()
    {
        if (isFlash)
            return;

        originalMaterial = spriteRenderer.material;
        spriteRenderer.material = flashMaterial;

        flashTimeCount = 0;

        isFlash = true;
    }

    private void EndFlash()
    {
        spriteRenderer.material = originalMaterial;

        isFlash = false;
    }
}

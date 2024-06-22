using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[DisallowMultipleComponent]
[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAnimator : MonoBehaviour
{
    public event Action OnAnimationFinished;

    private SpriteRenderer spriteRenderer;
    private SpriteAnimationSO spriteAnimationSO;

    private bool isPlaying = false;
    private int frameCount = 0;
    private float timeCount = 0;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // if playing animation
        if (!isPlaying)
            return;

        timeCount += Time.deltaTime;

        // if frame finish playing
        if (timeCount < spriteAnimationSO.secPerFrame)
            return;

        // when finish one animation loop check if continue looping
        if (frameCount >= spriteAnimationSO.frameArray.Length)
        {
            if (spriteAnimationSO.isLoop)
            {
                frameCount = 0;
            }
            else if (!spriteAnimationSO.isLoop)
            {
                FinishPlaying();
                return;
            }
        }

        spriteRenderer.sprite = spriteAnimationSO.frameArray[frameCount];

        frameCount++;
        timeCount = 0;
    }

    public void PlayAnimation(SpriteAnimationSO spriteAnimationSO)
    {
        this.spriteAnimationSO = spriteAnimationSO;

        frameCount = 0;
        timeCount = spriteAnimationSO.secPerFrame;

        isPlaying = true;
    }

    public void FinishPlaying()
    {
        isPlaying = false;

        OnAnimationFinished?.Invoke();
    }
}

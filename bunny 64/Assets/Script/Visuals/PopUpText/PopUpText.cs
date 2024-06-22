using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[RequireComponent(typeof(TextMeshPro))]
[DisallowMultipleComponent]
public class PopUpText : MonoBehaviour
{
    public event Action OnFinishPop;

    private TextMeshPro textMeshPro;
    private float textSize = 0;
    private float lifeTime = 0;
    private float timeCount = 0;
    bool isPop = false;

    [Space(10f)]
    [Header("TEXT SIZE CURVE")]
    [SerializeField] private AnimationCurve textSizeCurve;

    [Space(10f)]
    [Header("MOVE UP EFFECT")]
    [SerializeField] private float moveUpStartTime = 0;
    [SerializeField] private float moveUpSpeed = 1f;

    [Space(10f)]
    [Header("FADE EFFECT")]
    [SerializeField] private float fadeStartTime = 0;
    [SerializeField] private float fadeSpeed = 1f;



    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshPro>();

        textMeshPro.text = "";
    }

    private void Update()
    {
        if (!isPop)
            return;

        timeCount += Time.deltaTime;

        ChangeTextSizeByCurve();

        MoveUpEffect();

        Fade();

        if (timeCount >= lifeTime)
            FinishPop();
    }

    public void PopText(Vector3 position, float lifeTime, string text, float textSize, Color color)
    {
        transform.position = position;

        this.lifeTime = lifeTime;

        textMeshPro.text = text;

        this.textSize = textSize;

        textMeshPro.color = color;

        timeCount = 0;

        ChangeTextSizeByCurve();

        isPop = true;
    }

    private void ChangeTextSizeByCurve()
    {
        textMeshPro.fontSize = textSize * textSizeCurve.Evaluate(timeCount / lifeTime);
    }

    private void MoveUpEffect()
    {
        if (timeCount >= moveUpStartTime)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + moveUpSpeed * Time.deltaTime, 0);
        }
    }

    private void Fade()
    {
        if (timeCount >= fadeStartTime)
        {
            Color fadeColor = textMeshPro.color;
            fadeColor.a -= Time.deltaTime * fadeSpeed;

            textMeshPro.color = fadeColor;
        }
    }

    private void FinishPop()
    {
        textMeshPro.text = "";
        timeCount = 0;
        isPop = false;

        OnFinishPop?.Invoke();
    }
}

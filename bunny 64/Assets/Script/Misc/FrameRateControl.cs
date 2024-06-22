using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRateControl : MonoBehaviour
{
    [SerializeField] int defaultFrameRate = 30;
    [SerializeField] int unfocusFrameRate = 60;
    [SerializeField] int focusFrameRate = 60;

    private void Awake()
    {
        Application.targetFrameRate = defaultFrameRate;
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
            Application.targetFrameRate = focusFrameRate;
        else
            Application.targetFrameRate = unfocusFrameRate;
    }
}

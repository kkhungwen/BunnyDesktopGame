using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

[RequireComponent(typeof(PixelPerfectCamera))]
[DisallowMultipleComponent]
public class PixelPerfectCameraScaler : MonoBehaviour
{
    private PixelPerfectCamera pixelPerfectCamera;

    private void Awake()
    {
        pixelPerfectCamera = GetComponent<PixelPerfectCamera>();

        float aspectRatio = (float)Screen.height / (float)Screen.width;

        pixelPerfectCamera.refResolutionX = 960;
        pixelPerfectCamera.refResolutionY = (int)(960 * aspectRatio);
    }
}

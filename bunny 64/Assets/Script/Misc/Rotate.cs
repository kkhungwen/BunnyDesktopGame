using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    private bool isRotate = false;
    private Vector3 originalEuler;
    private Vector3 rotateEuler;

    private void Update()
    {
        if (!isRotate)
            return;

        transform.Rotate(rotateEuler, Space.Self);
    }

    private void Awake()
    {
        originalEuler = transform.localEulerAngles;
    }

    private void OnDisable()
    {
        StopRotate();
    }

    public void StartRotate(float rotateSpeed)
    {
        rotateEuler = new Vector3(0, 0, rotateSpeed);

        isRotate = true;
    }

    public void StopRotate()
    {
        transform.localEulerAngles = originalEuler;

        isRotate = false;
    }
}

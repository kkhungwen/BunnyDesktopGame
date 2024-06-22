using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ClickInput : MonoBehaviour
{
    [SerializeField] private float holdThreshHold;
    [SerializeField] private float dragDistanceThreshHold;

    private IClickable currentClickable;
    private Vector3 leftMouseDownPosition;
    private Vector3 targetRelativePosition;
    private Vector3 mouseWorldPosition;
    private bool isLeftMouse;
    private bool isStartLeftMouseDrag;
    private float leftMouseHoldTime;
    private Collider2D[] overlapColliderArray = new Collider2D[3];

    private void Awake()
    {
        leftMouseHoldTime = 0;
        currentClickable = null;
        isLeftMouse = false;
        isStartLeftMouseDrag = false;
    }

    private void Update()
    {
        mouseWorldPosition = HelperUtils.GetMouseWorldPosition();

        CountLeftHoldTime();

        HandleLeftMouseHold();

        if (Input.GetMouseButtonDown(0))
            HandleLeftMouseDown();

        else if (Input.GetMouseButtonUp(0))
            HandleLeftMouseUp();
    }

    private void HandleLeftMouseDown()
    {
        // Reset parameters
        currentClickable = null;
        isStartLeftMouseDrag = false;
        leftMouseHoldTime = 0;
        leftMouseDownPosition = mouseWorldPosition;

        Array.Clear(overlapColliderArray, 0, overlapColliderArray.Length);

        // Try physics overlap collider
        overlapColliderArray = Physics2D.OverlapPointAll(leftMouseDownPosition);

        foreach (Collider2D collider in overlapColliderArray)
        {
            currentClickable = collider.GetComponent<IClickable>();

            if (currentClickable == null)
                continue;

            targetRelativePosition = currentClickable.GetWorldPosition() - leftMouseDownPosition;
            break;
        }

        // Start left mouse down
        isLeftMouse = true;
    }

    private void HandleLeftMouseUp()
    {
        // Call IClickable mouse up function
        if (currentClickable != null)
        {
            if (leftMouseHoldTime < holdThreshHold)
            {
                currentClickable.LeftClick(mouseWorldPosition);
            }

            currentClickable.LeftMouseUp(mouseWorldPosition, mouseWorldPosition + targetRelativePosition);
        }

        // Reset parameters
        isStartLeftMouseDrag = false;
        leftMouseHoldTime = 0;
        currentClickable = null;

        // End left mouse down
        isLeftMouse = false;
    }

    private void HandleLeftMouseHold()
    {
        // If holdong left mouse
        if (!isLeftMouse)
            return;

        // If there is Clickable being held
        if (currentClickable == null)
            return;

        float dragDistance = Vector3.Distance(leftMouseDownPosition, mouseWorldPosition);

        // If held longer then hold threshhold     or     mouse move furthur then drag distance thrashhold
        if (leftMouseHoldTime < holdThreshHold && dragDistance < dragDistanceThreshHold)
            return;

        if (!isStartLeftMouseDrag)
        {
            currentClickable.StartLeftDrag();
            isStartLeftMouseDrag = true;
        }

        currentClickable.LeftDrag(mouseWorldPosition, mouseWorldPosition + targetRelativePosition);
    }


    private void CountLeftHoldTime()
    {
        if (isLeftMouse)
            leftMouseHoldTime += Time.deltaTime;
    }
}

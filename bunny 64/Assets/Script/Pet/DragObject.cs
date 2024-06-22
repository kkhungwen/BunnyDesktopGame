using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[DisallowMultipleComponent]
[RequireComponent(typeof(EnterStateEvent_Drag))]
[RequireComponent(typeof(ExitStateEvent_Drag))]

public class DragObject : MonoBehaviour
{
    private EnterStateEvent_Drag enterStateEvent_Drag;
    private ExitStateEvent_Drag exitStateEvent_Drag;
    [SerializeField] private bool isBunny;
    [SerializeField] private DragOnObject dragOnObject;
    private bool isDrag = false;

    private DragOnObject currentDragOnObject;

    public event Action OnDrop;

    private Collider2D[] overlapColliderArray = new Collider2D[3];

    private void Awake()
    {
        enterStateEvent_Drag = GetComponent<EnterStateEvent_Drag>();
        exitStateEvent_Drag = GetComponent<ExitStateEvent_Drag>();
    }
    private void OnEnable()
    {
        enterStateEvent_Drag.OnEnterState_Drag += EnterStateEvent_Drag_OnEnterState_Drag;
        exitStateEvent_Drag.OnExitState_Drag += ExitStateEvent_Drag_OnExitState_Drag;
    }

    private void OnDisable()
    {
        enterStateEvent_Drag.OnEnterState_Drag -= EnterStateEvent_Drag_OnEnterState_Drag;
        exitStateEvent_Drag.OnExitState_Drag -= ExitStateEvent_Drag_OnExitState_Drag;
    }

    private void Update()
    {
        if (!isDrag)
            return;

        Array.Clear(overlapColliderArray, 0, overlapColliderArray.Length);

        overlapColliderArray = Physics2D.OverlapPointAll(HelperUtils.GetMouseWorldPosition());

        foreach (Collider2D overlapCollider in overlapColliderArray)
        {
            if (overlapCollider == null)
                continue;

            DragOnObject tempDragOnObject = overlapCollider.GetComponent<DragOnObject>();

            if (tempDragOnObject == null)
                continue;

            if (dragOnObject != null)
            {
                if (tempDragOnObject == dragOnObject)
                    continue;
            }

            if (currentDragOnObject != tempDragOnObject)
            {
                // DRAG ON
                if (currentDragOnObject != null)
                    currentDragOnObject.DragOut();

                currentDragOnObject = tempDragOnObject;
                currentDragOnObject.DragOn();
            }
            return;
        }

        if (currentDragOnObject == null)
            return;

        // DRAG OUT
        currentDragOnObject.DragOut();
        currentDragOnObject = null;
    }

    private void EnterStateEvent_Drag_OnEnterState_Drag(EnterStateEvent_Drag obj)
    {
        isDrag = true;
    }

    private void ExitStateEvent_Drag_OnExitState_Drag(ExitStateEvent_Drag obj)
    {
        Array.Clear(overlapColliderArray, 0, overlapColliderArray.Length);

        overlapColliderArray = Physics2D.OverlapPointAll(HelperUtils.GetMouseWorldPosition());

        foreach (Collider2D overlapCollider in overlapColliderArray)
        {
            if (overlapCollider == null)
                continue;

            currentDragOnObject = overlapCollider.GetComponent<DragOnObject>();

            if (currentDragOnObject == null)
                continue;

            if (dragOnObject != null)
            {
                if (currentDragOnObject == dragOnObject)
                    continue;
            }

            // DROP ON
            currentDragOnObject.DropOn(isBunny);
            OnDrop?.Invoke();
            break;
        }

        isDrag = false;
    }
}

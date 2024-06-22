using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(EnterStateEvent_Idle))]
[RequireComponent(typeof(EnterStateEvent_Stagger))]
[RequireComponent(typeof(EnterStateEvent_Drag))]
[RequireComponent(typeof(BunnyStorage))]
[DisallowMultipleComponent]
public class AnimatePet_PoopBox : MonoBehaviour
{
    [SerializeField] SpriteRenderer petSpriteRenderer;


    private Animator anima;

    private EnterStateEvent_Idle enterStateEvent_Idle;
    private EnterStateEvent_Stagger enterStateEvent_Stagger;
    private EnterStateEvent_Drag enterStateEvent_Drag;
    private EnterStateEvent_Fall enterStateEvent_Fall;
    private DragOnObject dragOnObject;
    private BunnyStorage bunnyStorage;

    private void Awake()
    {
        anima = GetComponent<Animator>();

        bunnyStorage = GetComponent<BunnyStorage>();
        dragOnObject = GetComponentInChildren<DragOnObject>();
        enterStateEvent_Idle = GetComponent<EnterStateEvent_Idle>();
        enterStateEvent_Stagger = GetComponent<EnterStateEvent_Stagger>();
        enterStateEvent_Drag = GetComponent<EnterStateEvent_Drag>();
        enterStateEvent_Fall = GetComponent<EnterStateEvent_Fall>();

        dragOnObject.OnDropOn += DragOnObject_OnDropOn;
        enterStateEvent_Idle.OnEnterState_Idle += EnterStateEvent_Idle_OnEnterState_Idle;
        enterStateEvent_Stagger.OnEnterStateStagger += EnterStateEvent_Stagger_OnEnterStateStagger;
        enterStateEvent_Drag.OnEnterState_Drag += EnterStateEvent_Drag_OnEnterState_Drag;
        enterStateEvent_Fall.OnEnterState_Fall += EnterStateEvent_Fall_OnEnterState_Fall;
    }

    private void DragOnObject_OnDropOn(bool isBunny)
    {
        if (isBunny)
            anima.Play(Settings.dropOnIsBunny, 0, 0);
        else
            anima.Play(Settings.dropOn, 0, 0);
    }

    private void EnterStateEvent_Idle_OnEnterState_Idle(EnterStateEvent_Idle enterStateEvent_Idle)
    {
        if (bunnyStorage.isBunny)
            anima.Play(Settings.idleIsBunny);
        else
            anima.Play(Settings.idle);
    }

    private void EnterStateEvent_Stagger_OnEnterStateStagger(EnterStateEvent_Stagger enterStateEvent_Stagger)
    {
        anima.Play(Settings.stagger, 0, 0);
    }

    private void EnterStateEvent_Drag_OnEnterState_Drag(EnterStateEvent_Drag enterStateEvent_Drag)
    {
        //anima.Play(Settings.drag);
    }
    private void EnterStateEvent_Fall_OnEnterState_Fall(EnterStateEvent_Fall enterStateEvent_Fall)
    {
        anima.Play(Settings.fall);
    }

#if UNITY_EDITOR
    #region Validation
    private void OnValidate()
    {
        HelperUtils.ValidateCheckNullValue(this, nameof(petSpriteRenderer), petSpriteRenderer);
    }
    #endregion
#endif
}

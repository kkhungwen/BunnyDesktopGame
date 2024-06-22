using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pet))]
[RequireComponent(typeof(EnterStateEvent_Drag))]
[RequireComponent(typeof(EnterStateEvent_Idle))]
[RequireComponent(typeof(EnterStateEvent_Fall))]
[DisallowMultipleComponent]
public class State_Idle_Fruit : MonoBehaviour, IPetState
{
    private Pet pet;

    // Events
    private EnterStateEvent_Idle enterStateEvent_Idle;
    private EnterStateEvent_Drag enterStateEvent_Drag;
    private EnterStateEvent_Fall enterStateEvent_Fall;

    private void Awake()
    {
        // Cache required components
        pet = GetComponent<Pet>();
        enterStateEvent_Idle = GetComponent<EnterStateEvent_Idle>();
        enterStateEvent_Drag = GetComponent<EnterStateEvent_Drag>();
        enterStateEvent_Fall = GetComponent<EnterStateEvent_Fall>();
    }

    private void OnEnable()
    {
        enterStateEvent_Idle.OnEnterState_Idle += EnterStateEvent_Idle_OnEnterState_Idle;
        enterStateEvent_Fall.OnEnterState_Fall += EnterStateEvent_Fall_OnEnterState_Fall;
        
    }

    private void OnDisable()
    {
        enterStateEvent_Idle.OnEnterState_Idle -= EnterStateEvent_Idle_OnEnterState_Idle;
        enterStateEvent_Fall.OnEnterState_Fall -= EnterStateEvent_Fall_OnEnterState_Fall;
    }

    private void EnterStateEvent_Idle_OnEnterState_Idle(EnterStateEvent_Idle args)
    {
        EnterState();
    }

    private void EnterStateEvent_Fall_OnEnterState_Fall(EnterStateEvent_Fall obj)
    {
        EnterState();
    }

    public void StateUpdate()
    {

    }

    public void LeftClick(Vector3 position)
    {

    }

    public void StartLeftDrag()
    {
        ExitState();

        enterStateEvent_Drag.CallOnEnterStateDrag();
    }

    public void LeftDrag(Vector3 position, Vector3 positionRelative)
    {

    }

    public void LeftMouseUp(Vector3 position, Vector3 positionRelative)
    {

    }

    public void GetHit(Vector3 position, bool isCritical)
    {

    }

    public void Stun()
    {

    }

    public void TryJump(Vector3 targetPosition)
    {

    }

    private void EnterState()
    {
        //Debug.Log("idle");
        pet.SetState(this);
    }

    private void ExitState()
    {

    }
}

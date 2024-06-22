using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pet))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(MovementByVelocityEvent))]
[RequireComponent(typeof(EnterStateEvent_Drag))]
[RequireComponent(typeof(ExitStateEvent_Drag))]
[RequireComponent(typeof(EnterStateEvent_Fall))]
[DisallowMultipleComponent]
public class State_Drag : MonoBehaviour, IPetState
{
    private Pet pet;
    private Rigidbody2D rb;

    // Events
    private MovementByVelocityEvent movementByVelocityEvent;
    private ExitStateEvent_Drag exitStateEvent_Drag;
    private EnterStateEvent_Drag enterStateEvent_Drag;
    private EnterStateEvent_Fall enterStateEvent_Fall;

    // Configurables
    [SerializeField] private float dragStrength;
    [SerializeField] private float throwStrength;
    [SerializeField] private float maxThrowSpeed;
    [SerializeField] private float followMouseSoftRange;

    private float originalGravityScale;



    private void Awake()
    {
        // Cache required components
        pet = GetComponent<Pet>();
        rb = GetComponent<Rigidbody2D>();
        exitStateEvent_Drag = GetComponent<ExitStateEvent_Drag>();
        movementByVelocityEvent = GetComponent<MovementByVelocityEvent>();
        enterStateEvent_Drag = GetComponent<EnterStateEvent_Drag>();
        enterStateEvent_Fall = GetComponent<EnterStateEvent_Fall>();
    }

    private void OnEnable()
    {
        enterStateEvent_Drag.OnEnterState_Drag += EnterStateEvent_Drag_OnEnterState_Drag;
    }
    private void OnDisable()
    {
        enterStateEvent_Drag.OnEnterState_Drag -= EnterStateEvent_Drag_OnEnterState_Drag;
    }

    private void EnterStateEvent_Drag_OnEnterState_Drag(EnterStateEvent_Drag args)
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

    }

    public void LeftDrag(Vector3 position, Vector3 positionRelative)
    {
        MoveToMousePosition(position);
    }

    public void LeftMouseUp(Vector3 position, Vector3 positionRelative)
    {
        Throw(position);

        enterStateEvent_Fall.CallOnEnterStateFall();

        ExitState();
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
        //Debug.Log("drag");

        originalGravityScale = rb.gravityScale;
        rb.gravityScale = 0;

        pet.SetState(this);
    }

    private void ExitState()
    {
        rb.gravityScale = originalGravityScale;
        exitStateEvent_Drag.CallOnExitStateDrag();
    }

    private void MoveToMousePosition(Vector3 mousePosition)
    {
        float mouseDistance = Vector2.Distance(mousePosition, transform.position);
        if (mouseDistance < followMouseSoftRange)
        {
            movementByVelocityEvent.CallMovementByVelocityEvent(Vector3.zero, 0);
            return;
        }

        Vector2 direction = (mousePosition - transform.position).normalized;
        float speed = Vector2.Distance(mousePosition, transform.position) * dragStrength;

        movementByVelocityEvent.CallMovementByVelocityEvent(direction, speed);
    }

    private void Throw(Vector3 mousePosition)
    {
        Vector2 direction = (mousePosition - transform.position).normalized;
        float speed = Vector2.Distance(mousePosition, transform.position) * throwStrength;
        if (speed > maxThrowSpeed) speed = maxThrowSpeed;

        movementByVelocityEvent.CallMovementByVelocityEvent(direction, speed);
    }
}

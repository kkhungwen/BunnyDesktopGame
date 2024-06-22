using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnterStateEvent_Idle))]
[RequireComponent(typeof(EnterStateEvent_Fall))]
[RequireComponent(typeof(DragObject))]
[RequireComponent(typeof(MovementByVelocityEvent))]
[RequireComponent(typeof(GetHitEvent))]
[DisallowMultipleComponent]
public class Pet : MonoBehaviour, IPopOutItem
{
    [HideInInspector] public IPetState currentState;
    public ObjectPoolManager poolManager;
    private GameObject poolKey;
    private DragObject dragObject;
    // Events
    private GetHitEvent getHitEvent;
    private MovementByVelocityEvent movementByVelocityEvent;
    private EnterStateEvent_Idle enterStateEvent_Idle;
    private EnterStateEvent_Fall enterStateEvent_Fall;

    private void Awake()
    {
        // Cache required components
        dragObject = GetComponent<DragObject>();
        getHitEvent = GetComponent<GetHitEvent>();
        movementByVelocityEvent = GetComponent<MovementByVelocityEvent>();
        enterStateEvent_Idle = GetComponent<EnterStateEvent_Idle>();
        enterStateEvent_Fall = GetComponent<EnterStateEvent_Fall>();
    }

    private void OnEnable()
    {
        dragObject.OnDrop += DragObject_OnDrop;
        getHitEvent.OnGetHit += GetHitEvent_OnGetHit;
    }

    private void OnDisable()
    {
        dragObject.OnDrop -= DragObject_OnDrop;
        getHitEvent.OnGetHit -= GetHitEvent_OnGetHit;
    }

    private void Start()
    {
        // Start as idle
        enterStateEvent_Idle.CallOnEnterStateIdle();
    }

    private void Update()
    {
        if (currentState != null)
            currentState.StateUpdate();
    }

    private void DragObject_OnDrop()
    {
        DisablePet();
    }

    private void GetHitEvent_OnGetHit(GetHitEvent getHitEvent, GetHitEventArgs getHitEventArgs)
    {
        currentState.GetHit(getHitEventArgs.worldPosition, getHitEventArgs.isCritical);
    }

    public void SetState(IPetState stateToSet)
    {
        currentState = stateToSet;
    }

    public void LeftClick(Vector3 position)
    {
        currentState.LeftClick(position);
    }

    public void StartLeftDrag()
    {
        currentState.StartLeftDrag();
    }

    public void LeftDrag(Vector3 position, Vector3 positionRelative)
    {
        currentState.LeftDrag(position, positionRelative);
    }

    public void LeftMouseUp(Vector3 position, Vector3 positionRelative)
    {
        currentState.LeftMouseUp(position, positionRelative);
    }

    public void PopOut(ObjectPoolManager poolManager,GameObject poolKey, Vector3 position, Vector2 popDirection, float speed)
    {
        this.poolManager = poolManager;

        this.poolKey = poolKey;

        transform.position = position;

        gameObject.SetActive(true);

        enterStateEvent_Fall.CallOnEnterStateFall();

        movementByVelocityEvent.CallMovementByVelocityEvent(popDirection, speed);
    }

    public void DisablePet()
    {
        gameObject.SetActive(false);

        poolManager.ReleaseComponentToPool(poolKey, this);
    }
}

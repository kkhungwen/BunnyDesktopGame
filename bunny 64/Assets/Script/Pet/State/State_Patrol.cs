using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pet))]
[RequireComponent(typeof(Hunger))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(MovementByVelocityEvent))]
[RequireComponent(typeof(TryHitEvent))]
[RequireComponent(typeof(EnterStateEvent_Eat))]
[RequireComponent(typeof(EnterStateEvent_Patrol))]
[RequireComponent(typeof(EnterStateEvent_Idle))]
[RequireComponent(typeof(EnterStateEvent_Drag))]
[RequireComponent(typeof(EnterStateEvent_Stagger))]
[RequireComponent(typeof(EnterStateEvent_Fall))]
[DisallowMultipleComponent]
public class State_Patrol : MonoBehaviour, IPetState
{
    private Pet pet;
    private Hunger hunger;

    // Events
    private MovementByVelocityEvent movementByVelocityEvent;
    private TryHitEvent tryHitEvent;
    private EnterStateEvent_Eat enterStateEvent_Eat;
    private EnterStateEvent_Patrol enterStateEvent_Patrol;
    private EnterStateEvent_Idle enterStateEvent_Idle;
    private EnterStateEvent_Drag enterStateEvent_Drag;
    private EnterStateEvent_Stagger enterStateEvent_Stagger;
    private EnterStateEvent_Fall enterStateEvent_Fall;

    // Movement Configurables
    [SerializeField] private float maxPatrolTime = 10f;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float minDistance = 2f;
    [SerializeField] private float maxDistance = 6f;
    [SerializeField] private float colliderHeight = 0.5f;
    [SerializeField] private float colliderHalfWidth = .5f;

    [SerializeField] private LayerMask groundLayerMask;
    private BoxCollider2D collider;

    // Movement parameters
    private float moveToTargetSoftRange = 0.1f;
    private Vector2 patrolTargetPosition;
    private float patrolTimeCount = 0f;

    // Screen boundaries world position
    private Vector2 workingspaceLowerBoundPosition;
    private Vector2 workingspaceUpperBoundPosition;

    private void Awake()
    {
        // Cache required components
        pet = GetComponent<Pet>();
        hunger = GetComponent<Hunger>();
        collider = GetComponent<BoxCollider2D>();
        movementByVelocityEvent = GetComponent<MovementByVelocityEvent>();
        tryHitEvent = GetComponent<TryHitEvent>();
        enterStateEvent_Eat = GetComponent<EnterStateEvent_Eat>();
        enterStateEvent_Patrol = GetComponent<EnterStateEvent_Patrol>();
        enterStateEvent_Idle = GetComponent<EnterStateEvent_Idle>();
        enterStateEvent_Drag = GetComponent<EnterStateEvent_Drag>();
        enterStateEvent_Stagger = GetComponent<EnterStateEvent_Stagger>();
        enterStateEvent_Fall = GetComponent<EnterStateEvent_Fall>();
    }

    private void OnEnable()
    {
        enterStateEvent_Patrol.OnEnterState_Patrol += EnterStateEvent_Patrol_OnEnterState_Patrol;
    }
    private void OnDisable()
    {
        enterStateEvent_Patrol.OnEnterState_Patrol -= EnterStateEvent_Patrol_OnEnterState_Patrol;
    }

    private void EnterStateEvent_Patrol_OnEnterState_Patrol(EnterStateEvent_Patrol args)
    {
        EnterState();
    }

    public void StateUpdate()
    {
        CountTimer();

        CheckIfGrounded();

        TryEat();


        if (Mathf.Abs(patrolTargetPosition.x - transform.position.x) > moveToTargetSoftRange)
        {
            MoveToPosition(patrolTargetPosition);
        }
        else
        {
            ExitState();

            enterStateEvent_Idle.CallOnEnterStateIdle();
        }
    }

    public void LeftClick(Vector3 position)
    {
        tryHitEvent.CallTryHit(position);
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
        ExitState();

        enterStateEvent_Stagger.CallOnEnterStateStagger();
    }

    public void Stun()
    {

    }

    public void TryJump(Vector3 targetPosition)
    {

    }

    private void EnterState()
    {
        //Debug.Log("patrol");
        patrolTargetPosition = GetPatrolTartgetPosition();
        patrolTimeCount = 0f;

        pet.SetState(this);
    }

    private void ExitState()
    {
        movementByVelocityEvent.CallMovementByVelocityEvent(Vector2.zero, 0);
    }

    private void MoveToPosition(Vector2 targetPosition)
    {
        Vector2 targetDirection = (targetPosition - (Vector2)transform.position).normalized;
        movementByVelocityEvent.CallMovementByVelocityEvent(targetDirection, moveSpeed);
    }

    private Vector2 GetPatrolTartgetPosition()
    {
        Vector2 targetPosition = transform.position;

        int maxLoopCount = 20;
        int loopCount = 0;
        while (Vector2.Distance(targetPosition, transform.position) < minDistance || Vector2.Distance(targetPosition, transform.position) > maxDistance)
        {
            loopCount++;
            if (loopCount > maxLoopCount)
                break;

            // Set screen boundaries world position
            workingspaceLowerBoundPosition = WindowFormsDllUtils.GetWorkingSpaceLowerBoundPosition();
            workingspaceUpperBoundPosition = WindowFormsDllUtils.GetWorkingSpaceUpperBoundPosition();

            targetPosition = new Vector2(Random.Range(workingspaceLowerBoundPosition.x + colliderHalfWidth, workingspaceUpperBoundPosition.x - colliderHalfWidth), workingspaceLowerBoundPosition.y + colliderHeight);
        }

        return targetPosition;
    }

    private void CheckIfGrounded()
    {
        RaycastHit2D rayCastHit = Physics2D.BoxCast((Vector2)transform.position + collider.offset, collider.size, transform.eulerAngles.z, transform.TransformVector(Vector2.down), 0.2f, groundLayerMask);

        if (rayCastHit.collider != null)
            return;

        ExitState();
        enterStateEvent_Fall.CallOnEnterStateFall();
    }

    private void CountTimer()
    {
        patrolTimeCount += Time.deltaTime;

        if (patrolTimeCount >= maxPatrolTime)
        {
            ExitState();
            enterStateEvent_Idle.CallOnEnterStateIdle();
        }
    }

    private void TryEat()
    {
        if (hunger.TryEat(out IEdible edible))
        {
            ExitState();
            enterStateEvent_Eat.CallOnEnterStateEat(edible.GetEat(transform.position));
        }
    }
}

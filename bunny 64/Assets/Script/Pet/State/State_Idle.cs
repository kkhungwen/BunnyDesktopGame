using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pet))]
[RequireComponent(typeof(TryHitEvent))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(JumpDetect))]
[RequireComponent(typeof(MovementByVelocityEvent))]
[RequireComponent(typeof(EnterStateEvent_Idle))]
[RequireComponent(typeof(EnterStateEvent_Patrol))]
[RequireComponent(typeof(EnterStateEvent_Drag))]
[RequireComponent(typeof(EnterStateEvent_Stagger))]
[RequireComponent(typeof(EnterStateEvent_Fall))]
[DisallowMultipleComponent]
public class State_Idle : MonoBehaviour, IPetState
{
    private Pet pet;
    private JumpDetect jumpDetect;
    [SerializeField] float jumpCooldown = 1;
    private float jumpCooldownCount = 0;

    // Events
    private MovementByVelocityEvent movementByVelocityEvent;
    private TryHitEvent tryHitEvent;
    private EnterStateEvent_Idle enterStateEvent_Idle;
    private EnterStateEvent_Patrol enterStateEvent_Patrol;
    private EnterStateEvent_Drag enterStateEvent_Drag;
    private EnterStateEvent_Stagger enterStateEvent_Stagger;
    private EnterStateEvent_Fall enterStateEvent_Fall;

    // Configurables
    [Space(10f)]
    [Header("IDLE MOVEMENT")]
    [SerializeField] private LayerMask groundLayerMask;
    private BoxCollider2D collider;
    [SerializeField] private float idleTimeMin;
    [SerializeField] private float idleTimeMax;

    // Idle Parameters
    private float idleTime;


    private void Awake()
    {
        // Cache required components
        pet = GetComponent<Pet>();
        movementByVelocityEvent = GetComponent<MovementByVelocityEvent>();
        jumpDetect = GetComponent<JumpDetect>();
        collider = GetComponent<BoxCollider2D>();
        tryHitEvent = GetComponent<TryHitEvent>();
        enterStateEvent_Idle = GetComponent<EnterStateEvent_Idle>();
        enterStateEvent_Patrol = GetComponent<EnterStateEvent_Patrol>();
        enterStateEvent_Drag = GetComponent<EnterStateEvent_Drag>();
        enterStateEvent_Stagger = GetComponent<EnterStateEvent_Stagger>();
        enterStateEvent_Fall = GetComponent<EnterStateEvent_Fall>();
    }

    private void OnEnable()
    {
        enterStateEvent_Idle.OnEnterState_Idle += EnterStateEvent_Idle_OnEnterState_Idle;
    }
    private void OnDisable()
    {
        enterStateEvent_Idle.OnEnterState_Idle -= EnterStateEvent_Idle_OnEnterState_Idle;
    }

    private void EnterStateEvent_Idle_OnEnterState_Idle(EnterStateEvent_Idle args)
    {
        EnterState();
    }

    public void StateUpdate()
    {
        CheckIfGrounded();

        TryJump();

        idleTime -= Time.deltaTime;
        jumpCooldownCount -= Time.deltaTime;

        if (idleTime <= 0)
        {
            ExitState();
            enterStateEvent_Patrol.CallOnEnterStatePatrol();
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
        //Debug.Log("idle");

        idleTime = Random.Range(idleTimeMin, idleTimeMax);

        pet.SetState(this);
    }

    private void ExitState()
    {

    }

    private void CheckIfGrounded()
    {
        RaycastHit2D rayCastHit = Physics2D.BoxCast((Vector2)transform.position + collider.offset, collider.size, transform.eulerAngles.z, transform.TransformVector(Vector2.down), 0.2f, groundLayerMask);

        if (rayCastHit.collider != null)
            return;

        ExitState();
        enterStateEvent_Fall.CallOnEnterStateFall();
    }

    private void TryJump()
    {
        if (jumpDetect.DetectEdible(out Vector2 position))
        {
            if (jumpCooldownCount > 0)
                return;

            Debug.Log("jump");

            jumpCooldownCount = jumpCooldown;

            movementByVelocityEvent.CallMovementByVelocityEvent(GetJumpVelocity(transform.position, position, 1f), 1f);

            ExitState();

            enterStateEvent_Fall.CallOnEnterStateFall();
        }
    }

    private Vector2 GetJumpVelocity(Vector2 startPosition, Vector2 targetPosition, float time)
    {
        Vector2 velocity = targetPosition / time - startPosition / time - 0.5f * Physics2D.gravity * time;
        return velocity;
    }
}

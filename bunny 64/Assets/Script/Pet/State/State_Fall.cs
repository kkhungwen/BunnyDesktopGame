using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pet))]
[RequireComponent(typeof(Hunger))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(TryHitEvent))]
[RequireComponent(typeof(MovementByVelocityEvent))]
[RequireComponent(typeof(EnterStateEvent_Fall))]
[RequireComponent(typeof(EnterStateEvent_Idle))]
[RequireComponent(typeof(EnterStateEvent_Eat))]
[DisallowMultipleComponent]
public class State_Fall : MonoBehaviour, IPetState
{
    // Fall parameters
    [SerializeField] private LayerMask groundLayerMask;
    private BoxCollider2D collider;
    private Hunger hunger;
    [SerializeField] private GameObject eatParticalKey;

    [Space(10f)]
    [Header("BOUNCE")]
    // BounceParameters
    [SerializeField] private float bounceXStrength = 1;
    [SerializeField] private float bounceXRange = 1;
    [SerializeField] private float minBounceVelocityY = 1f;
    [SerializeField] private float maxBounceVelocityY = 1f;
    [SerializeField] private float bounceSpeed = 1;
    [SerializeField] private float criticalModifier;

    private Pet pet;

    // Events
    private TryHitEvent tryHitEvent;
    private MovementByVelocityEvent movementByVelocityEvent;
    private EnterStateEvent_Eat enterStateEvent_Eat;
    private EnterStateEvent_Fall enterStateEvent_Fall;
    private EnterStateEvent_Idle enterStateEvent_Idle;

    // Screen boundaries world position
    private float workingspaceLowerBound;

    private void Awake()
    {
        // // Cache required components
        pet = GetComponent<Pet>();
        hunger = GetComponent<Hunger>();
        collider = GetComponent<BoxCollider2D>();
        tryHitEvent = GetComponent<TryHitEvent>();
        movementByVelocityEvent = GetComponent<MovementByVelocityEvent>();
        enterStateEvent_Eat = GetComponent<EnterStateEvent_Eat>();
        enterStateEvent_Fall = GetComponent<EnterStateEvent_Fall>();
        enterStateEvent_Idle = GetComponent<EnterStateEvent_Idle>();

        // Set screen boundaries world position
        workingspaceLowerBound = WindowFormsDllUtils.GetWorkingSpaceLowerBoundPosition().y;
    }

    private void OnEnable()
    {
        enterStateEvent_Fall.OnEnterState_Fall += EnterStateEvent_Fall_OnEnterState_Fall;
    }

    private void OnDisable()
    {
        enterStateEvent_Fall.OnEnterState_Fall -= EnterStateEvent_Fall_OnEnterState_Fall;
    }

    private void EnterStateEvent_Fall_OnEnterState_Fall(EnterStateEvent_Fall args)
    {
        EnterState();
    }

    public void StateUpdate()
    {
        CheckIfGrounded();

        TryEat();
    }

    public void LeftClick(Vector3 position)
    {
        tryHitEvent.CallTryHit(position);
    }

    public void StartLeftDrag()
    {

    }

    public void LeftDrag(Vector3 position, Vector3 positionRelative)
    {

    }

    public void LeftMouseUp(Vector3 position, Vector3 positionRelative)
    {

    }

    public void GetHit(Vector3 position, bool isCritical)
    {
        BounceUp(position, isCritical);
    }

    public void Stun()
    {

    }

    public void TryJump(Vector3 targetPosition)
    {

    }

    private void BounceUp(Vector3 position, bool isCritical)
    {
        float bounceVelocityX;

        /*
        bounceVelocityX = transform.position.x - position.x;
        bounceVelocityX = Mathf.Clamp(bounceVelocityX, -bounceXRange, bounceXRange);
        bounceVelocityX = bounceVelocityX * bounceXStrength;
        */

        bounceVelocityX = Random.Range(-bounceXRange, bounceXRange);

        float bounceVelocityY = Random.Range(minBounceVelocityY, maxBounceVelocityY);

        if (isCritical)
        {
            movementByVelocityEvent.CallMovementByVelocityEvent(new Vector2(bounceVelocityX, bounceVelocityY), bounceSpeed * criticalModifier);
        }
        else
        {
            movementByVelocityEvent.CallMovementByVelocityEvent(new Vector2(bounceVelocityX, bounceVelocityY), bounceSpeed);
        }
    }

    private void EnterState()
    {
        //Debug.Log("fall");

        pet.SetState(this);
    }

    private void ExitState()
    {

    }

    private void CheckIfGrounded()
    {
        RaycastHit2D rayCastHit = Physics2D.BoxCast((Vector2)transform.position + collider.offset, collider.size, transform.eulerAngles.z, transform.TransformVector(Vector2.down), 0.1f, groundLayerMask);

        if (rayCastHit.collider == null)
            return;
        Debug.Log("grounded");
        ExitState();
        enterStateEvent_Idle.CallOnEnterStateIdle();
    }

    private void TryEat()
    {
        if (hunger.TryEat(out IEdible edible))
        {
            ParticalPoolObject particalPoolObject = pet.poolManager.GetComponentFromPool(eatParticalKey) as ParticalPoolObject;

            if (particalPoolObject == null)
                return;

            particalPoolObject.Emit(pet.poolManager, eatParticalKey, transform.position);

            ExitState();

            enterStateEvent_Eat.CallOnEnterStateEat(edible.GetEat(transform.position));
        }
    }

#if UNITY_EDITOR
    #region Validation
    private void OnValidate()
    {

    }
    #endregion
#endif
}

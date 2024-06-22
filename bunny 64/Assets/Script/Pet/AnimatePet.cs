using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(MovementByVelocityEvent))]
[RequireComponent(typeof(SetFaceDirectionEvent))]
[RequireComponent(typeof(EnterStateEvent_Idle))]
[RequireComponent(typeof(EnterStateEvent_Patrol))]
[RequireComponent(typeof(EnterStateEvent_Stagger))]
[RequireComponent(typeof(EnterStateEvent_Drag))]
[RequireComponent(typeof(EnterStateEvent_Fall))]
[RequireComponent(typeof(EnterStateEvent_Eat))]
[DisallowMultipleComponent]
public class AnimatePet : MonoBehaviour
{
    [SerializeField] SpriteRenderer petSpriteRenderer;

    [SerializeField] bool isFlipSprite = true;

    [HideInInspector] public bool isFlipX = false;

    private Animator anima;

    private MovementByVelocityEvent movementByVelocityEvent;
    private SetFaceDirectionEvent setFaceDirectionEvent;
    private EnterStateEvent_Idle enterStateEvent_Idle;
    private EnterStateEvent_Patrol enterStateEvent_Patrol;
    private EnterStateEvent_Stagger enterStateEvent_Stagger;
    private EnterStateEvent_Drag enterStateEvent_Drag;
    private EnterStateEvent_Fall enterStateEvent_Fall;
    private EnterStateEvent_Eat enterStateEvent_Eat;

    private void Awake()
    {
        anima = GetComponent<Animator>();

        movementByVelocityEvent = GetComponent<MovementByVelocityEvent>();
        setFaceDirectionEvent = GetComponent<SetFaceDirectionEvent>();
        enterStateEvent_Idle = GetComponent<EnterStateEvent_Idle>();
        enterStateEvent_Patrol = GetComponent<EnterStateEvent_Patrol>();
        enterStateEvent_Stagger = GetComponent<EnterStateEvent_Stagger>();
        enterStateEvent_Drag = GetComponent<EnterStateEvent_Drag>();
        enterStateEvent_Fall = GetComponent<EnterStateEvent_Fall>();
        enterStateEvent_Eat = GetComponent<EnterStateEvent_Eat>();

        movementByVelocityEvent.OnMovementByVelocity += MovementByVelocityEvent_OnMovementByVelocity;
        setFaceDirectionEvent.OnSetFaceDirection += SetFaceDirectionEvent_OnSetFaceDirection;
        enterStateEvent_Idle.OnEnterState_Idle += EnterStateEvent_Idle_OnEnterState_Idle;
        enterStateEvent_Patrol.OnEnterState_Patrol += EnterStateEvent_Patrol_OnEnterState_Patrol;
        enterStateEvent_Stagger.OnEnterStateStagger += EnterStateEvent_Stagger_OnEnterStateStagger;
        enterStateEvent_Drag.OnEnterState_Drag += EnterStateEvent_Drag_OnEnterState_Drag;
        enterStateEvent_Fall.OnEnterState_Fall += EnterStateEvent_Fall_OnEnterState_Fall;
        enterStateEvent_Eat.OnEnterState_Eat += EnterStateEvent_Eat_OnEnterState_Eat;
    }

    private void MovementByVelocityEvent_OnMovementByVelocity(MovementByVelocityEvent movementByVelocityEvent, MovementByVelocityEventArgs movementByVelocityEventArgs)
    {
        FlipPetSpriteRenderer(movementByVelocityEventArgs.moveDirection);
    }

    private void SetFaceDirectionEvent_OnSetFaceDirection(SetFaceDirectionEventArgs setFaceDirectionEventArgs)
    {
        FlipPetSpriteRenderer(setFaceDirectionEventArgs.direction);
    }

    private void EnterStateEvent_Idle_OnEnterState_Idle(EnterStateEvent_Idle enterStateEvent_Idle)
    {
        anima.Play(Settings.idle);
    }

    private void EnterStateEvent_Patrol_OnEnterState_Patrol(EnterStateEvent_Patrol enterStateEvent_Patrol)
    {
        anima.Play(Settings.patrol);
    }
    private void EnterStateEvent_Stagger_OnEnterStateStagger(EnterStateEvent_Stagger enterStateEvent_Stagger)
    {
        anima.Play(Settings.stagger, 0, 0);
    }

    private void EnterStateEvent_Drag_OnEnterState_Drag(EnterStateEvent_Drag enterStateEvent_Drag)
    {
        anima.Play(Settings.drag);
    }
    private void EnterStateEvent_Fall_OnEnterState_Fall(EnterStateEvent_Fall enterStateEvent_Fall)
    {
        anima.Play(Settings.fall);
    }

    private void EnterStateEvent_Eat_OnEnterState_Eat(EnterStateEvent_Eat obj, SpriteAnimationSO getEatAnimation)
    {
        anima.Play(Settings.eat);
    }

    private void FlipPetSpriteRenderer(Vector3 direction)
    {
        if (!isFlipSprite)
            return;

        if (direction.x < 0)
        {
            petSpriteRenderer.flipX = true;
            isFlipX = true;
        }
        else if (direction.x > 0)
        {
            petSpriteRenderer.flipX = false;
            isFlipX = false;
        }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pet))]
[RequireComponent(typeof(GroundDetection))]
[RequireComponent(typeof(TryHitEvent))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(EnterStateEvent_Eat))]
[RequireComponent(typeof(EnterStateEvent_Idle))]
[RequireComponent(typeof(EnterStateEvent_Drag))]
[RequireComponent(typeof(EnterStateEvent_Stagger))]
[RequireComponent(typeof(EnterStateEvent_Fall))]
[RequireComponent(typeof(AnimatePet))]
[DisallowMultipleComponent]
public class State_Eat : MonoBehaviour, IPetState
{
    private Pet pet;
    private AnimatePet animatePet;

    // Events
    private TryHitEvent tryHitEvent;
    private EnterStateEvent_Eat enterStateEvent_Eat;
    private EnterStateEvent_Idle enterStateEvent_Idle;
    private EnterStateEvent_Drag enterStateEvent_Drag;
    private EnterStateEvent_Stagger enterStateEvent_Stagger;
    private EnterStateEvent_Fall enterStateEvent_Fall;

    // Configurables
    [SerializeField] private float eatTime;
    private float eatTimeCount = 0f;

    [SerializeField] private SpriteAnimator getEatSpriteAnimator;
    [SerializeField] private Vector2 getEatPosition;


    private void Awake()
    {
        // Cache required components
        pet = GetComponent<Pet>();
        animatePet = GetComponent<AnimatePet>();
        tryHitEvent = GetComponent<TryHitEvent>();
        enterStateEvent_Eat = GetComponent<EnterStateEvent_Eat>();
        enterStateEvent_Idle = GetComponent<EnterStateEvent_Idle>();
        enterStateEvent_Drag = GetComponent<EnterStateEvent_Drag>();
        enterStateEvent_Stagger = GetComponent<EnterStateEvent_Stagger>();
        enterStateEvent_Fall = GetComponent<EnterStateEvent_Fall>();
    }

    private void OnEnable()
    {
        enterStateEvent_Eat.OnEnterState_Eat += EnterStateEvent_Eat_OnEnterState_Eat;
    }

    private void OnDisable()
    {
        enterStateEvent_Eat.OnEnterState_Eat -= EnterStateEvent_Eat_OnEnterState_Eat;
    }

    private void EnterStateEvent_Eat_OnEnterState_Eat(EnterStateEvent_Eat obj, SpriteAnimationSO getEatAnimation)
    {
        EnterState(getEatAnimation);
    }

    public void StateUpdate()
    {
        eatTimeCount += Time.deltaTime;

        if (eatTimeCount >= eatTime)
        {
            ExitState();
            enterStateEvent_Idle.CallOnEnterStateIdle();
        }
    }

    public void GetHit(Vector3 position, bool isCritical)
    {

    }

    public void LeftClick(Vector3 position)
    {

    }

    public void LeftDrag(Vector3 position, Vector3 positionRelative)
    {

    }

    public void LeftMouseUp(Vector3 position, Vector3 positionRelative)
    {

    }

    public void StartLeftDrag()
    {

    }

    public void Stun()
    {

    }

    public void TryJump(Vector3 targetPosition)
    {

    }

    private void EnterState(SpriteAnimationSO getEatAnimation)
    {
        Debug.Log("eat");
        pet.SetState(this);

        eatTimeCount = 0f;

        if (animatePet.isFlipX)
            getEatSpriteAnimator.transform.localPosition = new Vector2(-getEatPosition.x, getEatPosition.y);
        else
            getEatSpriteAnimator.transform.localPosition = getEatPosition;
        

        getEatSpriteAnimator.gameObject.SetActive(true);

        getEatSpriteAnimator.PlayAnimation(getEatAnimation);
    }

    private void ExitState()
    {
        getEatSpriteAnimator.gameObject.SetActive(false);
    }
}

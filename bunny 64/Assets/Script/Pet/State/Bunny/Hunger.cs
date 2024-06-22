using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[DisallowMultipleComponent]
[RequireComponent(typeof(EnterStateEvent_Eat))]
[RequireComponent(typeof(BoxCollider2D))]
public class Hunger : MonoBehaviour
{
    [SerializeField] private float hungerTimeMin;
    [SerializeField] private float hungerTimMax;

    private EnterStateEvent_Eat enterStateEvent_Eat;
    private float hungerTimeCount = 0f;
    private float hungerTime = 10f;

    private BoxCollider2D eatCollider;
    [SerializeField] private LayerMask edibleLayerMask;
    private Collider2D[] edibleColliderArray = new Collider2D[4];
    private ContactFilter2D edibleContactFilter = new ContactFilter2D();

    public bool isHunger { get; private set; }

    private void Awake()
    {
        eatCollider = GetComponent<BoxCollider2D>();

        enterStateEvent_Eat = GetComponent<EnterStateEvent_Eat>();

        edibleContactFilter.SetLayerMask(edibleLayerMask);
        edibleContactFilter.useLayerMask = true;
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
        ResetHungerTimer();
    }

    private void Update()
    {
        hungerTimeCount += Time.deltaTime;

        if (hungerTimeCount >= hungerTime)
        {
            isHunger = true;
        }
    }

    private void ResetHungerTimer()
    {
        hungerTime = UnityEngine.Random.Range(hungerTimeMin, hungerTimMax);
        hungerTimeCount = 0f;

        isHunger = false;
    }

    public bool TryEat(out IEdible edible)
    {
        edible = null;

        if (!isHunger)
            return false;

        Array.Clear(edibleColliderArray, 0, edibleColliderArray.Length);

        eatCollider.OverlapCollider(edibleContactFilter, edibleColliderArray);

        foreach (Collider2D edibleCollider in edibleColliderArray)
        {
            if (edibleCollider == null)
                continue;

            IEdible edibleTemp = edibleCollider.GetComponent<IEdible>();
            if (edibleTemp != null)
                edible = edibleTemp;
        }

        if (edible != null)
            return true;

        return false;
    }
}

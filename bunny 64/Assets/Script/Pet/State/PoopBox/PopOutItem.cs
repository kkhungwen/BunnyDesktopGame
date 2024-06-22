using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PopOutItemEvent))]
[DisallowMultipleComponent]
public class PopOutItem : MonoBehaviour
{
    [SerializeField] private ObjectPoolManager objectPoolManager;
    [SerializeField] private GameObject BunnyPoolKey;
    [SerializeField] private RandomObject<GameObject> randomPopOutKey;
    [SerializeField] private GameObject confettiKey;
    [SerializeField] private GameObject starsKey;

    [Space(10f)]
    [Header("POP OUT")]
    // BounceParameters
    [SerializeField] Transform popTransform;
    [SerializeField] private float popXRange = 1;
    [SerializeField] private float minPopVelocityY = 1f;
    [SerializeField] private float maxPopVelocityY = 1f;
    [SerializeField] private float popSpeed = 1;

    private PopOutItemEvent popOutItemEvent;

    private void Awake()
    {
        popOutItemEvent = GetComponent<PopOutItemEvent>();

        popOutItemEvent.OnPopOutItem += PopOutItemEvent_OnPopOutItem;
    }

    private void PopOutItemEvent_OnPopOutItem(bool isBunny)
    {
        PopOut(isBunny);
        PopOutEffect(isBunny);
    }

    private void PopOut(bool isBunny)
    {
        float bounceVelocityX = Random.Range(-popXRange, popXRange);

        float bounceVelocityY = Random.Range(minPopVelocityY, maxPopVelocityY);

        if(!isBunny)
        {
            GameObject key = randomPopOutKey.GetRandomObject();

            IPopOutItem popOutItem = objectPoolManager.GetComponentFromPool(key) as IPopOutItem;

            if (popOutItem != null)
            {
                popOutItem.PopOut(objectPoolManager, key, popTransform.position, new Vector2(bounceVelocityX, bounceVelocityY), popSpeed);
            }
        }
        else
        {

            IPopOutItem popOutItem = objectPoolManager.GetComponentFromPool(BunnyPoolKey) as IPopOutItem;

            if (popOutItem != null)
            {
                popOutItem.PopOut(objectPoolManager, BunnyPoolKey, popTransform.position, new Vector2(bounceVelocityX, bounceVelocityY), popSpeed);
            }
        }
    }

    private void PopOutEffect(bool isBunny)
    {
        if (!isBunny)
            return;

        ParticalPoolObject confettiObject = objectPoolManager.GetComponentFromPool(confettiKey) as ParticalPoolObject;

        if (confettiObject == null)
            return;

        confettiObject.Emit(objectPoolManager, confettiKey, popTransform.position);

        ParticalPoolObject starsObject = objectPoolManager.GetComponentFromPool(starsKey) as ParticalPoolObject;

        if (starsObject == null)
            return;

        starsObject.Emit(objectPoolManager, starsKey, popTransform.position);
    }
}

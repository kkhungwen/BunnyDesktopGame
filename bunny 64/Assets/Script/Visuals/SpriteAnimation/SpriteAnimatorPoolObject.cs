using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(SpriteAnimator))]
public class SpriteAnimatorPoolObject : MonoBehaviour
{
    private ObjectPoolManager objectPoolManager;
    private GameObject poolKey;
    private SpriteAnimator spriteAnimator;
    private bool isMoveUp = false;
    private float moveUpSpeed = 0;

    private void Awake()
    {
        spriteAnimator = GetComponent<SpriteAnimator>();
    }

    private void OnEnable()
    {
        spriteAnimator.OnAnimationFinished += SpriteAnimator_OnAnimationFinished;
    }

    private void OnDisable()
    {
        spriteAnimator.OnAnimationFinished -= SpriteAnimator_OnAnimationFinished;
    }

    private void SpriteAnimator_OnAnimationFinished()
    {
        isMoveUp = false;

        gameObject.SetActive(false);

        //Release object to pool
        objectPoolManager.ReleaseComponentToPool(poolKey, this);
    }

    private void Update()
    {
        MoveUpEffect();
    }

    public void SetPositionAngle(Vector3 position, float angle)
    {
        transform.position = position;

        Vector3 eulerAngle = new Vector3(0, 0, angle);

        transform.eulerAngles = eulerAngle;
    }

    public void SetMovement(bool isMoveUp, float moveUpSpeed)
    {
        this.isMoveUp = isMoveUp;
        this.moveUpSpeed = moveUpSpeed;
    }

    public void PlayAnimation(SpriteAnimationSO spriteAnimationSO, GameObject poolKey, ObjectPoolManager objectPoolManager)
    {
        this.objectPoolManager = objectPoolManager;

        this.poolKey = poolKey;

        gameObject.SetActive(true);

        spriteAnimator.PlayAnimation(spriteAnimationSO);
    }

    private void MoveUpEffect()
    {
        if (isMoveUp)
            transform.position = new Vector3(transform.position.x, transform.position.y + moveUpSpeed * Time.deltaTime, 0);
    }
}


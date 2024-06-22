using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[DisallowMultipleComponent]
public class GroundDetection : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayerMask;
    private BoxCollider2D collider;
    public bool isGrounded { get; private set; }

    private void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        CheckIfGrounded();
    }

    private void CheckIfGrounded()
    {
        RaycastHit2D rayCastHit = Physics2D.BoxCast((Vector2)transform.position + collider.offset, collider.size, transform.eulerAngles.z, transform.TransformVector(Vector2.down), 0.2f, groundLayerMask);

        if (rayCastHit.collider != null)
            isGrounded = true;
        else
            isGrounded = false;
    }
}

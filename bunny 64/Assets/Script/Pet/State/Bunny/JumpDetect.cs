using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[DisallowMultipleComponent]
public class JumpDetect : MonoBehaviour
{
    [SerializeField] private Hunger hunger;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private BoxCollider2D detectCollider;
    private Collider2D[] colliderArray = new Collider2D[3];
    private ContactFilter2D contactFilter = new ContactFilter2D();

    private void Awake()
    {
        contactFilter.layerMask = layerMask;
        contactFilter.useLayerMask = true;
    }


    public bool DetectEdible(out Vector2 position)
    {
        position = Vector2.zero;

        if (!hunger.isHunger)
            return false;

        Array.Clear(colliderArray, 0, colliderArray.Length);

        detectCollider.OverlapCollider(contactFilter, colliderArray);

        foreach (Collider2D collider in colliderArray)
        {
            if (collider == null)
                continue;

            IEdible edible = collider.GetComponent<IEdible>();

            if (edible == null)
                continue;

            position = collider.transform.position;
            return true;
        }

        return false;
    }
}

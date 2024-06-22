using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(EdgeCollider2D))]
[DisallowMultipleComponent]
public class Boundaries : MonoBehaviour
{
    private EdgeCollider2D boundsCollider;

    private Vector2 workingSpaceLowerBoundPosition;
    private Vector2 workingSpaceUpperBoundPosition;

    
    private void Start()
    {
        Invoke("SetUpBoundaries", .01f);
    }

    private void SetUpBoundaries()
    {
        workingSpaceLowerBoundPosition = WindowFormsDllUtils.GetWorkingSpaceLowerBoundPosition();
        workingSpaceUpperBoundPosition = WindowFormsDllUtils.GetWorkingSpaceUpperBoundPosition();

        boundsCollider = GetComponent<EdgeCollider2D>();
        boundsCollider.points = new Vector2[5] { workingSpaceLowerBoundPosition, new Vector2(workingSpaceUpperBoundPosition.x, workingSpaceLowerBoundPosition.y), workingSpaceUpperBoundPosition,
            new Vector2(workingSpaceLowerBoundPosition.x, workingSpaceUpperBoundPosition.y), workingSpaceLowerBoundPosition };
    }
}

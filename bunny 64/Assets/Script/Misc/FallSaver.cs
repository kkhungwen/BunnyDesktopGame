using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallSaver : MonoBehaviour
{
    private Vector2 workingSpaceLowerBoundPosition;
    private Vector2 workingSpaceUpperBoundPosition;


    private void Start()
    {
        Invoke("SetUpBoundaries", .01f);

        InvokeRepeating("CheckIfFall", .02f, 1f);
    }

    private void SetUpBoundaries()
    {
        workingSpaceLowerBoundPosition = WindowFormsDllUtils.GetWorkingSpaceLowerBoundPosition();
        workingSpaceUpperBoundPosition = WindowFormsDllUtils.GetWorkingSpaceUpperBoundPosition();
    }

    private void CheckIfFall()
    {
        if (transform.position.x > workingSpaceLowerBoundPosition.x && transform.position.y > workingSpaceLowerBoundPosition.y && transform.position.x < workingSpaceUpperBoundPosition.x && transform.position.y < workingSpaceUpperBoundPosition.y)
            return;

        transform.position = (Vector2)Camera.main.transform.position;
    }
}

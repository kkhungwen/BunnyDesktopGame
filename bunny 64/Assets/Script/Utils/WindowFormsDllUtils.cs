using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WindowFormsDllUtils
{
    public static Vector2 GetWorkingSpaceLowerBoundPosition()
    {
        System.Drawing.Rectangle workingRectangle = System.Windows.Forms.Screen.GetWorkingArea(System.Drawing.Point.Empty);
        Vector2 workingSpaceLowerBoundPosition = Camera.main.ScreenToWorldPoint(new Vector3(workingRectangle.X, Screen.height - workingRectangle.Height - workingRectangle.Y, 0));
        return workingSpaceLowerBoundPosition;
    }

    public static Vector2 GetWorkingSpaceUpperBoundPosition()
    {
        System.Drawing.Rectangle workingRectangle = System.Windows.Forms.Screen.GetWorkingArea(System.Drawing.Point.Empty);
        Vector2 workingSpaceUpperBoundPosition = Camera.main.ScreenToWorldPoint(new Vector2(workingRectangle.X + workingRectangle.Width, Screen.height - workingRectangle.Y));
        return workingSpaceUpperBoundPosition;
    }
}

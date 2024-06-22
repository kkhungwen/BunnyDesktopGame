using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IClickable 
{
    public Vector3 GetWorldPosition();

    public void LeftClick(Vector3 position);

    public void StartLeftHold();

    public void LeftHold(Vector3 position, Vector3 positionRelative);

    public void StartLeftDrag();

    public void LeftDrag(Vector3 position, Vector3 positionRelative);

    public void LeftMouseUp(Vector3 position, Vector3 positionRelative);
}

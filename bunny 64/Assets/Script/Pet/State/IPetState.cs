using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPetState 
{
    public void StateUpdate();

    public void LeftClick(Vector3 position);

    public void StartLeftDrag();

    public void LeftDrag(Vector3 position, Vector3 positionRelative);

    public void LeftMouseUp(Vector3 position, Vector3 positionRelative);

    public void GetHit(Vector3 position,bool isCritical);

    public void Stun();

    public void TryJump(Vector3 targetPosition);
}

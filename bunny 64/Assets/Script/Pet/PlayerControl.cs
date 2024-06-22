using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pet))]
[DisallowMultipleComponent]
public class PlayerControl : MonoBehaviour, IClickable
{
    private Pet pet;
    private void Awake()
    {
        pet = GetComponent<Pet>();
    }

    public Vector3 GetWorldPosition()
    {
        return transform.position;
    }

    public void LeftClick(Vector3 position)
    {
        pet.LeftClick(position);
    }

    public void StartLeftDrag()
    {
        pet.StartLeftDrag();
    }

    public void LeftDrag(Vector3 position, Vector3 positionRelative)
    {
        pet.LeftDrag(position, positionRelative);
    }

    public void StartLeftHold()
    {

    }

    public void LeftHold(Vector3 position, Vector3 positionRelative)
    {

    }

    public void LeftMouseUp(Vector3 position, Vector3 positionRelative)
    {
        pet.LeftMouseUp(position, positionRelative);
    }
}

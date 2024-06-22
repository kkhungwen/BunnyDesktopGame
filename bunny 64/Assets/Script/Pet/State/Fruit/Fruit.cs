using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pet))]
[DisallowMultipleComponent]
public class Fruit : MonoBehaviour, IEdible
{
    private Pet pet;
    [SerializeField] private GameObject fruitPoolKey;
    [SerializeField] private SpriteAnimationSO getEatAnimation;

    private void Awake()
    {
        pet = GetComponent<Pet>();
    }

    public SpriteAnimationSO GetEat(Vector2 position)
    {
        pet.DisablePet();

        return getEatAnimation;
    }
}

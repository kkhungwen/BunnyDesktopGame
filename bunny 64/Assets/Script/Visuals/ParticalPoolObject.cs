using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class ParticalPoolObject : MonoBehaviour
{
    public void Emit(ObjectPoolManager objectPoolManager, GameObject poolKey, Vector3 position)
    {
        transform.position = position;
        gameObject.SetActive(true);

        objectPoolManager.ReleaseComponentToPool(poolKey, this);
    }
}

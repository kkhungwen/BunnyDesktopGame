using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPopOutItem 
{
    public void PopOut(ObjectPoolManager poolManager, GameObject poolKey,Vector3 position, Vector2 popDirection, float popSpeed);
}

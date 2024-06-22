using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PopUpText))]
[DisallowMultipleComponent]
public class PopUpTextPoolObject : MonoBehaviour
{
    private ObjectPoolManager objectPoolManager;
    private GameObject poolKey;
    private PopUpText popUpText;

    private void Awake()
    {
        popUpText = GetComponent<PopUpText>();
    }

    private void OnEnable()
    {
        popUpText.OnFinishPop += PopUpText_OnFinishPop;
    }
    private void OnDisable()
    {
        popUpText.OnFinishPop -= PopUpText_OnFinishPop;
    }

    public void PopText(Vector3 position, float lifeTime, string text, float textSize, Color color, GameObject poolKey, ObjectPoolManager objectPoolManager)
    {
        this.objectPoolManager = objectPoolManager;

        this.poolKey = poolKey;

        gameObject.SetActive(true);

        popUpText.PopText(position, lifeTime, text, textSize, color);
    }

    private void PopUpText_OnFinishPop()
    {
        gameObject.SetActive(false);

        //Release object to pool
        objectPoolManager.ReleaseComponentToPool(poolKey, this);
    }
}

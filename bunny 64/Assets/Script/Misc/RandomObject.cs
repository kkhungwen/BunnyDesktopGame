using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RandomObject<T>
{
    [System.Serializable]
    public class RandomObjectData
    {
        public T obj;
        public int chance;
    }

    [SerializeField] private RandomObjectData[] randomObjectDataArray;

    public T GetRandomObject()
    {
        int totalChance = 0;
        for (int i = 0; i < randomObjectDataArray.Length; i++)
        {
            totalChance += randomObjectDataArray[i].chance;
        }

        int randomChance = Random.Range(0, totalChance);
        int chanceAddUp = 0;
        for (int i = 0; i < randomObjectDataArray.Length; i++)
        {
            chanceAddUp += randomObjectDataArray[i].chance;
            if (chanceAddUp > randomChance)
                return randomObjectDataArray[i].obj;
        }
        Debug.Log("random chance out of range");
        return default;
    }
}

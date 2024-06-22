using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[DisallowMultipleComponent]
public class ObjectPoolManager : MonoBehaviour
{
    [SerializeField] private PoolData[] poolArray = null;
    private Dictionary<int, Pool> poolDictionary = new Dictionary<int, Pool>();
    private Transform objectPoolTransform;

    [System.Serializable]
    public struct PoolData
    {
        #region Tooltip
        [Tooltip("Amount of object intantiate at the start of pool manager")]
        #endregion
        public int startPoolSize;
        #region Tooltip
        [Tooltip("Max amount of object that will be stored in the pool when release")]
        #endregion
        public int maxPoolSize;
        #region Tooltip
        [Tooltip("InstanceID used as key for dictionary")]
        #endregion
        public GameObject prefab;
        #region Tooltip
        [Tooltip("Component type for pool queue. Populate with name string of the component type")]
        #endregion
        public string componentType;
    }

    public class Pool
    {
        public Pool(int maxPoolSize, Transform parentTransform, string componentType)
        {
            this.maxPoolSize = maxPoolSize;
            this.parentTransform = parentTransform;
            this.componentType = componentType;
            componentQueue = new Queue<Component>();
        }

        public int maxPoolSize;

        public Transform parentTransform;

        public string componentType;

        public Queue<Component> componentQueue;
    }


    private void Awake()
    {
        objectPoolTransform = transform;
        CreatePoolDictionary();
    }


    private void CreatePoolDictionary()
    {
        for (int i = 0; i < poolArray.Length; i++)
        {
            CreatePool(poolArray[i].startPoolSize, poolArray[i].maxPoolSize, poolArray[i].prefab, poolArray[i].componentType);
        }
    }

    private void CreatePool(int startPoolSize, int maxPoolSize, GameObject prefab, string componentType)
    {
        int poolKey = prefab.GetInstanceID();

        string prefabName = prefab.name;

        GameObject parentGameObject = new GameObject(prefabName + "Anchor");

        parentGameObject.transform.SetParent(objectPoolTransform);

        if (!poolDictionary.ContainsKey(poolKey))
        {
            poolDictionary.Add(poolKey, new Pool(maxPoolSize, parentGameObject.transform, componentType));

            for (int i = 0; i < startPoolSize; i++)
            {
                GameObject gameObject = Instantiate(prefab, parentGameObject.transform);

                gameObject.SetActive(false);

                poolDictionary[poolKey].componentQueue.Enqueue(gameObject.GetComponent(Type.GetType(componentType)));

                if (gameObject.GetComponent(Type.GetType(componentType)) == null)
                    Debug.Log("Cannot get component type " + componentType + " from " + prefab);
            }
        }
        else
        {
            Debug.Log("Already has prefab " + prefab + " as pool");
        }
    }

    // Get component type object with gameobject instanceid
    public Component GetComponentFromPool(GameObject prefab)
    {
        int poolKey = prefab.GetInstanceID();

        if (poolDictionary.ContainsKey(poolKey))
        {
            if (poolDictionary[poolKey].componentQueue.TryDequeue(out Component component))
            {
                component.gameObject.SetActive(false);

                return component;
            }
            else
            {
                return CreateNewObjectForPool(prefab);
            }
        }
        else
        {
            Debug.Log("No object pool for" + prefab);
            return null;
        }
    }


    // Return conponent type to queue specified by gameObject instanceID
    public void ReleaseComponentToPool(GameObject prefab, Component component)
    {
        int poolKey = prefab.GetInstanceID();
        if (poolDictionary.TryGetValue(poolKey, out Pool pool))
        {
            if (pool.componentQueue.Contains(component))
            {
                Debug.Log("Try to release the same component " + prefab + " to pool twice");
                return;
            }
            
            if (pool.componentQueue.Count >= pool.maxPoolSize)
            {
                //destroy object
                Destroy(component.gameObject);
            }
            else
            {
                if (component.gameObject.transform.parent != pool.parentTransform)
                    component.gameObject.transform.parent = pool.parentTransform;

                pool.componentQueue.Enqueue(component);
            }
        }
        else
        {
            Debug.Log("No object pool for" + component.gameObject);
        }
    }

    private Component CreateNewObjectForPool(GameObject prefab)
    {
        int poolKey = prefab.GetInstanceID();

        GameObject gameObject = Instantiate(prefab, poolDictionary[poolKey].parentTransform);

        gameObject.SetActive(false);

        Component component = gameObject.GetComponent(Type.GetType(poolDictionary[poolKey].componentType));

        return component;
    }
}

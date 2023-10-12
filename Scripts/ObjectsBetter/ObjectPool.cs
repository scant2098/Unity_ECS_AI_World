using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ObjectPool<T> where T : MonoBehaviour
{
    private List<T> pooledObjects;
    private T prefab;
    private Transform parentTransform;
    public ObjectPool(T prefab, int initialSize, Transform parentTransform = null)
    {
        this.prefab = prefab;
        this.parentTransform = parentTransform;

        pooledObjects = new List<T>();

        for (int i = 0; i < initialSize; i++)
        {
            CreateObject();
        }
    }
    private T CreateObject()
    {
        T newObj = Object.Instantiate(prefab);

        if (parentTransform != null)
        {
            newObj.transform.SetParent(parentTransform);
        }

        newObj.gameObject.SetActive(false);
        pooledObjects.Add(newObj);

        return newObj;
    }

    public T GetObject()
    {
        foreach (T obj in pooledObjects)
        {
            if (!obj.gameObject.activeInHierarchy)
            {
                obj.gameObject.SetActive(true);
                return obj;
            }
        }

        T newObj = CreateObject();
        newObj.gameObject.SetActive(true);
        return newObj;
    }

    public void ReleaseObject(T obj)
    {
        obj.gameObject.SetActive(false);
    }
}


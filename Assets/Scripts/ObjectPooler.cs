using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public GameObject objectToPool;
    public int initialPoolSize = 10;

    private List<GameObject> pooledObjects;

    void Start()
    {
        pooledObjects = new List<GameObject>();

        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject obj = Instantiate(objectToPool);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    /*public GameObject GetPooledObject()
    {
        foreach (var obj in pooledObjects)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }

        GameObject newObj = Instantiate(objectToPool);
        newObj.SetActive(false);
        pooledObjects.Add(newObj);
        return newObj;
    }

    public void ReturnPooledObject(GameObject obj)
    {
        obj.SetActive(false);
    }*/
}

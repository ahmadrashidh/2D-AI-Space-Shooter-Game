using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    protected GameObject objToPool;

    [SerializeField]
    protected int poolSize = 10;

    protected Queue<GameObject> objPool;

    public Transform spawnedObjectParent;


    private void Awake()
    {
        objPool = new Queue<GameObject>();
    }

    public void Initialize(GameObject objToPool, int poolSize = 10)
    {
        this.objToPool = objToPool;
        this.poolSize = poolSize;
    }

    public GameObject CreateObject()
    {
        CreateObjectParentIfNeeded();

        GameObject spawnedObj = null;

        if(objPool.Count < poolSize)
        {
            spawnedObj = Instantiate(objToPool, transform.position, Quaternion.identity);
            spawnedObj.name = transform.root.name + "_" + objToPool.name + "_" + objPool.Count;
            spawnedObj.transform.SetParent(spawnedObjectParent);
            spawnedObj.AddComponent<DestroyIfDisabled>();
        } else
        {
            spawnedObj = objPool.Dequeue();
            spawnedObj.transform.position = transform.position;
            spawnedObj.transform.rotation = Quaternion.identity;
            spawnedObj.SetActive(true);
        }

        objPool.Enqueue(spawnedObj);
        return spawnedObj;
    }

    private void CreateObjectParentIfNeeded()
    {
       if(spawnedObjectParent == null)
        {
            string name = "ObjectPool_" + objToPool.name;
            var parentObj = GameObject.Find(name);
            if(parentObj != null)
            {
                Debug.Log("ParentObjFound");
                spawnedObjectParent = parentObj.transform;
            } else
            {
                spawnedObjectParent = new GameObject(name).transform;
                Debug.Log("SpawnedObjectParent" + spawnedObjectParent.name);
            }
        }
    }

    private void OnDestroy()
    {
        foreach(var item in objPool)
        {
            if (item == null)
                continue;
            else if (item.activeSelf == false)
                Destroy(item);
            else
                item.GetComponent<DestroyIfDisabled>().SelfDestructionEnabled = true;
        }
    }
}

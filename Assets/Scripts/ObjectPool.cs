using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;

//
//  Copyright © 2022 Kyo Matias, Nate Florendo. All rights reserved.
//

public class ObjectPool : MonoBehaviour
{

    private GameObject _objectToPool;
    private bool _notEnoughObjectsInPool = true;

    private List<GameObject> _objectsPool;
    public Transform spawnedObjectsParent;

    private void Start()
    {
        _objectsPool = new List<GameObject>();
    }

    public GameObject GetObject(GameObject objectToPool)
    {
        _objectToPool = objectToPool;
        
        if (_objectsPool.Count > 0)
        {
            for (int i = 0; i < _objectsPool.Count; i++)
            {
                if (!_objectsPool[i].activeInHierarchy)
                {
                    _objectsPool[i].transform.position = transform.position;
                    return _objectsPool[i];
                }
            }
        }

        if (_notEnoughObjectsInPool)
        {
            CreateObjectParentIfNeeded();
            
            GameObject obj = Instantiate(_objectToPool, transform.position, Quaternion.identity);
            obj.name = transform.root.name + "_" + _objectToPool.name + "_" + _objectsPool.Count;
            obj.transform.SetParent(spawnedObjectsParent);
            obj.SetActive(false);
            _objectsPool.Add(obj);
            return obj;
        }

        return null;
    }
    
    public GameObject GetObject(GameObject objectToPool, Vector3 pos)
    {
        _objectToPool = objectToPool;

        if (_objectsPool.Count > 0)
        {
            for (int i = 0; i < _objectsPool.Count; i++)
            {
                if (!_objectsPool[i].activeInHierarchy)
                {
                    _objectsPool[i].transform.position = pos;
                    return _objectsPool[i];
                }
            }
        }

        
        if (_notEnoughObjectsInPool)
        {
            CreateObjectParentIfNeeded();
            
            GameObject obj = Instantiate(_objectToPool, pos, Quaternion.identity);
            obj.name = transform.root.name + "_" + _objectToPool.name + "_" + _objectsPool.Count;
            obj.transform.SetParent(spawnedObjectsParent);
            obj.SetActive(false);
            _objectsPool.Add(obj);
            return obj;
        }

        return null;
    }

    private void CreateObjectParentIfNeeded()
    {
        if (spawnedObjectsParent == null)
        {
            string name = "ObjectPool_" + _objectToPool.name;
            var parentObject = GameObject.Find(name);
            if (parentObject != null)
                spawnedObjectsParent = parentObject.transform;
            else
            {
                spawnedObjectsParent = new GameObject(name).transform;
            }

        }
    }


}
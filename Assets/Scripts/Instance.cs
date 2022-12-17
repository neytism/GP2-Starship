using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instance : MonoBehaviour
{
    public GameObject myPrefab;

    void Awake()
    {
        Instantiate(myPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }
}

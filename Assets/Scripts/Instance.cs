using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instance : MonoBehaviour
{
    public GameObject playerManager;
    public GameObject soundManager;
    public GameObject poolManager;

    void Awake()
    {
        Instantiate(playerManager, new Vector3(0, 0, 0), Quaternion.identity);
        Instantiate(soundManager, new Vector3(0, 0, 0), Quaternion.identity);
        Instantiate(poolManager, new Vector3(0, 0, 0), Quaternion.identity);
    }
}

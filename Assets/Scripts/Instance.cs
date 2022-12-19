using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
//  Copyright © 2022 Kyo Matias & Nate Florendo. All rights reserved.
//  

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
    
    //adds instance to avoid errors on unity error, and also to avoid repetition of singletons (i know...)
}

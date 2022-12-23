using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
//  Copyright © 2022 Kyo Matias & Nate Florendo. All rights reserved.
//  

public class Instance : MonoBehaviour
{
    public GameObject saveManager;
    public GameObject soundManager;
    public GameObject poolManager;
    public GameObject achievementManager;

    void Awake()
    {
        Instantiate(saveManager, new Vector3(0, 0, 0), Quaternion.identity);
        Instantiate(soundManager, new Vector3(0, 0, 0), Quaternion.identity);
        Instantiate(poolManager, new Vector3(0, 0, 0), Quaternion.identity);
        Instantiate(achievementManager, new Vector3(0, 0, 0), Quaternion.identity);
    }
    
    //adds instance to avoid errors on unity error, and also to avoid repetition of singletons (i know...)
}

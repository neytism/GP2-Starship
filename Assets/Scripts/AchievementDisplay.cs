using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
//  .cs
//  Script
//
//  Created by Kyo Matias on 00/00/2022.
//  Copyright © 2022 Kyo Matias. All rights reserved.
//
public class AchievementDisplay : MonoBehaviour
{
    [SerializeField] GameObject AchievementDebug10;


    private void OnEnable()
    {
        AchievementManager.Achievement._onAchievedTask += DisplayAch;
        AchievementDebug10.SetActive(false);

    }


    private void OnDisable()
    {
        AchievementManager.Achievement._onAchievedTask -= DisplayAch;
    }

    void DisplayAch()
    {
        AchievementDebug10.SetActive(true);
        Debug.Log("ACHIEVEMENT UNLOCKED!!!");
    }
}

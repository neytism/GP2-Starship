using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
//  Copyright © 2022 Kyo Matias & Nate Florendo. All rights reserved.
//  


public class AchievementManager : MonoBehaviour
{
    #region Instance

    private static AchievementManager _instance;
    public static  AchievementManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }
    
    #endregion

    public static Achievement[] defaultAchievements;
    public static event Action<string,string> achievementNotification;
    
    private static int totalKillsInGame;
    private static int totalDeathsInGame;


    private void Start()
    {
        InitializeData();
    }

    public static void InitializeData()
    {
        totalKillsInGame = SaveManager.Instance.TotalKillsInGame;
        totalDeathsInGame = SaveManager.Instance.TotalDeathsInGame;
    }

    private void OnEnable()
    {
        Enemy.enemyKill += EnemyKillObserver;
        Player.playerDeath += PlayerDeathObserver;
    }

    private void OnDisable()
    {
        Enemy.enemyKill -= EnemyKillObserver;
        Player.playerDeath -= PlayerDeathObserver;
    }

    private void EnemyKillObserver()
    {
        
        totalKillsInGame++;
        SaveManager.Instance.TotalKillsInGame = totalKillsInGame;
        
        if (totalKillsInGame == 1)
        {
            AchievementObserver(0);
        }else if (totalKillsInGame == 100)
        {
            AchievementObserver(2);
        }
    }

    private void PlayerDeathObserver()
    {
        totalDeathsInGame++;
        SaveManager.Instance.TotalDeathsInGame = totalDeathsInGame;
        
        if (Player.KillCount == 0 )
        {
            AchievementObserver(1);
        }
    }

    private void AchievementObserver(int id)
    {
        if (!SaveManager.Achievements[id].achieved)
        {
            achievementNotification?.Invoke(SaveManager.Achievements[id].title,SaveManager.Achievements[id].description);
            SaveManager.Achievements[id].achieved = true;
        }
    }

    public static Achievement[] DefaultAchievementList()
    {
        if (SaveManager.Instance.IsFirstTimePlaying || SaveManager.Achievements == null)
        {
            defaultAchievements = new Achievement[3];

            defaultAchievements[0] = new Achievement(0, "FIRST KILL!!", "Kill your first enemy");
            defaultAchievements[1] = new Achievement(1, "A MORAL SACRIFICE", "Die without killing a single enemy");
            defaultAchievements[2] = new Achievement(2, "100 KILLS!", "Kill 100 enemies in total");
            
            SaveManager.Instance.IsFirstTimePlaying = false;
        }
        
        return defaultAchievements;
    }

}

public class Achievement
{
    public Achievement(int id, string title, string description)
    {
        this.title = title;
        this.description = description;
    }

    public int id;
    public string title;
    public string description;

    public bool achieved;

    public void ResetAchievment()
    {
        this.achieved = false;
    }
}

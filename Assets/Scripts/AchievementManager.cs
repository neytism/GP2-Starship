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

    public static List<Achievement> achievements = new List<Achievement>();
    
    public static event Action<string,string> achievementNotification;
    
    public static int totalKillInGame;
    

    private void Start()
    {
        InitializeAchievements();
        totalKillInGame = PlayerManager.TotalKillsInGame;
    }

    private void InitializeAchievements()
    {

        if (achievements != null)
        {
            achievements.Add(new Achievement(0, "FIRST KILL!!", "Kill your first enemy"));
            achievements.Add(new Achievement(1, "A MORAL SACRIFICE", "Die without killing a single enemy"));
            achievements.Add(new Achievement(2, "100 KILLS!", "Kill 100 enemies"));
        }
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
        totalKillInGame++;
        PlayerManager.TotalKillsInGame = totalKillInGame;
        if (totalKillInGame == 1)
        {
            AchievementObserver(0);
        }else if (totalKillInGame == 100)
        {
            AchievementObserver(2);
        }
    }

    private void PlayerDeathObserver()
    {
        if (Player.KillCount == 0 )
        {
            AchievementObserver(1);
        }
    }

    private void AchievementObserver(int id)
    {
        if (!achievements[id].achieved)
        {
            achievementNotification?.Invoke(achievements[id].title,achievements[id].description);
            achievements[id].achieved = true;
        }
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

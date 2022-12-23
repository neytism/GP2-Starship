using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
//  Copyright © 2022 Kyo Matias & Nate Florendo. All rights reserved.
//  


[System.Serializable]
public class PlayerData
{
    public bool IsNewGame;
    public int Health;
    public int Kills;
    public Vector3 Position;
    public int SelectedCharacter;
    public Achievement[] Achievements;
    public int TotalKillsInGame;
    public int TotalDeathsInGame;
    public int HighScore;
    public bool IsFirstTimePlaying;
    public int LastRoundIn;
    
    //for saving all
    public PlayerData(bool isNewGame,int health, int kills, Vector3 position, int selectedCharacter, Achievement[] achievements, int totalKillsInGame, int totalDeathsInGame, int highScore, bool isFirstTimePlaying, int lastRoundIn)
    {
        this.IsNewGame = isNewGame;
        this.Health = health;
        this.Kills = kills;
        this.Position = position;
        this.SelectedCharacter = selectedCharacter;
        this.Achievements = achievements;
        this.TotalKillsInGame = totalKillsInGame;
        this.TotalDeathsInGame = totalDeathsInGame;
        this.HighScore = highScore;
        this.IsFirstTimePlaying = isFirstTimePlaying;
        this.LastRoundIn = lastRoundIn;
    }

    //for loading some data after death
    public PlayerData(bool isNewGame,int selectedCharacter, Achievement[] achievements, int totalKillsInGame, int totalDeathsInGame, int highScore)
    {
        IsNewGame = isNewGame;
        Health = (int)Player.MaxHealth;
        Kills = 0;
        Position = Vector3.zero;
        SelectedCharacter = selectedCharacter;
        Achievements = achievements;
        TotalKillsInGame = totalKillsInGame;
        TotalDeathsInGame = totalDeathsInGame;
        HighScore = highScore;
        IsFirstTimePlaying = false;
        LastRoundIn = 0;
    }
    
    //for reset game
    public PlayerData()
    {
        IsNewGame = true;
        Health = (int)Player.MaxHealth;
        Kills = 0;
        Position = Vector3.zero;
        SelectedCharacter = 0;
        Achievements = AchievementManager.DefaultAchievementList();
        foreach (var t in Achievements)
        {
            t.achieved = false;
        }
        TotalKillsInGame = 0;
        TotalDeathsInGame = 0;
        HighScore = 0;
        IsFirstTimePlaying = true;
        LastRoundIn = 0;
        
    }
    
    
}

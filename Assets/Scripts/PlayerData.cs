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
    public List<Achievement> Achievements;
    public int TotalKillsInGame;

    public PlayerData(bool isNewGame,int health, int kills, Vector3 position, int selectedCharacter, List<Achievement> achievements, int totalKillsInGame)
    {
        this.IsNewGame = isNewGame;
        this.Health = health;
        this.Kills = kills;
        this.Position = position;
        this.SelectedCharacter = selectedCharacter;
        this.Achievements = achievements;
        this.TotalKillsInGame = totalKillsInGame;
    }

    public PlayerData()
    {
        IsNewGame = true;
        Health = 10;
        Kills = 0;
        Position = Vector3.zero;
        SelectedCharacter = 0;
        TotalKillsInGame = 0;
    }
}

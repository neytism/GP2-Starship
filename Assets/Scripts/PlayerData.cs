using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int Health;
    public int Kills;
    public Vector3 Position;
    //public Quaternion Rotation;
    public int SelectedCharacter;
    
    public PlayerData(int health, int kills, int selectedCharacter)
    {
        this.Health = health;
        this.Kills = kills;
        this.SelectedCharacter = selectedCharacter;
    }

    public PlayerData()
    {
        Health = 10;
        Kills = 0;
        Position = new Vector3(0,0,0);
        SelectedCharacter = 0;
    }
}

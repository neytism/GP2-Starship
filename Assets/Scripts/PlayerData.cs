using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public bool IsNewGame;
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
    
    //TODO : Fix bug on not resetting on NewGame
    //TODO : Add Achievement system
    //TODO : Make Load Game not clickable if no save, Add isNewGame boolean
    //TODO : Add Achievement Section on MainMenu
    //TODO : Track Achievements on Save, but not replace when dead.
    //TODO : Quit on Pause Menu
    //TODO : Mute on Pause Menu and Main Menu
    //TODO : UI sounds
    
}

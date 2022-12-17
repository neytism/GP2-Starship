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
    public int SelectedCharacter;
    
    public PlayerData(bool isNewGame,int health, int kills, Vector3 position, int selectedCharacter)
    {
        this.IsNewGame = isNewGame;
        this.Health = health;
        this.Kills = kills;
        this.Position = position;
        this.SelectedCharacter = selectedCharacter;
    }

    public PlayerData()
    {
        IsNewGame = true;
        Health = 10;
        Kills = 0;
        Position = Vector3.zero;
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

using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

//
//  Copyright © 2022 Kyo Matias & Nate Florendo. All rights reserved.
//  


public class SaveManager : MonoBehaviour
{
    #region Instance

    private static SaveManager _instance;
    public static  SaveManager Instance
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
        LoadData();
    }

    #endregion

    //calls LoadData on awake
    
    private PlayerData _newPlayerData;
    private PlayerData _localPlayerData;

    private const string SAVE_DATA_KEY = "PlayerData";
    
    
    [SerializeField] private List<GameObject> _characters;
    public List<GameObject> Characters => _characters;

    //local data
    public static bool isFirstTimePlaying;
    private static bool _isNewGame;
    private static int _health = 10;
    private static int _kills;
    private static Vector3 _position;
    private static int _selectedCharacter;
    private static Achievement[] _achievements;
    private static int _totalKillsInGame;
    private static int _totalDeathsInGame;
    private static int _highScore;
    private static int _lastRoundIn;

    #region GetSets

    
    public int GetSelectedCharacter
    {
        get => _selectedCharacter;
        set => _selectedCharacter = value;

    }
    
    public bool IsNewGame
    {
        get => _isNewGame;
        set => _isNewGame = value;

    }
    public int Health
    {
        get => _health;
        set => _health = value;
    }

    public int Kills
    {
        get => _kills;
        set => _kills = value;
    }
    
    public Vector3 Position
    {
        get => _position;
        set => _position = value;
    }
    
    public static  Achievement[] Achievements
    {
        get => _achievements;
        set => _achievements = value;
    }
    
    public int TotalKillsInGame
    {
        get => _totalKillsInGame;
        set => _totalKillsInGame = value;
    }
    
    public int TotalDeathsInGame
    {
        get => _totalDeathsInGame;
        set => _totalDeathsInGame = value;
    }

    public int HighScore
    {
        get => _highScore;
        set => _highScore = value;
    }

    public bool IsFirstTimePlaying
    {
        get => isFirstTimePlaying;
        set => isFirstTimePlaying = value;
    }

    public int LastRoundIn
    {
        get => _lastRoundIn;
        set => _lastRoundIn = value;
    }

    #endregion
    
    //SAVE SYSTEM
    //SAVE SYSTEM
    //SAVE SYSTEM
    
    public SpriteRenderer SelectCharacterSprite(int index)
    {
        return _characters[index].GetComponent<SpriteRenderer>();
    }

    public void NewGame()
    {
        NewData();
        SceneManager.LoadScene("GameScene");
        AudioManager.Instance.StopPlayingBGM(AudioManager.Sounds.MainMenuBGM);
    }

    public void NewData()
    {
        _newPlayerData = new PlayerData(_isNewGame,_selectedCharacter,_achievements, _totalKillsInGame, _totalDeathsInGame, _highScore);
        var data = JsonConvert.SerializeObject(_newPlayerData, new JsonSerializerSettings{ReferenceLoopHandling = ReferenceLoopHandling.Ignore});
        PlayerPrefs.SetString(SAVE_DATA_KEY, data);
        Debug.Log($"{nameof(SaveManager)}.{nameof(NewGame)} : {data}");
        
        //loading data for new game
        LoadDataOnLocalData(_newPlayerData);
    }

    public void LoadData()
    {
        Debug.Log("Checking if data available");
        
        if (PlayerPrefs.HasKey(SAVE_DATA_KEY))
        {
            Debug.Log("Has data to load");
            var jsonToConvert = PlayerPrefs.GetString(SAVE_DATA_KEY);
            Debug.Log($"{nameof(SaveManager)}.LoadData : {jsonToConvert}");
            _localPlayerData = JsonConvert.DeserializeObject<PlayerData>(jsonToConvert, new JsonSerializerSettings{ReferenceLoopHandling = ReferenceLoopHandling.Ignore});
            
            // Setting up data when loading

            LoadDataOnLocalData(_localPlayerData);
            
        }
        else
        {
            Debug.Log("No available data to load, new game only");
            
            _newPlayerData = new PlayerData();
            var data = JsonConvert.SerializeObject(_newPlayerData, new JsonSerializerSettings{ReferenceLoopHandling = ReferenceLoopHandling.Ignore});
            PlayerPrefs.SetString(SAVE_DATA_KEY, data);
            LoadDataOnLocalData(_newPlayerData);
            AchievementManager.InitializeData();
            Debug.Log($"{nameof(SaveManager)}.LoadData : {data}");
        } 
    }

    private void LoadDataOnLocalData(PlayerData playerData)
    {
        _isNewGame = playerData.IsNewGame;
        _health = playerData.Health;
        _kills = playerData.Kills;
        _position = playerData.Position;
        _selectedCharacter = playerData.SelectedCharacter;
        _achievements = playerData.Achievements;
        _totalKillsInGame = playerData.TotalKillsInGame;
        _totalDeathsInGame = playerData.TotalDeathsInGame;
        _highScore = playerData.HighScore;
        isFirstTimePlaying = playerData.IsFirstTimePlaying;
        _lastRoundIn = playerData.LastRoundIn;
    }
    
    public void LoadGame()
    {
        AudioManager.Instance.StopPlayingBGM(AudioManager.Sounds.MainMenuBGM);
        SceneManager.LoadScene("GameScene");
    }
    
    public void SaveGame()
    {
        //save data
        _localPlayerData = new PlayerData(_isNewGame, _health,_kills, _position, _selectedCharacter, _achievements, _totalKillsInGame, _totalDeathsInGame, _highScore, isFirstTimePlaying, _lastRoundIn);
        var playerData = JsonConvert.SerializeObject(_localPlayerData, new JsonSerializerSettings{ReferenceLoopHandling = ReferenceLoopHandling.Ignore});
        Debug.Log($"{nameof(SaveManager)}.{nameof(SaveGame)} : {playerData}");
        PlayerPrefs.SetString(SAVE_DATA_KEY, playerData);
    }

    public void ResetGameData()  // RESETS ALL DATA
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("GAME HAS BEEN RESET");
        SceneManager.LoadScene("MainMenu");

    }


}
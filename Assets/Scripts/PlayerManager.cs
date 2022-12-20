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


public class PlayerManager : MonoBehaviour
{
    #region Instance

    private static PlayerManager _instance;
    public static  PlayerManager Instance
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
    
    [SerializeField] private PlayerData _newPlayerData;
    [SerializeField] private PlayerData _localPlayerData;

    private const string SAVE_DATA_KEY = "PlayerData";
    
    //for character selection, can be moved to MainMenu Method
    [SerializeField] private List<GameObject> _characters;
    [SerializeField] private List<String> _names;
    [SerializeField] private List<String> _description;
    [SerializeField] private SpriteRenderer _sr;
    private TextMeshProUGUI _characterName;
    private TextMeshProUGUI _characterDescription;

    //static data
    private static bool _isNewGame;
    private static int _health = 10;
    private static int _kills;
    private static Vector3 _position;
    private static int _selectedCharacter;
    private static List<Achievement> _achievements;
    private static int _totalKillsInGame;

    public int GetSelectedCharacter()
    {
        return _selectedCharacter;
    }

    public int GetChartacterCount()
    {
        return _characters.Count;
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
    
    public static List<Achievement> Achievements
    {
        get => _achievements;
        set => _achievements = value;
    }
    
    public static int TotalKillsInGame
    {
        get => _totalKillsInGame;
        set => _totalKillsInGame = value;
    }
    
    public Color SelectCharacterSprite(int index)
    {
        return _characters[index].GetComponent<SpriteRenderer>().color;
    }

    public void UpdateSelected(int value)
    {
        //for updating character selection on main menu
        _sr = GameObject.Find("SelectedSkin").GetComponent<SpriteRenderer>();
        _sr.color = _characters[value].GetComponent<SpriteRenderer>().color;
        _characterName = GameObject.Find("Name").GetComponent<TextMeshProUGUI>();
        _characterDescription = GameObject.Find("Description").GetComponent<TextMeshProUGUI>();
        _characterName.text = _names[value];
        _characterDescription.text = _description[value];
        _selectedCharacter = value;
        Debug.Log($"Selected: {value}");
    }

    //SAVE SYSTEM
    //SAVE SYSTEM
    //SAVE SYSTEM

    public void NewGame()
    {
        NewData();
        SceneManager.LoadScene("GameScene");
        AudioManager.Instance.StopPlayingBGM(AudioManager.Sounds.MainMenuBGM);
    }

    public void NewData()
    {
        _newPlayerData = new PlayerData();
        var data = JsonConvert.SerializeObject(_newPlayerData, new JsonSerializerSettings{ReferenceLoopHandling = ReferenceLoopHandling.Ignore});
        PlayerPrefs.SetString(SAVE_DATA_KEY, data);
        Debug.Log($"{nameof(PlayerManager)}.{nameof(NewGame)} : {data}");
        
        //loading data for new game
        _isNewGame = _newPlayerData.IsNewGame;
        _health = _newPlayerData.Health;
        _kills = _newPlayerData.Kills;
        _position = _newPlayerData.Position;
    }

    public void LoadData()
    {
        Debug.Log("Checking if data available");
        
        if (PlayerPrefs.HasKey(SAVE_DATA_KEY))
        {
            Debug.Log("Has data to load");
            var jsonToConvert = PlayerPrefs.GetString(SAVE_DATA_KEY);
            Debug.Log($"{nameof(PlayerManager)}.{nameof(LoadGame)} : {jsonToConvert}");
            _localPlayerData = JsonConvert.DeserializeObject<PlayerData>(jsonToConvert, new JsonSerializerSettings{ReferenceLoopHandling = ReferenceLoopHandling.Ignore});
            
            // Setting up data when loading

            _isNewGame = _localPlayerData.IsNewGame;
            _selectedCharacter = _localPlayerData.SelectedCharacter;
            _kills = _localPlayerData.Kills;
            _health = _localPlayerData.Health;
            _position = _localPlayerData.Position;
            _achievements = AchievementManager.achievements;
            _totalKillsInGame = _localPlayerData.TotalKillsInGame;
        }
        else
        {
            Debug.Log("No available data to load, new game only");
            
            _newPlayerData = new PlayerData();
            var data = JsonConvert.SerializeObject(_newPlayerData, new JsonSerializerSettings{ReferenceLoopHandling = ReferenceLoopHandling.Ignore});
            PlayerPrefs.SetString(SAVE_DATA_KEY, data);
        
            //loading data for new game
            _isNewGame = _newPlayerData.IsNewGame;
            _health = _newPlayerData.Health;
            _kills = _newPlayerData.Kills;
            _position = _newPlayerData.Position;
            _achievements = AchievementManager.achievements;
            _totalKillsInGame = _newPlayerData.TotalKillsInGame;
        } 
    }
    public void LoadGame()
    {
        LoadData();
        SceneManager.LoadScene("GameScene");
        AudioManager.Instance.StopPlayingBGM(AudioManager.Sounds.MainMenuBGM);
    }
    
    public void SaveGame()
    {
        //save date
        Debug.Log($"Position on Save {_position}");
        _localPlayerData = new PlayerData(false, _health, _kills, _position, _selectedCharacter, _achievements, _totalKillsInGame);
        
        var playerData = JsonConvert.SerializeObject(_localPlayerData, new JsonSerializerSettings{ReferenceLoopHandling = ReferenceLoopHandling.Ignore});
        Debug.Log($"{nameof(PlayerManager)}.{nameof(SaveGame)} : {playerData}");
        PlayerPrefs.SetString(SAVE_DATA_KEY, playerData);
    }

    public void ResetGameData()  // RESETS ALL DATA
    {
        _newPlayerData = new PlayerData();
        var data = JsonConvert.SerializeObject(_newPlayerData, new JsonSerializerSettings{ReferenceLoopHandling = ReferenceLoopHandling.Ignore});
        PlayerPrefs.SetString(SAVE_DATA_KEY, data);
        Debug.Log($"{nameof(PlayerManager)}.{nameof(NewGame)} : {data}");
        
        //loading data for new game for reset
        _isNewGame = _newPlayerData.IsNewGame;
        _health = _newPlayerData.Health;
        _kills = _newPlayerData.Kills;
        _position = _newPlayerData.Position;
        _totalKillsInGame = _newPlayerData.TotalKillsInGame;

        AchievementManager.totalKillInGame = _totalKillsInGame;
        for (int i = 0 ; i < AchievementManager.achievements.Count; i++)
        {
            AchievementManager.achievements[i].ResetAchievment();
        }

        _achievements = AchievementManager.achievements;
        Debug.Log("GAME HAS BEEN RESET");
        SceneManager.LoadScene("MainMenu");

    }


}
using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [SerializeField] private PlayerData _newPlayerData;
    [SerializeField] private PlayerData _localPlayerData;

    private const string SAVE_DATA_KEY = "PlayerData";
    
    [SerializeField] private List<GameObject> _characters;
    [SerializeField] private List<String> _names;
    [SerializeField] private List<String> _description;
    [SerializeField] private SpriteRenderer _sr;
    private TextMeshProUGUI _characterName;
    private TextMeshProUGUI _characterDescription;

    private static bool _isNewGame;
    private static int _health = 10;
    private static int _kills;
    private static Vector3 _position;
    private static int _selectedCharacter;

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

    public void SetSelected(int value)//for debug
    {
        //_selectedCharacter = value;
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
            _position = new Vector3(_localPlayerData.Position.x, _localPlayerData.Position.y);
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
        _localPlayerData = new PlayerData(false, _health, _kills, _position, _selectedCharacter);
        
        var playerData = JsonConvert.SerializeObject(_localPlayerData, new JsonSerializerSettings{ReferenceLoopHandling = ReferenceLoopHandling.Ignore});
        Debug.Log($"{nameof(PlayerManager)}.{nameof(SaveGame)} : {playerData}");
        PlayerPrefs.SetString(SAVE_DATA_KEY, playerData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    
}

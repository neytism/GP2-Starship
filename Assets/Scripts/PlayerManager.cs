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
    }

    #endregion

    [SerializeField] private PlayerData _playerData;
    [SerializeField] private PlayerData _localPlayerData;

    private const string SAVE_DATA_KEY = "PlayerData";
    
    [SerializeField] private List<GameObject> _characters;
    [SerializeField] private List<String> _names;
    [SerializeField] private List<String> _description;
    [SerializeField] private SpriteRenderer _sr;
    private TextMeshProUGUI _characterName;
    private TextMeshProUGUI _characterDescription;

    private static int _health = 10;
    private static int _kills;
    private static int _selectedCharacter;
    private static Vector3 _position;

    

    private void Start()
    {
        LoadData();
    }

    public Color SelectCharacterSprite(int index)
    {
        return _characters[index].GetComponent<SpriteRenderer>().color;
    }

    public void UpdateSelected(int value)
    {
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
        _selectedCharacter = value;
    }
    
    //SAVE SYSTEM

    public void NewGame()
    {
        this._playerData = new PlayerData();
        _health = 10;
        SceneManager.LoadScene("GameScene");
        AudioManager.Instance.StopPlayingBGM(AudioManager.Sounds.MainMenuBGM);
    }

    public void LoadData()
    {
        if (PlayerPrefs.HasKey(SAVE_DATA_KEY))
        {
            var jsonToConvert = PlayerPrefs.GetString(SAVE_DATA_KEY);
            Debug.Log($"{nameof(PlayerManager)}.{nameof(LoadGame)} : {jsonToConvert}");
            _localPlayerData = JsonConvert.DeserializeObject<PlayerData>(jsonToConvert);
            
            // Setting up data when loading
            
            _selectedCharacter = _localPlayerData.SelectedCharacter;
            _kills = _localPlayerData.Kills;
            _health = _localPlayerData.Health;
            _position = new Vector3(_localPlayerData.Position.x, _localPlayerData.Position.y);
        }
        else
        {
            var playerData = new PlayerData();
            _localPlayerData = playerData;
            var data = JsonConvert.SerializeObject(playerData);
            PlayerPrefs.SetString(SAVE_DATA_KEY, data);
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
        _playerData.SelectedCharacter = _selectedCharacter;
        _playerData.Kills = _kills;
        _playerData.Health = _health;
        Debug.Log($"{_position}");
        _playerData.Position = new Vector3(_position.x, _position.y);

        var playerData = JsonConvert.SerializeObject(_playerData, new JsonSerializerSettings{ReferenceLoopHandling = ReferenceLoopHandling.Ignore});
        Debug.Log($"{nameof(PlayerManager)}.{nameof(SaveGame)} : {playerData}");
        PlayerPrefs.SetString(SAVE_DATA_KEY, playerData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}

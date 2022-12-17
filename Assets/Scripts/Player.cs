using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//
//  Copyright © 2022 Kyo Matias, Nate Florendo. All rights reserved.
//

public class Player : MonoBehaviour
{
    //for UI
    [SerializeField] private Image HPBar;
    [SerializeField] public GameObject gameOverScreen;
    [SerializeField] private TextMeshProUGUI _killCountText;
    
    //for getting data for other classes
    [SerializeField] private float _maxHealth = 10;
    private static int _currentHealth;
    private static int _killCount;
    private static Vector3 _currentPos;
    
    private int _selectedCharacter;
    private PlayerManager _playerManager;
    private Color _color;
    
    [SerializeField] private GameObject _beam;  //for laser ability
    [SerializeField] private GameObject _aoe;   //for EMP ability
    public GameObject diePEffect;
    
    private bool _isInvincible = false;

    public bool IsInvincible
    {
        get => _isInvincible;
        set => _isInvincible = value;
    }
    
    public int KillCount
    {
        get => _killCount;
        set => _killCount = value;
    }
    
    public GameObject Beam => _beam;
    public GameObject AOE => _aoe;
    
    private void Update()
    {
        _currentPos = transform.position;   //for saving position
    }

    private void Awake()
    {
        AudioManager.Instance.PlayFadeIn(AudioManager.Sounds.GameBGM,0.005f, .5f);  //BGM
        
        _playerManager = GameObject.FindObjectOfType<PlayerManager>();
        _color = _playerManager.SelectCharacterSprite(PlayerManager.Instance.GetSelectedCharacter());
        gameObject.GetComponent<SpriteRenderer>().color = _color;
        
        _currentHealth = PlayerManager.Instance.Health;
        _killCount = PlayerManager.Instance.Kills;
        
        UpdateTextKillCount();
        HPBarUpdate();
    }
    
    public void ReduceHealth(int value)
    {
        if (!_isInvincible)
        {
            if (_currentHealth > 0) // if player is not dead
            {
                _currentHealth -= value;
            
                Debug.Log($"Health: {_currentHealth}");
            
                if (_currentHealth <= 0)  //if player is dead
                {
                
                    AudioManager.Instance.StopPlayingBGM(AudioManager.Sounds.GameBGM);
                    AudioManager.Instance.PlayOnce(AudioManager.Sounds.PlayerDeath);
                    
                    //did not put pooling because this only happens when dead 
                    GameObject particle = Instantiate(diePEffect, transform.position, Quaternion.identity);
                    Destroy(particle, 3);
                    
                    //to avoid enemy dying if player is dead, and to avoid movements if dead
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    gameObject.GetComponent<PolygonCollider2D>().enabled = false;
                    gameObject.GetComponent<CircleCollider2D>().enabled = false;
                    PlayerController.IsPauseOrDead = true;
                    
                    StartCoroutine(ShowGameOverScreen());
                    Debug.Log("GAME OVER");
                }
            }
            Debug.Log("Player Damaged");
            
            HPBarUpdate();
        }

    }
    
    private void HPBarUpdate() //updates health bar using image fill
    {
        HPBar.fillAmount =  _currentHealth/ _maxHealth;
    }
    
    IEnumerator ShowGameOverScreen() {
        yield return new WaitForSeconds(1.5f);
        AudioManager.Instance.PlayOnce(AudioManager.Sounds.GameOver);
        gameOverScreen.SetActive(true);
        
        Time.timeScale = 0f;
    }

    public void AddKillCount()
    {
        _killCount++;
        UpdateTextKillCount();
    }

    private void UpdateTextKillCount()
    {
        _killCountText.text = _killCount.ToString();
    }

    public static void UpdateStats()  //FOR SAVING DATA
    {
        PlayerManager.Instance.Position = _currentPos;
        PlayerManager.Instance.Health = _currentHealth;
        PlayerManager.Instance.Kills = _killCount;
    }

    private void OnApplicationQuit()
    {
        UpdateStats();
    }

}

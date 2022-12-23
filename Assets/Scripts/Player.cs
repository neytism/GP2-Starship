using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//
//  Copyright © 2022 Kyo Matias & Nate Florendo. All rights reserved.
//  


public class Player : MonoBehaviour
{
    public static event Action updateHealthBarUI;
    public static event Action playerDeath;
    public static event Action updateKillCountUI; 


    //for getting data for other classes
    private static float _maxHealth = 10;
    private static int _currentHealth;
    private static int _killCount;
    private static Vector3 _currentPos;
    private static Achievement[] _achievements;

    private int _selectedCharacter;
    private SaveManager _saveManager;
    private static SpriteRenderer _color;
    
    [SerializeField] private GameObject _beam;  //for laser ability
    [SerializeField] private GameObject _aoe;   //for EMP ability
    public GameObject diePEffect;
    
    private bool _isInvincible = false;

    public bool IsInvincible
    {
        get => _isInvincible;
        set => _isInvincible = value;
    }
    
    public static int KillCount
    {
        get => _killCount;
        set => _killCount = value;
    }
    
    public static float MaxHealth => _maxHealth;
    public static int CurrentHealth => _currentHealth;
    
    public GameObject Beam => _beam;
    public GameObject AOE => _aoe;
    
    private void Update()
    {
        _currentPos = transform.position;   //for saving position
    }
    
    private void OnEnable()
    {
        Enemy.enemyKill += AddKillCount;
        Enemy.ennemyCollisionWithPlayer += ReduceHealth;
        PlayerController.reachedGameBorder += Suicide;
    }
    

    private void OnDisable()
    {
        Enemy.enemyKill -= AddKillCount;
        Enemy.ennemyCollisionWithPlayer -= ReduceHealth;
        PlayerController.reachedGameBorder -= Suicide;
    }

    private void Awake()
    {
        _saveManager = GameObject.FindObjectOfType<SaveManager>();
        _color = _saveManager.SelectCharacterSprite(SaveManager.Instance.GetSelectedCharacter);
        gameObject.GetComponent<SpriteRenderer>().sprite = _color.sprite;
        gameObject.GetComponent<SpriteRenderer>().drawMode= _color.drawMode;
        gameObject.GetComponent<SpriteRenderer>().size= _color.size;
        
        _currentHealth = SaveManager.Instance.Health;
        _killCount = SaveManager.Instance.Kills;
        _achievements = SaveManager.Achievements;
    }
    
    public void ReduceHealth(int value)
    {
        if (!_isInvincible) //for debugging
        {
            if (_currentHealth > 0) // if player is not dead
            {
                _currentHealth -= value;
            
                Debug.Log($"Health: {_currentHealth}"); //remove
            
                if (_currentHealth <= 0)  //if player is dead
                {
                    GameOver();
                }
                updateHealthBarUI?.Invoke();
            }
        }
    }
    
    private void Suicide()
    {
        ReduceHealth(10);
    }
    
    public void AddKillCount()
    {
        _killCount++;
        updateKillCountUI?.Invoke();
    }

    private void GameOver()
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
        
        playerDeath?.Invoke();
        Debug.Log("GAME OVER");
    }
    
    public static void UpdateStats()  //FOR SAVING DATA
    {
        SaveManager.Instance.Position = _currentPos;
        SaveManager.Instance.Health = _currentHealth;
        SaveManager.Instance.Kills = _killCount;
        SaveManager.Achievements = _achievements;
    }

    private void OnApplicationQuit()
    {
        UpdateStats();
        SaveManager.Instance.SaveGame();
    }
}

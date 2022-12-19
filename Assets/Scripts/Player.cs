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
    public static event Action playerReduceHealth;
    public static event Action playerDeath;
    
    
    //for getting data for other classes
    private static float _maxHealth = 10;
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
    }

    private void OnDisable()
    {
        Enemy.enemyKill -= AddKillCount;
        Enemy.ennemyCollisionWithPlayer -= ReduceHealth;
    }

    private void Awake()
    {
        _playerManager = GameObject.FindObjectOfType<PlayerManager>();
        _color = _playerManager.SelectCharacterSprite(PlayerManager.Instance.GetSelectedCharacter());
        gameObject.GetComponent<SpriteRenderer>().color = _color;
        
        _currentHealth = PlayerManager.Instance.Health;
        _killCount = PlayerManager.Instance.Kills;
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
                
                playerReduceHealth?.Invoke();
            }
        }
    }
    
    public void AddKillCount()
    {
        _killCount++;
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
        PlayerManager.Instance.Position = _currentPos;
        PlayerManager.Instance.Health = _currentHealth;
        PlayerManager.Instance.Kills = _killCount;
    }
}

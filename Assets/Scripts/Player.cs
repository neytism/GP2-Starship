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
    [SerializeField] private Image HPBar;
    [SerializeField] public GameObject gameOverScreen;
    
    [SerializeField] private float _maxHealth = 10;
    private static int _currentHealth;
    private static int _killCount;
    private static Vector3 _currentPos;
    
    [SerializeField] private TextMeshProUGUI _killCountText;

    private int _selectedCharacter;

    private PlayerManager _playerManager;

    private Color _color;
    
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


    [SerializeField] private GameObject _beam;
    [SerializeField] private GameObject _aoe;

    public GameObject Beam => _beam;
    public GameObject AOE => _aoe;


    public GameObject diePEffect;

    private void Update()
    {
        _currentPos = transform.position;
    }

    private void Awake()
    {
        AudioManager.Instance.PlayFadeIn(AudioManager.Sounds.GameBGM,0.005f, .5f);
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
                
                    //insert death sound here
                    AudioManager.Instance.StopPlayingBGM(AudioManager.Sounds.GameBGM);
                    AudioManager.Instance.PlayOnce(AudioManager.Sounds.PlayerDeath);
                    //did not put pooling because this only happens when dead 
                    GameObject particle = Instantiate(diePEffect, transform.position, Quaternion.identity);
                    Destroy(particle, 3);
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    gameObject.GetComponent<PolygonCollider2D>().enabled = false;
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

    public static void UpdateStats()
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

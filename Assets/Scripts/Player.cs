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
    [SerializeField] private float _currentHealth;
    [SerializeField] private int _killCount;
    
    [SerializeField] private TextMeshProUGUI _killCountText;

    

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

    [SerializeField] private AudioClip _deathSound;
    [SerializeField] private AudioClip _gameOver;

    
    private void Awake()
    {
        _playerManager = GameObject.FindObjectOfType<PlayerManager>();
        _color = _playerManager.SelectCharacterSprite(_playerManager.selectedCharacter);
        gameObject.GetComponent<SpriteRenderer>().color = _color;
        _currentHealth = _maxHealth;
        HPBarUpdate();
    }
    
    public void ReduceHealth(float value)
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
                    AudioSource.PlayClipAtPoint(_deathSound, gameObject.transform.position);
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
        AudioSource.PlayClipAtPoint(_gameOver, gameObject.transform.position);
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



}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{

    public bool isPaused = false;

    public GameObject pauseMenuUI;
    public GameObject hpBar;
    public GameObject abilityBar;
    public GameObject killCount;
    
    
    private void Awake()
    {
        pauseMenuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        AudioManager.Instance.ResumePlayingBGM(AudioManager.Sounds.GameBGM);
        hpBar.SetActive(true);
        abilityBar.SetActive(true);
        killCount.SetActive(true);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        AudioManager.Instance.PausePlayingBGM(AudioManager.Sounds.GameBGM);
        pauseMenuUI.SetActive(true);
        hpBar.SetActive(false);
        abilityBar.SetActive(false);
        killCount.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void LoadMenu()
    {
        Player.UpdateStats();
        ObjectPool.Instance._objectsPool.Clear();
        PlayerManager.Instance.SaveGame();
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        Time.timeScale = 1f;
        ObjectPool.Instance._objectsPool.Clear();
        PlayerManager.Instance.NewData();
        SceneManager.LoadScene("MainMenu");
    }

    public void Restart()
    {
        ObjectPool.Instance.Dispose();
        ObjectPool.Instance._objectsPool.Clear();
        PlayerManager.Instance.NewGame();
        Time.timeScale = 1f;
    }
    
    public void ClickSound()
    {
        AudioManager.Instance.PlayOnce(AudioManager.Sounds.ButtonClick);
    }

}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private int _selectedCharacter;

    private void Start()
    {
        AudioManager.Instance.PlayLoop(AudioManager.Sounds.MainMenuBGM);
    }

    public void Init(int value)
    {
        _selectedCharacter = value;
        PlayerManager.Instance.UpdateSelected(_selectedCharacter);
    }
    
    public void NextOption()
    {
        _selectedCharacter += 1;
        if (_selectedCharacter == PlayerManager.Instance.GetChartacterCount())
        {
            _selectedCharacter = 0;
        }

        PlayerManager.Instance.UpdateSelected(_selectedCharacter);
    }
    
    public void BackOption()
    {
        _selectedCharacter -= 1;
        if (_selectedCharacter < 0)
        {
            _selectedCharacter = PlayerManager.Instance.GetChartacterCount()-1;
        }

        PlayerManager.Instance.UpdateSelected(_selectedCharacter);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
        AudioManager.Instance.StopPlayingBGM(AudioManager.Sounds.MainMenuBGM);
        Debug.Log($"Selected before loading: {_selectedCharacter}");
    }
    
    public void QuitGame()
    {
        //UIClickSound.Play();
        Application.Quit();
    }
}

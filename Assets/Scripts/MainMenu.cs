using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    private int _selectedCharacter;
    [SerializeField] private GameObject _loadButton;
    [SerializeField] private TextMeshProUGUI BGMtext;
    [SerializeField] private TextMeshProUGUI SFXtext;
    private bool _isMutedBGM = false;
    private bool _isMutedSFX = false;
    

    private void Start()
    {
        AudioManager.Instance.PlayLoop(AudioManager.Sounds.MainMenuBGM);
        Debug.Log($"Isnewgame: {PlayerManager.Instance.IsNewGame}");
        CheckNewGame();
        
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

    private void CheckNewGame()
    {
        if (PlayerManager.Instance.IsNewGame)
        {
            _loadButton.GetComponent<Button>().interactable = false;
        }
    }

    public void MuteUnmuteBGM()
    {
        if (!_isMutedBGM)
        {
            BGMtext.text = "BGM: Off";
            AudioManager.Instance.SoundBGMusic.mute = true;
            _isMutedBGM = true;
        }
        else
        {
            BGMtext.text = "BGM: On";
            AudioManager.Instance.SoundBGMusic.mute = false;
            _isMutedBGM = false;
        }
    }
    
    public void MuteUnmuteSFX()
    {
        if (!_isMutedSFX)
        {
            SFXtext.text = "SFX: Off";
            AudioManager.Instance.SoundSfx.mute = true;
            _isMutedSFX = true;
        }
        else
        {
            SFXtext.text = "SFX: On";
            AudioManager.Instance.SoundSfx.mute = false;
            _isMutedSFX = false;
        }
    }
    
    public void ClickSound()
    {
        AudioManager.Instance.PlayOnce(AudioManager.Sounds.ButtonClick);
    }
}

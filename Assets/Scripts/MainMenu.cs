using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//
//  Copyright © 2022 Kyo Matias & Nate Florendo. All rights reserved.
//  


public class MainMenu : MonoBehaviour
{
    private int _selectedCharacter;
    [SerializeField] private GameObject _loadButton;
    [SerializeField] private GameObject _achievementContent;
    [SerializeField] private GameObject _achievementPanel;
    [SerializeField] private TextMeshProUGUI BGMtext;
    [SerializeField] private TextMeshProUGUI SFXtext;
    
    [SerializeField] private Color _locked;
    [SerializeField] private Color _unlocked;
    
    private bool _isMutedBGM = false;
    private bool _isMutedSFX = false;


    private void Start()
    {
        AudioManager.Instance.PlayLoop(AudioManager.Sounds.MainMenuBGM);
        Debug.Log($"Isnewgame: {PlayerManager.Instance.IsNewGame}");
        CheckNewGame();
    }

    
    public void Init(int value)  //initializes value of selected character before selecting
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

    public void QuitGame()
    {
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
    
    public void ClickSound() //Referenced on EventTrigger component of UI buttons
    {
        AudioManager.Instance.PlayOnce(AudioManager.Sounds.ButtonClick);
    }
    
    public void LoadAchievementsPanel() // DISPLAYS ACHIEVEMENTS ON MAIN MENU USING THE LIST FROM ACHIEVEMENT MANAGER
    {
        foreach (var t in PlayerManager.Achievements)
        {
            GameObject obj = ObjectPool.Instance.AchievementPanel(_achievementContent, _achievementPanel);
            obj.SetActive(true);
            obj.transform.SetParent(_achievementPanel.transform);
            obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = t.title;
            obj.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = t.description;
            if (!t.achieved)
            {
                obj.GetComponent<Image>().color = _locked;
                obj.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "";
            }
            else
            {
                obj.GetComponent<Image>().color = _unlocked;
                obj.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "done";
            }
        }
    }

    public void BackAchievementPanel() //called on the back button on achievement list
    {
        foreach (var o in ObjectPool.Instance._objectsPool)
        {
            o.SetActive(false);
        }
    }
}

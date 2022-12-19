using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//
//  Copyright © 2022 Kyo Matias & Nate Florendo. All rights reserved.
//  


public class UserInterfaceManager : MonoBehaviour
{
    //variables
    private int _killCount;
    
    //for UI
    [SerializeField] private Image HPBar;
    [SerializeField] public GameObject gameOverScreen;
    [SerializeField] private TextMeshProUGUI _killCountText;

    private void Awake()
    {
        AudioManager.Instance.PlayFadeIn(AudioManager.Sounds.GameBGM,0.005f, .5f);  //BGM
        UpdateKillCountText();
        UpdateHPBar();
    }

    private void OnEnable()
    {
        Enemy.enemyKill += UpdateKillCountText;
        Player.playerReduceHealth += UpdateHPBar;
        Player.playerDeath += ShowGameOverScreen;
        AchievementManager.achievementNotification += ShowAchievement;
    }

    private void OnDisable()
    {
        Enemy.enemyKill -= UpdateKillCountText;
        Player.playerReduceHealth -= UpdateHPBar;
        Player.playerDeath -= ShowGameOverScreen;
        AchievementManager.achievementNotification -= ShowAchievement;
    }
    
    private void UpdateKillCountText()
    {
        _killCountText.text = Player.KillCount.ToString();
    }

    private void UpdateHPBar() 
    {
        HPBar.fillAmount = Player.CurrentHealth / Player.MaxHealth;
    }

    private void ShowGameOverScreen()
    {
        StartCoroutine(GameOverScreenDelay());
    }
    
    private void ShowAchievement(string title, string description)
    {
        //getting achievement panel and text for popup
        GameObject achievementPanel = GameObject.Find("AchievementNotificationPanel");
        GameObject achievementPrompt = achievementPanel.transform.GetChild(0).gameObject;
        GameObject achievementTitle = achievementPanel.transform.GetChild(1).gameObject;
        GameObject achievementDescription = achievementPanel.transform.GetChild(2).gameObject;
        
        //showing text
        achievementPrompt.SetActive(true);
        achievementTitle.SetActive(true);
        achievementDescription.SetActive(true);
        achievementTitle.GetComponent<TextMeshProUGUI>().text = title;
        achievementDescription.GetComponent<TextMeshProUGUI>().text = description;
        
        //sound
        AudioManager.Instance.PlayOnce(AudioManager.Sounds.Achievement);
        
        //disabling again the PopUpText
        StartCoroutine(ClosePopUp(achievementPanel.transform.GetChild(0).gameObject));
        StartCoroutine(ClosePopUp(achievementPanel.transform.GetChild(1).gameObject));
        StartCoroutine(ClosePopUp(achievementPanel.transform.GetChild(2).gameObject));
    }

    IEnumerator ClosePopUp(GameObject window)
    {
        yield return new WaitForSeconds(3);
        window.SetActive(false);
    }
    
    IEnumerator GameOverScreenDelay() {
        yield return new WaitForSeconds(1.5f);
        AudioManager.Instance.PlayOnce(AudioManager.Sounds.GameOver);
        gameOverScreen.SetActive(true);
        
        Time.timeScale = 0f;
    }
}

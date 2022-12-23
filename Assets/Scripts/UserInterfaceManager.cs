using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

//
//  Copyright © 2022 Kyo Matias & Nate Florendo. All rights reserved.
//  


public class UserInterfaceManager : MonoBehaviour
{
    //variables
    private int _killCount;
    
    //for UI
    [SerializeField] private Image HPBar;
    [SerializeField] private GameObject outOfBoundsWarningImage;
    [SerializeField] public GameObject gameOverScreen;
    [SerializeField] public GameObject gameOverScore;
    [SerializeField] public GameObject gameOverHighScore;
    [SerializeField] private TextMeshProUGUI _killCountText;
    private int _killCountUI;

    [SerializeField] private Transform _wallLeft;
    [SerializeField] private Transform _wallRight;
    [SerializeField] private Transform _wallTop;
    [SerializeField] private Transform _wallBottom;

    //for bounds
    [SerializeField] private float _bounderyDistance;
    [SerializeField] private float _flickerIntensty;
    private float temp;
    private float[] _distances;
    private Transform _player;
    private float _distToPlayer;
    private Color _opacity;
    
    private void Awake()
    {
        _distances = new float[4];
        AudioManager.Instance.PlayFadeIn(AudioManager.Sounds.GameBGM,0.005f, .5f);
        HPBar.fillAmount = SaveManager.Instance.Health / Player.MaxHealth;
        _player = GameObject.Find("Player").GetComponent<Transform>();
        _opacity = outOfBoundsWarningImage.GetComponent<SpriteRenderer>().color;
        _killCount = Player.KillCount;
        UpdateKillCountText();
    }

    private void Start()
    {
        UpdateKillCountText();
    }

    private void Update()
    {
        ShowOutOfBoundsWarning();
    }

    private void OnEnable()
    {
        Player.updateKillCountUI += UpdateKillCountText;
        Player.updateHealthBarUI += UpdateHPBar;
        Player.playerDeath += ShowGameOverScreen;
        AchievementManager.achievementNotification += ShowAchievement;
    }

    private void OnDisable()
    {
        Player.updateKillCountUI -= UpdateKillCountText;
        Player.updateHealthBarUI -= UpdateHPBar;
        Player.playerDeath -= ShowGameOverScreen;
        AchievementManager.achievementNotification -= ShowAchievement;
    }
    
    private void UpdateKillCountText()
    {
        _killCountText.text = Player.KillCount.ToString();
    }

    private void UpdateHPBar()
    { 
        //HPBar.fillAmount = temp / Player.MaxHealth             //instant health update
        temp = Player.CurrentHealth + 1;
        StartCoroutine(ReduceHealthDelay());                 //adds delay 
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

    private void ShowOutOfBoundsWarning()
    {
        //getting distance of nearest wall
        Vector3 position = _player.transform.position;
        _distances[0] = Math.Abs(_wallLeft.transform.position.x - position.x);
        _distances[1] = Math.Abs(_wallRight.transform.position.x - position.x);
        _distances[2] = Math.Abs(_wallTop.transform.position.y - position.y);
        _distances[3] = Math.Abs(_wallBottom.transform.position.y - position.y);
        _distToPlayer = _distances.Min();

        //setting opacity of warning
        if (_distToPlayer < _bounderyDistance)
        {
            _opacity.a = Random.Range(1 - (_distToPlayer / _bounderyDistance),
                _flickerIntensty - (_distToPlayer / _bounderyDistance));
        }
        else
        {
            _opacity.a = 0f;
        }
        
        outOfBoundsWarningImage.GetComponent<SpriteRenderer>().color = _opacity;
    }
    

    IEnumerator ClosePopUp(GameObject window)
    {
        yield return new WaitForSeconds(3);
        window.SetActive(false);
    }
    
    IEnumerator GameOverScreenDelay() {
        yield return new WaitForSeconds(3);
        AudioManager.Instance.PlayOnce(AudioManager.Sounds.GameOver);
        gameOverScreen.SetActive(true);
        
        Time.timeScale = 0f;
    }

    IEnumerator ReduceHealthDelay()
    {
        temp -= 0.075f;
        HPBar.fillAmount = temp / Player.MaxHealth;
        yield return new WaitForSeconds(0.01f); // delay speed
        if (temp > Player.CurrentHealth)
        {
            StartCoroutine(ReduceHealthDelay());
        }
    }
}

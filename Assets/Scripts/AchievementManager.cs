using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

//
//  AchievementManager.cs
//  Script
//
//  Created by Kyo Matias on 00/00/2022.
//  Copyright © 2022 Kyo Matias. All rights reserved.
//
public class AchievementManager : MonoBehaviour
{
    public List<Achievement> Achievements;

    [SerializeField] private GameObject PlayerObject;
    private Player player;
    //[SerializeField] public TextMeshProUGUI AchievementText;
    //private TextMeshProUGUI _achText;
    // 2 lines above for ui testing purposes
    
    
    
    
    public int Integer;
    public float Floating_Point;

    
    
    public bool AchievementUnlocked(string achievementName)
    {
        bool result = false;

        if (Achievements == null)
        {
            return false;
        }

        Achievement[] AchievementArray = Achievements.ToArray();
        Achievement A = Array.Find(AchievementArray, ach => achievementName == ach.Title);

        if (A == null)
        {
            return false;
        }

        result = A.Achieved;
        
        return result;
    }

    private void Start()
    {
        InitializeAchievements();
    }

    private void InitializeAchievements()
    {
        if (Achievements != null)
        
            return;

            Achievements = new List<Achievement>();
            Achievements.Add(new Achievement("My First 10", "Kill 10 Enemies", (object o) => Integer >= 10));
            Achievements.Add(new Achievement("My First 100", "Kill 100 Enemies", (object o) => Integer >= 100));
            


    }

    private void Awake()
    {
        //_achText = AchievementText;
        player = PlayerObject.GetComponent<Player>();
    }

    private void Update()
    {
        Integer = player.KillCount;
        CheckAchievementCompletion();
    }

    private void CheckAchievementCompletion()
    {
        if (Achievements == null)
        
            return;

            foreach (var Achievements in Achievements)
            {
                Achievements.UpdateCompletion();
            }
        
        
        
    }

    public class Achievement
    {
        public Achievement(string title, string description, Predicate<object> requirement)
        {
            this.Title = title;
            this.Description = description;
            this.requirement = requirement;
        }

        public string Title;
        public string Description;
        public Predicate<object> requirement;

        public bool Achieved;

        public void UpdateCompletion()
        {
            if (Achieved)
                return;

            if (RequirementsMet())
            {
                Debug.Log($"{Title}: {Description}");
                Achieved = true;
            }
        }

        public bool RequirementsMet()
        {
            return requirement.Invoke(null);
        }

    }
}



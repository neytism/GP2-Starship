using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEBUG : MonoBehaviour
{
    public static int _selectedCharacterDEBUG;

    [SerializeField] private int _int;

    private void Awake()
    {
        _selectedCharacterDEBUG = _int;
       PlayerManager.Instance.SetSelected(_selectedCharacterDEBUG);
    }
}

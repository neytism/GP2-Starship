using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEBUG : MonoBehaviour
{
    [SerializeField] private int _int;

    private void Awake()
    {
       PlayerManager.Instance.SetSelected(_int);
    }
}

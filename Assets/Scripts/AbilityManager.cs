using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//
//  Copyright © 2022 Kyo Matias & Nate Florendo. All rights reserved.
//  


public class AbilityManager : ScriptableObject
{
    public string Name;
    public float Cooldown;
    public float ActiveTime;
    
    public virtual void Activate(GameObject parent){}
}

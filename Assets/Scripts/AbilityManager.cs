using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//
//  .cs
//  Script
//
//  Created by Kyo Matias on 00/00/2022.
//  Copyright © 2022 Kyo Matias. All rights reserved.
//

public class AbilityManager : ScriptableObject
{
    public string Name;
    public float Cooldown;
    public float ActiveTime;
    
    public virtual void Activate(GameObject parent){}
}

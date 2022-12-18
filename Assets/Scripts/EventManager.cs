using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



//
//  EventManager.cs
//  Script
//
//  Created by Kyo Matias on 00/00/2022.
//  Copyright © 2022 Kyo Matias. All rights reserved.
//
public static class EventManager
{
    public static event UnityAction KilledOppon;
    public static void OnKill() => KilledOppon?.Invoke();
}

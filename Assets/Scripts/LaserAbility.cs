using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
//  Copyright © 2022 Kyo Matias & Nate Florendo. All rights reserved.
//  

[CreateAssetMenu]
public class LaserAbility : AbilityManager
{
    public override void Activate(GameObject parent)
    {
        AbilityHolder player = parent.GetComponent<AbilityHolder>();

        AudioManager.Instance.PlayOnce(AudioManager.Sounds.LaserSound);
        player.TurnOnLaser();
        Debug.Log("Laser ABILITY ");
    }
    
    
}
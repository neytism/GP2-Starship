using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
//  Copyright © 2022 Kyo Matias & Nate Florendo. All rights reserved.
//  

[CreateAssetMenu]
public class EMPAbility : AbilityManager
{
    public override void Activate(GameObject parent)
    {
        AbilityHolder player = parent.GetComponent<AbilityHolder>();
        
        AudioManager.Instance.PlayOnce(AudioManager.Sounds.EMPsound);
        
        player.Explode();
        Debug.Log("EMP ABILITY ");
    }
}

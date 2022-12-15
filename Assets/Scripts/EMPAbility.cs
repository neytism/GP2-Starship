using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EMPAbility : AbilityManager
{
    
    public override void Activate(GameObject parent)
    {
        AbilityHolder player = parent.GetComponent<AbilityHolder>();

        AudioSource.PlayClipAtPoint(player._explodeSound, player.transform.position);
        
        player.Explode();
        Debug.Log("EMP ABILITY ");
    }
}

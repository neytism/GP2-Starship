using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
//  Copyright © 2022 Kyo Matias & Nate Florendo. All rights reserved.
//  


public class WeaponHolder : MonoBehaviour
{
    public GameObject SelectWeaponType(int index)
    {
        GameObject weaponPrefab1 = GameObject.Find("RapidGun");
        GameObject weaponPrefab2 = GameObject.Find("SpreadGun");
        GameObject weaponPrefab3 = GameObject.Find("ExplodeGun");
        
        switch (index)
        {
            case 0:
                return weaponPrefab1;
            case 1:
                return weaponPrefab2;
            case 2:
                return weaponPrefab3;
        }

        return weaponPrefab1;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
//  Copyright © 2022 Kyo Matias & Nate Florendo. All rights reserved.
//  

public class ExplosionRadius : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(RadiusLife(gameObject));
    }
    
    IEnumerator RadiusLife(GameObject radius)
    {
        yield return new WaitForSeconds(0.1f);
        radius.SetActive(false);
    }
}

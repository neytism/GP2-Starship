using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
//  Copyright © 2022 Kyo Matias & Nate Florendo. All rights reserved.
//  


public class ParticleSystem : MonoBehaviour
{
    [SerializeField] private float timeBeforeDisabling;
    private void OnEnable()
    {
        StartCoroutine(ParticleLife(gameObject, timeBeforeDisabling));
    }
    
    IEnumerator ParticleLife(GameObject particle,float time)
    {
        yield return new WaitForSeconds(time);
        particle.SetActive(false);
    }
}

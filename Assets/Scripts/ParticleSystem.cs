using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

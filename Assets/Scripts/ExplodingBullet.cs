using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingBullet : MonoBehaviour
{
    [SerializeField] private GameObject _explosionRadius;

    private void Awake()
    {
    }

    private void OnCollisionEnter2D(Collision2D col) 
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.gameObject.tag.Equals("Enemy"))
        {
            //GameObject circle = Instantiate(_explosionRadius, transform.position, Quaternion.identity);
            //Destroy(circle, .1f);
            //GameObject circle = _radiusPool.GetObject(_explosionRadius, transform.position);
            GameObject circle = ObjectPool.Instance.GetObject(_explosionRadius, transform.position);
            circle.SetActive(true);
        }
            
        gameObject.SetActive(false);
    }
    
}

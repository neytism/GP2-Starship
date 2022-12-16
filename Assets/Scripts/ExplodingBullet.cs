using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingBullet : MonoBehaviour
{
    [SerializeField] private GameObject _explosionRadius;
    [SerializeField] private AudioClip _explodeSound;
    private ObjectPool _radiusPool;

    private void Awake()
    {
        _radiusPool = gameObject.AddComponent<ObjectPool>();
    }

    private void OnCollisionEnter2D(Collision2D col) 
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        AudioSource.PlayClipAtPoint(_explodeSound, gameObject.transform.position);
        if (col.gameObject.tag.Equals("Enemy"))
        {
            //GameObject circle = Instantiate(_explosionRadius, transform.position, Quaternion.identity);
            //Destroy(circle, .1f);
            GameObject circle = _radiusPool.GetObject(_explosionRadius);
            circle.SetActive(true);
        }
            
        gameObject.SetActive(false);
    }
    
}

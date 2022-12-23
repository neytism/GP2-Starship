using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
//  Copyright © 2022 Kyo Matias & Nate Florendo. All rights reserved.
//  


public class EnemyBullet : MonoBehaviour
{
    public GameObject smallDiePEffect;
    private bool _isDead;

    private void OnEnable()
    {
        _isDead = false;
        StartCoroutine(BulletLife(gameObject));
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
       
        Player _player = GameObject.FindObjectOfType<Player>();
        
        if (col.gameObject.tag.Equals("Player")) {
            GameObject particle = ObjectPool.Instance.GetObject(smallDiePEffect, transform.position);
            particle.SetActive(true);
            if (!_isDead)
            {
                _player.ReduceHealth(1);
                _isDead = true;
            }
        }
        else if (col.gameObject.tag.Equals("Bullet") || col.gameObject.tag.Equals("ExplosionRadius"))
        {
            GameObject particle = ObjectPool.Instance.GetObject(smallDiePEffect, transform.position);
            particle.SetActive(true);
        }
        
        AudioManager.Instance.PlayOnce(AudioManager.Sounds.MiniExplosion);
        gameObject.SetActive(false);
    }
    
    IEnumerator BulletLife(GameObject bullet)
    {
        yield return new WaitForSeconds(5);
        GameObject particle = ObjectPool.Instance.GetObject(smallDiePEffect, transform.position);
        particle.SetActive(true);
        bullet.SetActive(false);
    }
}

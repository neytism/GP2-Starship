using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

//
//  Copyright © 2022 Kyo Matias, Nate Florendo. All rights reserved.
//

public class Enemy : MonoBehaviour
{
    private Player _player;
    [SerializeField] private int _damage = 1;
    public GameObject diePEffect;
    [SerializeField] private GameObject _enemyBullet;
    [SerializeField] private Transform _enemyFirePoint;
    [SerializeField] private float _enemyFireForce;
    private float timeBtwShot;
    [SerializeField] private float startTimeBtwShots;
    
    [SerializeField] private bool _canFire = true;  //debug
    
    
    private void Awake()
    {
        _player = GameObject.FindObjectOfType<Player>();
        timeBtwShot = startTimeBtwShots;
    }

    private void Update()
    {
        if (_canFire)  //for debugging, set _canfire to false
        {
            EnemyFire();
        }
       
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player") || col.gameObject.tag.Equals("Bullet") || col.gameObject.tag.Equals("ExplosionRadius")) 
        {
            AudioManager.Instance.PlayOnce(AudioManager.Sounds.EnemyDeath);
            GameObject particle = ObjectPool.Instance.GetObject(diePEffect, transform.position);
            particle.SetActive(true);
            
            if (col.gameObject.tag.Equals("Player")) {
               _player.ReduceHealth(_damage);
            }
            
            _player.AddKillCount();
            CameraFollow.startShaking = true;
            gameObject.SetActive(false);
        }
    }

    public void EnemyFire()
    {
        if (timeBtwShot <= 0)
        {
            GameObject bullet = ObjectPool.Instance.GetObject(_enemyBullet, _enemyFirePoint.position);
            bullet.SetActive(true);
            bullet.GetComponent<Rigidbody2D>().AddForce(_enemyFirePoint.up * _enemyFireForce,ForceMode2D.Impulse);
            timeBtwShot = startTimeBtwShots;  // adds interval between shots
        }
        else
        {
            timeBtwShot -= Time.deltaTime;
        }
    }

}

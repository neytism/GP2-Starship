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
    [SerializeField] private float _damage = 1;
    public GameObject diePEffect;
    [SerializeField] private GameObject _enemyBullet;
    [SerializeField] private Transform _enemyFirePoint;
    [SerializeField] private float _enemyFireForce;
    private float timeBtwShot;
    [SerializeField] private float startTimeBtwShots;

    private ObjectPool _enemyBulletPool;
    private ObjectPool _particlePool;
    
    
    [SerializeField] private bool _canFire = true;
    
    
    
    private void Awake()
    {
        _enemyBulletPool = gameObject.AddComponent<ObjectPool>();
        _particlePool = gameObject.AddComponent<ObjectPool>();
        _player = GameObject.FindObjectOfType<Player>();
        timeBtwShot = startTimeBtwShots;
    }

    private void Update()
    {
        if (_canFire)
        {
            EnemyFire();
        }
       
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player") || col.gameObject.tag.Equals("Bullet")) 
        {
            //deploys particle before destroying object
            //particle system can be converted to pooling system if time possible
            AudioManager.Instance.PlayOnce(AudioManager.Sounds.EnemyDeath);
            GameObject particle = _particlePool.GetObject(diePEffect);
            particle.SetActive(true);
            //GameObject particle = Instantiate(diePEffect, transform.position, Quaternion.identity);
            

            if (col.gameObject.tag.Equals("Player")) {
               _player.ReduceHealth(_damage);
            }
            
            _player.AddKillCount();
            gameObject.SetActive(false);
        }
    }

    public void EnemyFire()
    {
        if (timeBtwShot <= 0)
        {
            //GameObject bullet = Instantiate(_enemyBullet, _enemyFirePoint.position, Quaternion.identity);
            GameObject bullet = _enemyBulletPool.GetObject(_enemyBullet);
            bullet.SetActive(true);
            bullet.GetComponent<Rigidbody2D>().AddForce(_enemyFirePoint.up * _enemyFireForce,ForceMode2D.Impulse);
            timeBtwShot = startTimeBtwShots;
        }
        else
        {
            timeBtwShot -= Time.deltaTime;
        }
    }

}
